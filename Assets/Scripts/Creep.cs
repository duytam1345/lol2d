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
        Idle,
        Move,
        Attack,
        EndGame
    }

    enum Dir
    {
        Left,
        Right
    }

    public
    Team team;

    [SerializeField]
    Type type;

    [SerializeField]
    State state;

    [SerializeField]
    Dir direction;

    [SerializeField]
    Vector2 targetPos;

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
                break;
            case State.EndGame:
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
            rb.velocity = dir.normalized * speed;
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
                if (Vector2.Distance(transform.position, curTarget.transform.position) <= rangeToAttack)
                {
                    targetPos = Vector2.zero;
                    state = State.Attack;
                }
                else
                {
                    Vector2 v = curTarget.transform.position - transform.position;
                    vToSet = v + (Vector2)transform.position;
                }
            }
            else
            {
                Vector2 v = targetBase.transform.position - transform.position;
                vToSet = v + (Vector2)transform.position;
            }
        }
        else
        {
            Vector2 v = targetBase.transform.position - transform.position;
            vToSet = v + (Vector2)transform.position;
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

        RaycastHit2D[] hit = Physics2D.BoxCastAll((Vector2)transform.position + (direction == Dir.Left ? new Vector2(-1, 0) : new Vector2(1, 0)),
            new Vector2(.8f, .8f), 0, direction == Dir.Left ? Vector2.left : Vector2.right, 0);


        foreach (var item in hit)
        {
            if (item.collider.name != name && item.collider.GetComponent<Creep>())
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
                        newP.y = countUp + 1;
                        result = newP;
                        return result;
                    }
                    else
                    {
                        newP.y = countDown - 1;
                        result = newP;
                        return result;
                    }
                }
                else if (permissionDown && !permissionUp)
                {
                    newP.y = countDown - 1;
                    result = newP;
                    return result;
                }
                else if (!permissionDown && permissionUp)
                {
                    newP.y = countUp + 1;
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

            if (Vector2.Distance(transform.position, curTarget.transform.position) <= rangeToAttack)
            {
                if (timeAttackSecond <= 0)
                {
                    anim.SetTrigger("Attack");
                    timeAttackSecond = timeAttack;

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
            else if (item.tag == "Minion")
            {
                if (team == Team.Red && item.GetComponent<Creep>().team == Team.Blue ||
                    team == Team.Blue && item.GetComponent<Creep>().team == Team.Red)
                {
                    if (Vector2.Distance(transform.position, item.transform.position) < distance)
                    {
                        distance = Vector2.Distance(transform.position, item.transform.position);
                        gTarget = item.gameObject;
                    }
                }
            }
            else if (item.tag == "Turret")
            {
                if (item.GetComponent<Turret>().team!=team)
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
            state = State.Move;
        }
    }

    public void TakeDamage(GameObject g, int dmg)
    {
        if (state == State.Move &&  g&& Vector2.Distance(transform.position, g.transform.position) <= rangeToAttack)
        {
            curTarget = g;
        }

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
