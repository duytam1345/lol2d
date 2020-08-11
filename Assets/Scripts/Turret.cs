using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Turret : MonoBehaviour
{
    enum State
    {
        Idle,
        Attack,
        Destroy
    }

    public Team team;

    [SerializeField]
    Property property;

    [SerializeField]
    UIFollowTarget ui;

    [SerializeField]
    int currentHealth;

    [SerializeField]
    float attackSpeedSecond;

    [SerializeField]
    GameObject targetAttack;

    [SerializeField]
    State state;

    [SerializeField]
    float range;

    [SerializeField]
    Animator anim;

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

            Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(transform.position, range);
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
                else if (item.GetComponent<Charater>() && item.GetComponent<Charater>().team != team)
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
        if (Vector2.Distance(transform.position, targetAttack.transform.position) > range)
        {
            targetAttack = null;
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
                    targetAttack.GetComponent<Charater>().TakeDamage(gameObject, property.damage);
                }
                attackSpeedSecond = property.attackSpeed;

                Vector2 rectPos = targetAttack.transform.position;
                rectPos = new Vector2(
                    (float)Random.Range(rectPos.x - .5f, rectPos.x + .5f),
                    (float)Random.Range(rectPos.y - .5f, rectPos.y + .5f));
                UIManager.instace.MakeTextDamage(rectPos, property.damage.ToString());
            }
        }
        else
        {
            attackSpeedSecond = property.attackSpeed;
            state = State.Idle;
        }
    }

    public void TakeDamage(GameObject g, int amount)
    {
        currentHealth -= amount;
        if (ui)
        {
            ui.transform.GetChild(2).GetComponent<Image>().fillAmount = (float)currentHealth / (float)property.healthPoint;
        }

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        state = State.Destroy;
        team = Team.Red;
        anim.SetBool("Death", true);
        Destroy(ui.gameObject);
        Destroy(gameObject, 1.5f);
    }
}
