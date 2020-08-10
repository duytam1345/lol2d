using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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

    [SerializeField]
    Team team;

    public float speed;

    [SerializeField]
    Property property;

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
        string nameHit = "";
        if (r.collider)
        {
            nameHit = r.collider.name;
        }

        switch (state)
        {
            case State.Idle:
                if (!string.IsNullOrEmpty(nameHit))
                {
                    if (nameHit == "Creep")
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
                }
                break;
            case State.Walk:
                if (!string.IsNullOrEmpty(nameHit))
                {
                    targetPos = Vector2.zero;
                    if (nameHit == "Creep")
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
                }
                break;
            case State.WalkToAttack:
                if (!string.IsNullOrEmpty(nameHit))
                {
                    targetPos = Vector2.zero;
                    if (nameHit == "Creep")
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
                }
                break;
            case State.Attack:
                if (!string.IsNullOrEmpty(nameHit))
                {
                    targetPos = Vector2.zero;
                    if (nameHit == "Creep")
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
                }
                break;
            case State.Spell:
                break;
            default:
                break;
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

                rb2d.velocity = (dir.normalized) * speed;

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
        if (attackSpeedSecond <= 0)
        {
            attackSpeedSecond = property.attackSpeed;

            Vector2 pos = new Vector2(
                Random.Range(targetAttack.transform.position.x - .5f, targetAttack.transform.position.x + .5f),
                Random.Range(targetAttack.transform.position.y - .5f, targetAttack.transform.position.y + .5f));

            Vector3 rot = new Vector3(0, 0, Random.Range(0, 360));

            GameObject e = Instantiate(prefabHit, pos, Quaternion.Euler(rot.x, rot.y, rot.z));
        }
    }

    void Move()
    {
        if (targetPos != Vector2.zero)
        {
            if (Vector2.Distance(transform.position, targetPos) >= .2f)
            {
                Vector2 dir = targetPos - (Vector2)transform.position;

                rb2d.velocity = (dir.normalized) * speed;
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
}
