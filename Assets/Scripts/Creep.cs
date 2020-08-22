using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Creep : MonoBehaviour
{
    public enum Type
    {
        melee,
        Caster,
        Siege,
        Super
    }

    public enum State
    {
        Idle,
        Move,
        Attack,
        Stun,
        EndGame
    }

    public enum Dir
    {
        Left,
        Right
    }

    public
    Team team;

    [SerializeField]
    Type type;

    public State state;

    public Dir direction;

    [SerializeField]
    Vector2 targetPos;

    public UIFollowTarget ui;

    public Property property;

    public int currentHealth;

    [SerializeField]
    float timeAttackSecond;

    [SerializeField]
    Rigidbody2D rb;

    [SerializeField]
    Animator anim;

    [SerializeField]
    Vector2 oldPos;

    [SerializeField]
    float maxDistance;

    public GameObject targetBase;

    [SerializeField]
    GameObject curTarget;

    [SerializeField]
    GameObject arrow;

    [SerializeField]
    int damage;

    private void Start()
    {
        targetPos = (Vector2)targetBase.transform.position;
    }

    void Update()
    {
        property.UpdateValue();
        switch (state)
        {
            case State.Idle:
                anim.SetBool("Move", false);
                rb.velocity = Vector2.zero;
                state = State.Move;
                break;
            case State.Move:
                anim.SetBool("Move", true);
                SetTarget();
                MoveToTarget();
                break;
            case State.Attack:
                anim.SetBool("Move", false);
                Attack();
                if (timeAttackSecond > 0)
                {
                    timeAttackSecond -= Time.deltaTime;
                }
                break;
            case State.EndGame:
                break;
            case State.Stun:
                rb.velocity = Vector2.zero;
                break;
        }
        if (!targetBase)
        {
            state = State.EndGame;
        }
    }

    void MoveToTarget()
    {
        if (targetPos.x < transform.position.x)
        {
            anim.GetComponent<SpriteRenderer>().flipX = true;
            direction = Dir.Left;
        }
        else if (targetPos.x < transform.position.x)
        {
            anim.GetComponent<SpriteRenderer>().flipX = false;
            direction = Dir.Right;
        }

        if (targetPos != Vector2.zero)
        {
            Vector2 dir = targetPos - (Vector2)transform.position;
            rb.velocity = dir.normalized * property.moveSpeedCurrent;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    void SetTarget()
    {
        FindEnemy();

        Vector2 vToSet = Vector2.zero;

        if (curTarget)
        {
            if (curTarget.transform.position.x < transform.position.x)
            {
                anim.GetComponent<SpriteRenderer>().flipX = true;
                direction = Dir.Left;
            }
            else if (curTarget.transform.position.x > transform.position.x)
            {
                anim.GetComponent<SpriteRenderer>().flipX = false;
                direction = Dir.Right;
            }

            if (Vector2.Distance(transform.position, oldPos) < maxDistance)
            {
                if (Vector2.Distance(transform.position, curTarget.transform.position) <= property.rangeToAttack)
                {
                    targetPos = Vector2.zero;
                    state = State.Attack;
                }
                else
                {
                    Vector2 v = curTarget.transform.position - transform.position;
                    vToSet = v + (Vector2)transform.position;
                    curTarget = null;
                }
            }
            else
            {
                Vector2 v = targetBase.transform.position - transform.position;
                vToSet = v + (Vector2)transform.position;
                curTarget = null;
            }
        }
        else
        {
            Vector2 v = targetBase.transform.position - transform.position;
            vToSet = v + (Vector2)transform.position;
        }

        if (targetPos != (Vector2)targetBase.transform.position &&
            Vector2.Distance(transform.position, targetPos) >= .1f && targetPos != Vector2.zero)
        {
            return;
        }

        if (FindWay() != Vector2.zero)
        {
            targetPos = (Vector2)transform.position + FindWay();
        }
        else
        {
            targetPos = vToSet;
        }
    }

    Vector2 FindWay()
    {
        //kiem tra ben tren
        bool permissionUp = true;
        Collider2D[] cUp = Physics2D.OverlapBoxAll((Vector2)transform.position + Vector2.up, new Vector2(.2f, .2f), 0);
        foreach (var c in cUp)
        {
            if (c.tag == "Minion")
            {
                permissionUp = false;
            }
        }
        //kiem tra ben duoi
        bool permissionDown = true;
        Collider2D[] cDown = Physics2D.OverlapBoxAll((Vector2)transform.position + Vector2.down, new Vector2(.2f, .2f), 0);
        foreach (var c in cDown)
        {
            if (c.tag == "Minion")
            {
                permissionDown = false;
            }
        }
        //kiem tra phia truoc
        bool permissionFor = true;
        Collider2D[] cFor = Physics2D.OverlapBoxAll((Vector2)transform.position + (direction == Dir.Left ? Vector2.left : Vector2.right), new Vector2(.8f, .8f), 0);
        foreach (var c in cFor)
        {
            if (c.tag == "Minion")
            {
                permissionFor = false;
            }
        }

        //neu 2 duong bi chan -> dung yen
        if (!permissionDown && !permissionUp && !permissionFor)
        {
            state = State.Idle;
            return Vector2.zero;
        }

        Vector2 result = Vector2.zero;

        RaycastHit2D[] hit = Physics2D.BoxCastAll((Vector2)transform.position + (direction == Dir.Left ? new Vector2(-.1f, 0) : new Vector2(.1f, 0)),
            new Vector2(1f, .1f), 0, direction == Dir.Left ? Vector2.left : Vector2.right, 0);

        foreach (var item in hit)
        {
            if (!GameObject.ReferenceEquals(item.collider.gameObject, gameObject) && item.collider.GetComponent<Creep>())
            {
                Vector2 newP = Vector2.zero;

                //kiem tra so luong vat can ben tren
                int countUp = 0;
                for (int i = 1; i <= 3; i++)
                {
                    newP.y = i;
                    Vector2 point = (Vector2)transform.position + (direction == Dir.Left ? Vector2.left : Vector2.right) + newP;
                    Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(point, new Vector2(.1f, .1f), 0);

                    foreach (var c in collider2Ds)
                    {
                        if (c.tag == "Minion")
                        {
                            countUp++;
                            break;
                        }
                    }
                }

                //kiem tra so luong vat can ben duoi
                int countDown = 0;
                for (int i = -1; i >= -3; i--)
                {
                    newP.y = i;
                    Vector2 point = (Vector2)transform.position + (direction == Dir.Left ? Vector2.left : Vector2.right) + newP;
                    Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(point, new Vector2(.1f, .1f), 0);

                    foreach (var c in collider2Ds)
                    {
                        if (c.tag == "Minion")
                        {
                            countDown++;
                            break;
                        }
                    }
                }

                newP = Vector2.zero;

                if (permissionDown && permissionUp)
                {
                    if (countDown >= countUp)
                    {
                        newP.y = countUp * 1.2f + 1.2f;
                        result = newP;
                        return result;
                    }
                    else
                    {
                        newP.y = -(countDown * 1.2f + 1.2f);
                        result = newP;
                        return result;
                    }
                }
                else if (permissionDown && !permissionUp)
                {
                    newP.y = -(countDown * 1.2f + 1.2f);
                    result = newP;
                    return result;
                }
                else if (!permissionDown && permissionUp)
                {
                    newP.y = countUp * 1.2f + 1.2f;
                    result = newP;
                    return result;
                }
            }
        }
        return result;
    }

    void Attack()
    {
        if (curTarget)
        {
            if (curTarget.GetComponent<Champion>() && curTarget.GetComponent<Champion>().state == Champion.State.Death)
            {
                curTarget = null;
                return;
            }
            if (curTarget.transform.position.x < transform.position.x)
            {
                anim.GetComponent<SpriteRenderer>().flipX = true;
                direction = Dir.Left;
            }
            else if (curTarget.transform.position.x > transform.position.x)
            {
                anim.GetComponent<SpriteRenderer>().flipX = false;
                direction = Dir.Right;
            }

            if (Vector2.Distance(transform.position, curTarget.transform.position) <= property.rangeToAttack)
            {
                if (timeAttackSecond <= 0)
                {
                    anim.SetTrigger("Attack");
                    timeAttackSecond = 1 / property.attackSpeed;

                    GameObject g = Instantiate(arrow, transform.position, Quaternion.identity);
                    g.GetComponent<ArrowOfMinion>().g = gameObject;
                    g.GetComponent<ArrowOfMinion>().target = curTarget;
                    g.GetComponent<ArrowOfMinion>().damage = damage;
                }
            }
            else
            {
                oldPos = transform.position;
                state = State.Move;
            }
        }
        else
        {
            state = State.Idle;
        }
    }

    void FindEnemy()
    {
        Collider2D[] collider2D = Physics2D.OverlapCircleAll(transform.position, property.rangeToAttack);

        GameObject gTarget = null;
        float distance = Mathf.Infinity;

        foreach (var item in collider2D)
        {
            if (item.GetComponent<Champion>() && item.GetComponent<Champion>().state != Champion.State.Death)
            {
                if (item.GetComponent<Champion>().propertyChampion.healthPointSecond > 0 && team != item.GetComponent<Champion>().team)
                {
                    if (Vector2.Distance(transform.position, item.transform.position) < distance)
                    {
                        distance = Vector2.Distance(transform.position, item.transform.position);
                        gTarget = item.gameObject;
                    }
                }
            }
            else if (item.GetComponent<Creep>() && team != item.GetComponent<Creep>().team)
            {
                if (Vector2.Distance(transform.position, item.transform.position) < distance)
                {
                    distance = Vector2.Distance(transform.position, item.transform.position);
                    gTarget = item.gameObject;
                }
            }
            else if (item.GetComponent<Turret>() && item.GetComponent<Turret>().team != team)
            {
                if (Vector2.Distance(transform.position, item.transform.position) < distance)
                {
                    distance = Vector2.Distance(transform.position, item.transform.position);
                    gTarget = item.gameObject;
                }
            }
            else if (item.GetComponent<H28GOfHeimerdinger>() && item.GetComponent<H28GOfHeimerdinger>().team != team)
            {
                if (Vector2.Distance(transform.position, item.transform.position) < distance)
                {
                    distance = Vector2.Distance(transform.position, item.transform.position);
                    gTarget = item.gameObject;
                }
            }
        }

        if (gTarget)
        {
            curTarget = gTarget;
            oldPos = transform.position;
            state = State.Move;
        }
    }

    public void TakeDamage(GameObject g, int dmg)
    {
        UIManager.instace.MakeTextDamage(transform.position, dmg.ToString());

        if (state == State.Move && g && Vector2.Distance(transform.position, g.transform.position) <= property.rangeToAttack)
        {
            curTarget = g;
        }

        currentHealth -= dmg;
        if (ui)
        {
            ui.transform.GetChild(2).GetComponent<Image>().fillAmount = (float)currentHealth / (float)property.healthPoint;
        }

        if (currentHealth <= 0)
        {
            if (g.tag == "FromChampion")
            {
                Vector2 p = new Vector2(transform.position.x, transform.position.y + 2);
                int money = CongThuc.MoneyOfCreep(type);

                UIManager.instace.MakeTextMoney(p, money.ToString());
                FindObjectOfType<Champion>().propertyChampion.money += money;
            }
            Death();
        }
    }

    void Death()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(transform.position, 7);
        foreach (var item in collider2Ds)
        {
            if (item.GetComponent<Charater>() && item.GetComponent<Charater>().champion.team != team)
            {
                item.GetComponentInChildren<Champion>().TakeExp(CongThuc.ExpOfCreep(type));
            }
        }

        Destroy(ui.gameObject);
        Destroy(gameObject);
    }
}
