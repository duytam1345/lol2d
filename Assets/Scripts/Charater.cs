using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Charater : MonoBehaviour
{


    public Champion champion;

    [SerializeField]
    UIFollowTarget uI;

    public int currentHealth;


    [SerializeField]
    Rigidbody2D rb2d;

    [SerializeField]
    Animator anim;

    [SerializeField]
    Vector2 targetPos;

    [SerializeField]
    bool attacking;

    //private void Awake()
    //{
    //champion =  GameObject.Find("aasd").GetComponent<MundoChampion>();
    //}

    void Update()
    {
        UIManager.instace.UpdateImage(champion);

        champion.SkillPassive();
        if (InputManager.m_KeyDownQ)
        {
            champion.StopReCall();
            champion.SkillQ();
        }
        if (InputManager.m_KeyDownW)
        {
            champion.StopReCall();
            champion.SkillW();
        }
        if (InputManager.m_KeyDownE)
        {
            champion.StopReCall();
            champion.SkillE();
        }
        if (InputManager.m_KeyDownR)
        {
            champion.StopReCall();
            champion.SkillR();
        }

        if (InputManager.m_KeyDownD)
        {
            champion.spellD.Use(champion);
        }

        if (InputManager.m_KeyDownF)
        {
            champion.spellF.Use(champion);
        }

        if (InputManager.m_KeyDownP)
        {
            UIManager.instace.ClickShop();
        }

        if (InputManager.m_KeyDownB)
        {
            champion.ReCall();
        }

        switch (champion.state)
        {
            case Champion.State.Idle:
                break;
            case Champion.State.Walk:
                Move();
                break;
            case Champion.State.WalkToAttack:
                MoveToAttack();
                break;
            case Champion.State.Attack:
                champion.LoadAttack();
                break;
            case Champion.State.Spell:
                break;
            default:
                break;
        }

        SetCursor();

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
            rb2d.velocity = Vector2.zero;
            champion.targetAttack = null;
            champion.state = Champion.State.Idle;
        }
    }

    void SetCursor()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(InputManager.m_mousePosition), Vector2.zero);
        if (hit.collider)
        {
            if (hit.collider.GetComponent<Creep>() && hit.collider.GetComponent<Creep>().team != champion.team)
            {
                Mananger.instance.SetCursor(1);
            }
            else if (hit.collider.GetComponent<Turret>() && hit.collider.GetComponent<Turret>().team != champion.team)
            {
                Mananger.instance.SetCursor(1);
            }
            else
            {
                Mananger.instance.SetCursor(0);
            }
        }
        else
        {
            Mananger.instance.SetCursor(0);
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
            else if (r.collider.GetComponent<Creep>())
            {
                UIManager.instace.SetInfoPanel(r.collider.gameObject);
            }
            else if (r.collider.GetComponent<Turret>())
            {
                UIManager.instace.SetInfoPanel(r.collider.gameObject);
            }
        }
        else
        {
            UIManager.instace.SetInfoPanel(null);
        }
    }

    void RightClick()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(InputManager.m_mousePosition);
        RaycastHit2D r = Physics2D.Raycast(mousePos, Vector2.zero);
        switch (champion.state)
        {
            case Champion.State.Idle:
                Check(r);
                break;
            case Champion.State.Walk:
                Check(r);
                break;
            case Champion.State.WalkToAttack:
                Check(r);
                break;
            case Champion.State.Attack:
                Check(r);
                break;
            case Champion.State.Spell:
                break;
            case Champion.State.Death:
                if (champion.canMove)
                {
                    Check(r);
                }
                break;
            default:
                break;
        }
    }

    void Check(RaycastHit2D r)
    {
        if (r.collider && r.collider.tag == "Minion")
        {
            if (r.collider.GetComponent<Creep>().team != champion.team && champion.canAttack)
            {
                champion.targetAttack = r.collider.gameObject;
                champion.state = Champion.State.WalkToAttack;

                champion.StopReCall();
            }
        }
        else if (r.collider && r.collider.tag == "Turret" && r.collider.GetComponent<Turret>().team != champion.team)
        {
            if (r.collider.GetComponent<Turret>().team != champion.team && champion.canAttack)
            {
                champion.targetAttack = r.collider.gameObject;
                champion.state = Champion.State.WalkToAttack;

                champion.StopReCall();
            }
        }
        else
        {
            attacking = false;
            champion.state = Champion.State.Walk;
            champion.targetAttack = null;
            targetPos = Camera.main.ScreenToWorldPoint(InputManager.m_mousePosition);

            Mananger.instance.MakeAnimClickMove(Camera.main.ScreenToWorldPoint(InputManager.m_mousePosition));

            champion.StopReCall();
        }
    }

    void MoveToAttack()
    {
        if (champion.targetAttack)
        {
            targetPos = Vector2.zero;
            attacking = true;
            if (Vector2.Distance(transform.position, champion.targetAttack.transform.position) > champion.propertyChampion.attackRange_Real)
            {
                Vector2 dir = (Vector2)champion.targetAttack.transform.position - (Vector2)transform.position;

                rb2d.velocity = (dir.normalized) * champion.propertyChampion.moveSpeed_Real;

                champion.state = Champion.State.WalkToAttack;
            }
            else
            {
                rb2d.velocity = Vector2.zero;

                champion.state = Champion.State.Attack;
            }
        }
        else
        {
            champion.state = Champion.State.Idle;
            attacking = false;
        }
    }

    void Move()
    {
        if (targetPos != Vector2.zero)
        {
            if (Vector2.Distance(transform.position, targetPos) >= .2f)
            {
                Vector2 dir = targetPos - (Vector2)transform.position;

                rb2d.velocity = (dir.normalized) * champion.propertyChampion.moveSpeed_Real;
                champion.state = Champion.State.Walk;
            }
            else
            {
                champion.state = Champion.State.Idle;
                targetPos = Vector2.zero;
                rb2d.velocity = Vector2.zero;
            }
        }
    }

}
