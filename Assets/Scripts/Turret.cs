using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ValueExtra
{
    public float timeLeft;
    public float value;
}

[System.Serializable]
public class Property
{
    public float rangeToAttack;

    public float attackSpeed;

    public int healthPoint;

    public int damage;

    public float arrmor;
    public Dictionary<string, ValueExtra> arrmorExtra = new Dictionary<string, ValueExtra>();
    public float arrmor_Real;

    public float magicResistance;
    public Dictionary<string, ValueExtra> magicResistanceExtra = new Dictionary<string, ValueExtra>();
    public float magicResistance_Real;

    public float moveSpeedBase;
    public Dictionary<string, ValueExtra> moveSpeedExtra = new Dictionary<string, ValueExtra>();//tốc độ phụ thêm là con số, không phải phần trăm
    public float moveSpeedCurrent;

    public void UpdateValue()
    {
        //Giáp
        arrmor_Real = arrmor;
        foreach (var item in arrmorExtra.Values)
        {
            arrmor_Real += item.value;
        }
        //

        //Kháng phép
        magicResistance_Real = magicResistance;
        foreach (var item in magicResistanceExtra.Values)
        {
            magicResistance_Real += item.value;
        }
        //

        //Di chuyển
        moveSpeedCurrent = moveSpeedBase;
        foreach (var item in moveSpeedExtra.Values)
        {
            moveSpeedCurrent += item.value;

            item.timeLeft -= Time.deltaTime;
        }
        List<string> keys = new List<string>();
        foreach (var item in moveSpeedExtra.Keys)
        {
            if (moveSpeedExtra[item].timeLeft <= 0)
            {
                keys.Add(item);
            }
        }
        foreach (var item in keys)
        {
            moveSpeedExtra.Remove(item);
        }
        //
    }
}

public class Turret : MonoBehaviour
{
    enum State
    {
        Idle,
        Attack,
        Destroy
    }

    public Team team;

    public Property property;

    public UIFollowTarget ui;

    public int currentHealth;

    [SerializeField]
    float attackSpeedSecond;

    [SerializeField]
    GameObject targetAttack;

    [SerializeField]
    State state;

    [SerializeField]
    Animator anim;

    private void Start()
    {
        if (team == Team.Red)
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private void Update()
    {
        switch (state)
        {
            case State.Idle:
                FindTarget();
                break;
            case State.Attack:
                AttackTarget();
                break;
            case State.Destroy:
                break;
            default:
                break;
        }
    }

    void FindTarget()
    {
        if (!targetAttack)
        {
            float minDistance = Mathf.Infinity;
            GameObject minG = null;

            Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(transform.position, property.rangeToAttack);
            foreach (var item in collider2Ds)
            {
                if (item.GetComponent<Creep>() && item.GetComponent<Creep>().team != team)
                {
                    if (Vector2.Distance(transform.position, item.transform.position) < minDistance)
                    {
                        minG = item.gameObject;
                        minDistance = Vector2.Distance(transform.position, item.transform.position);
                    }
                }
                else if (item.GetComponent<Charater>() && item.GetComponent<Charater>().champion.team != team)
                {
                    if (Vector2.Distance(transform.position, item.transform.position) < minDistance)
                    {
                        minG = item.gameObject;
                        minDistance = Vector2.Distance(transform.position, item.transform.position);
                    }
                }
                else if (item.GetComponent<H28GOfHeimerdinger>() && item.GetComponent<H28GOfHeimerdinger>().team != team)
                {
                    if (Vector2.Distance(transform.position, item.transform.position) < minDistance)
                    {
                        minG = item.gameObject;
                        minDistance = Vector2.Distance(transform.position, item.transform.position);
                    }
                }
            }

            if (minG)
            {
                targetAttack = minG;
                state = State.Attack;
            }
        }
    }

    void AttackTarget()
    {
        if (targetAttack && Vector2.Distance(transform.position, targetAttack.transform.position) > property.rangeToAttack)
        {
            targetAttack = null;
        }
        if (targetAttack)
        {
            if (targetAttack.GetComponent<Champion>())
            {
                if (targetAttack.GetComponent<Champion>().state == Champion.State.Death)
                {
                    targetAttack = null;
                }
            }
        }

        if (targetAttack)
        {
            attackSpeedSecond -= Time.deltaTime;

            if (attackSpeedSecond <= 0)
            {
                anim.SetTrigger("Attack");
                if (targetAttack.GetComponent<Creep>())
                {
                    targetAttack.GetComponent<Creep>().TakeDamage(gameObject, property.damage);
                }
                else if (targetAttack.GetComponent<Charater>())
                {
                    float resultDmg = CongThuc.LayDamage(property.damage, targetAttack.GetComponent<Champion>().propertyChampion.arrmor_Real);
                    targetAttack.GetComponent<Charater>().champion.TakeDamage(gameObject, (int)resultDmg, targetAttack.transform.position);
                }
                else if (targetAttack.GetComponent<H28GOfHeimerdinger>())
                {
                    targetAttack.GetComponent<H28GOfHeimerdinger>().TakeDamage(gameObject, property.damage);
                }
                attackSpeedSecond = 1 / property.attackSpeed;
            }
        }
        else
        {
            attackSpeedSecond = 1 / property.attackSpeed;
            state = State.Idle;
        }
    }

    public void TakeDamage(GameObject g, int amount)
    {
        currentHealth -= amount;
        if (ui)
        {
            ui.transform.GetChild(3).GetComponent<Image>().fillAmount = (float)currentHealth / (float)property.healthPoint;
        }

        if (currentHealth <= 0)
        {
            UIManager.instance.CreateTextDestroyed(g, gameObject);
            Death();
        }
    }

    void Death()
    {
        FindObjectOfType<Charater>().champion.propertyChampion.money += 250;
        UIManager.instance.MakeTextMoney(transform.position, "250g");

        state = State.Destroy;
        team = Team.Red;
        anim.SetBool("Death", true);
        if (ui)
        {
            Destroy(ui.gameObject);
        }
        Destroy(gameObject, 1.5f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, property.rangeToAttack);
    }
}
