using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Creep : MonoBehaviour
{
    enum Type
    {
        melee,
    }

    enum State
    {
        MoveToBase,
        MoveToTarget,
        Attack
    }

    [SerializeField]
    Team team;

    [SerializeField]
    Type type;

    [SerializeField]
    State state;

    [SerializeField]
    UIFollowTarget ui;

    [SerializeField]
    Property property;

    [SerializeField]
    int currentHealth;

    [SerializeField]
    float speed;

    [SerializeField]
    float range;

    [SerializeField]
    float rangeToAttack;

    [SerializeField]
    float timeAttack;
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

    [SerializeField]
    GameObject targetBase;

    [SerializeField]
    GameObject curTarget;

    [SerializeField]
    GameObject arrow;

    [SerializeField]
    int damage;

    void Update()
    {
        if (timeAttackSecond > 0)
        {
            timeAttackSecond -= Time.deltaTime;
        }

        switch (state)
        {
            case State.MoveToBase:
                anim.SetBool("Move", true);
                MoveToBase();
                FindTarget();
                break;
            case State.MoveToTarget:
                anim.SetBool("Move", true);
                MoveToTarget();
                break;
            case State.Attack:
                anim.SetBool("Move", false);
                Attack();
                break;
            default:
                break;
        }
    }

    void MoveToBase()
    {
        if (targetBase.transform.position.x < transform.position.x)
        {
            anim.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (targetBase.transform.position.x < transform.position.x)
        {
            anim.GetComponent<SpriteRenderer>().flipX = false;
        }

        Vector2 dir = targetBase.transform.position - transform.position;
        rb.velocity = dir.normalized * speed;
    }

    void MoveToTarget()
    {
        if (curTarget)
        {
            if (curTarget.transform.position.x < transform.position.x)
            {
                anim.GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (curTarget.transform.position.x > transform.position.x)
            {
                anim.GetComponent<SpriteRenderer>().flipX = false;
            }

            if (Vector2.Distance(transform.position, oldPos) < maxDistance)
            {
                if (Vector2.Distance(transform.position, curTarget.transform.position) <= rangeToAttack)
                {
                    rb.velocity = Vector2.zero;
                    state = State.Attack;
                }
                else
                {
                    Vector2 dir = curTarget.transform.position - transform.position;
                    rb.velocity = dir.normalized * speed;
                }
            }
            else
            {
                state = State.MoveToBase;
            }
        }
        else
        {
            state = State.MoveToBase;
        }
    }

    void Attack()
    {
        if (curTarget)
        {
            if (curTarget.transform.position.x < transform.position.x)
            {
                anim.GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (curTarget.transform.position.x > transform.position.x)
            {
                anim.GetComponent<SpriteRenderer>().flipX = false;
            }

            if (Vector2.Distance(transform.position, curTarget.transform.position) <= rangeToAttack)
            {
                if (timeAttackSecond <= 0)
                {
                    anim.SetTrigger("Attack");
                    timeAttackSecond = timeAttack;

                    GameObject g = Instantiate(arrow, transform.position, Quaternion.identity);
                    g.GetComponent<ArrowOfMinion>().target = curTarget;
                    g.GetComponent<ArrowOfMinion>().damage = damage;

                }
            }
            else
            {
                oldPos = transform.position;
                state = State.MoveToTarget;
            }
        }
        else
        {
            if (timeAttackSecond <= 0)
            {
                anim.SetTrigger("Attack");
                timeAttackSecond = timeAttack;

                GameObject g = Instantiate(arrow, transform.position,Quaternion.identity);
                g.GetComponent<ArrowOfMinion>().target = targetBase;
                    g.GetComponent<ArrowOfMinion>().damage = damage;
            }
        }
    }

    void FindTarget()
    {
        Collider2D[] collider2D = Physics2D.OverlapCircleAll(transform.position, range);

        GameObject gTarget = null;
        float distance = Mathf.Infinity;

        foreach (var item in collider2D)
        {
            if (item.tag == "Player")
            {
                if (team == Team.Red)
                {
                    if (Vector2.Distance(transform.position, item.transform.position) < distance)
                    {
                        distance = Vector2.Distance(transform.position, item.transform.position);
                        gTarget = item.gameObject;
                    }
                }
            }
        }

        if (gTarget)
        {
            curTarget = gTarget;
            oldPos = transform.position;
            state = State.MoveToTarget;
        }
    }

    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;
        ui.transform.GetChild(2).GetComponent<Image>().fillAmount = (float)currentHealth / (float)property.healthPoint;

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        // drop money, exp

        //destroy
        Destroy(ui.gameObject);
        Destroy(gameObject);
    }
}
