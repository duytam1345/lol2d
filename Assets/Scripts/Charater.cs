using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Charater : MonoBehaviour
{
    enum State
    {
        Idle,
        Walk,
        WalkToAttack,
        Attack,
        Spell
    }

    [SerializeField]
    State state;

    public Team team;

    [SerializeField]
    UIFollowTarget uI;

    [SerializeField]
    Property property;

    [SerializeField]
    int currentHealth;

    [SerializeField]
    float attackSpeedSecond;

    [SerializeField]
    GameObject prefabHit;

    [SerializeField]
    Rigidbody2D rb2d;

    [SerializeField]
    Animator anim;

    [SerializeField]
    Vector2 targetPos;

    [SerializeField]
    GameObject targetAttack;

    [SerializeField]
    bool attacking;

    void Update()
    {
        switch (state)
        {
            case State.Idle:
                break;
            case State.Walk:
                Move();
                break;
            case State.WalkToAttack:
                MoveToAttack();
                break;
            case State.Attack:
                Attack();
                break;
            case State.Spell:
                break;
            default:
                break;
        }

        if (attackSpeedSecond > 0)
        {
            attackSpeedSecond -= Time.deltaTime;
        }

        if (InputManager.m_GetMouseButtonDownRight)
        {
            RightClick();
        }

        if (InputManager.m_GetMouseButtonDownLeft)
        {
            LeftClick();
        }

        if (InputManager.m_KeyDownS)
        {
            attacking = false;
            targetAttack = null;
            state = State.Idle;
        }
    }

    void LeftClick()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(InputManager.m_mousePosition);

        RaycastHit2D r = Physics2D.Raycast(mousePos, Vector2.zero);
        if (r.collider)
        {
            string n = r.collider.name;

            if (n == "Ong chu tiem trang bi")
            {
                UIManager.instace.ClickShop();
            }
        }
    }

    void RightClick()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(InputManager.m_mousePosition);
        RaycastHit2D r = Physics2D.Raycast(mousePos, Vector2.zero);
        switch (state)
        {
            case State.Idle:
                Check(r);
                break;
            case State.Walk:
                Check(r);
                break;
            case State.WalkToAttack:
                Check(r);
                break;
            case State.Attack:
                Check(r);
                break;
            case State.Spell:
                break;
            default:
                break;
        }
    }

    void Check(RaycastHit2D r)
    {
        if (r.collider && r.collider.tag == "Minion" && r.collider.GetComponent<Creep>().team != team)
        {
            if (r.collider.GetComponent<Creep>().team != team)
            {
                targetAttack = r.collider.gameObject;
                state = State.WalkToAttack;
            }
        }
        else if (r.collider && r.collider.tag == "Turret" && r.collider.GetComponent<Turret>().team != team)
        {
            if (r.collider.GetComponent<Turret>().team != team)
            {
                targetAttack = r.collider.gameObject;
                state = State.WalkToAttack;
            }
        }
        else
        {
            attacking = false;
            targetAttack = null;
            targetPos = Camera.main.ScreenToWorldPoint(InputManager.m_mousePosition);
            state = State.Walk;

            Mananger.instance.MakeAnimClickMove(Camera.main.ScreenToWorldPoint(InputManager.m_mousePosition));
        }
    }

    void MoveToAttack()
    {
        if (targetAttack)
        {
            targetPos = Vector2.zero;
            attacking = true;
            if (Vector2.Distance(transform.position, targetAttack.transform.position) > property.rangeToAttack)
            {
                Vector2 dir = (Vector2)targetAttack.transform.position - (Vector2)transform.position;

                rb2d.velocity = (dir.normalized) * property.moveSpeed;

                state = State.WalkToAttack;
            }
            else
            {
                rb2d.velocity = Vector2.zero;

                state = State.Attack;
            }
        }
        else
        {
            state = State.Idle;
            attacking = false;
        }
    }

    void Attack()
    {
        if (targetAttack)
        {
            if (attackSpeedSecond <= 0)
            {
                if (targetAttack.GetComponent<Creep>())
                {
                    targetAttack.GetComponent<Creep>().TakeDamage(gameObject, property.damage);
                    UIManager.instace.MakeTextDamage(targetAttack.transform.position, property.damage.ToString());
                }
                else if (targetAttack.GetComponent<Turret>())
                {
                    targetAttack.GetComponent<Turret>().TakeDamage(gameObject, property.damage);
                    UIManager.instace.MakeTextDamage(targetAttack.transform.position, property.damage.ToString());
                }

                attackSpeedSecond = 1 / property.attackSpeed;

                Vector2 pos = new Vector2(
                    Random.Range(targetAttack.transform.position.x - .5f, targetAttack.transform.position.x + .5f),
                    Random.Range(targetAttack.transform.position.y - .5f, targetAttack.transform.position.y + .5f));

                Vector3 rot = new Vector3(0, 0, Random.Range(0, 360));

                GameObject e = Instantiate(prefabHit, pos, Quaternion.Euler(rot.x, rot.y, rot.z));
            }
        }
        else
        {
            attacking = false;
            targetPos = Vector2.zero;
            rb2d.velocity = Vector2.zero;
            state = State.Idle;
        }
    }

    void Move()
    {
        if (targetPos != Vector2.zero)
        {
            if (Vector2.Distance(transform.position, targetPos) >= .2f)
            {
                Vector2 dir = targetPos - (Vector2)transform.position;

                rb2d.velocity = (dir.normalized) * property.moveSpeed;
                state = State.Walk;
            }
            else
            {
                state = State.Idle;
                targetPos = Vector2.zero;
                rb2d.velocity = Vector2.zero;
            }
        }
    }

    public void TakeDamage(GameObject g, int dmg)
    {
        currentHealth -= dmg;
        uI.transform.GetChild(2).GetComponent<Image>().fillAmount = (float)currentHealth / (float)property.healthPoint;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, property.rangeToAttack);
    }
}
