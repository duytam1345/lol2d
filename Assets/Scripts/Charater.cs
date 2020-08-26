using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Charater : MonoBehaviour
{

    public Champion champion;

    public int currentHealth;


    [SerializeField]
    Rigidbody2D rb2d;

    [SerializeField]
    Animator anim;

    [SerializeField]
    bool attacking;

    //private void Awake()
    //{
    //champion =  GameObject.Find("aasd").GetComponent<MundoChampion>();
    //}

    private void Start()
    {
        champion = GetComponent<Champion>();
    }

    void Update()
    {
        UIManager.instance.UpdateImage(champion);

        champion.SkillPassive();
        if (InputManager.m_KeyDownQ)
        {
            if (InputManager.m_KeyCtrl)
            {
                UIManager.instance.BtnUpgradeSkill("Q");
            }
            else
            {
                champion.StopReCall();
                champion.SkillQ();
            }
        }
        if (InputManager.m_KeyDownW)
        {
            if (InputManager.m_KeyCtrl)
            {
                UIManager.instance.BtnUpgradeSkill("W");
            }
            else
            {
                champion.StopReCall();
                champion.SkillW();
            }
        }
        if (InputManager.m_KeyDownE)
        {
            if (InputManager.m_KeyCtrl)
            {
                UIManager.instance.BtnUpgradeSkill("E");
            }
            else
            {
                champion.StopReCall();
                champion.SkillE();
            }
        }
        if (InputManager.m_KeyDownR)
        {
            if (InputManager.m_KeyCtrl)
            {
                UIManager.instance.BtnUpgradeSkill("R");
            }
            else
            {
                champion.StopReCall();
                champion.SkillR();
            }
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
            UIManager.instance.ClickShop();
        }

        if (InputManager.m_KeyDownB)
        {
            champion.ReCall();
        }

        if (InputManager.m_KeyDownTab)
        {
            UIManager.instance.SetTabPanel(true);
        }
        if (InputManager.m_KeyUpTab)
        {
            UIManager.instance.SetTabPanel(false);
        }

        if (InputManager.m_KeyDownC)
        {
            transform.Find("Circle Range").localScale =
            new Vector3(champion.propertyChampion.attackRange_Real, champion.propertyChampion.attackRange_Real, 0);
            transform.Find("Circle Range").gameObject.SetActive(true);
        }
        if (InputManager.m_KeyUpC)
        {
            transform.Find("Circle Range").gameObject.SetActive(false);
        }

        switch (champion.state)
        {
            case Champion.State.Idle:
                break;
            case Champion.State.Walk:
                champion.Move();
                break;
            case Champion.State.WalkToAttack:
                champion.MoveToAttack();
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
            Mananger.instance.MakeAnimClickMove(Camera.main.ScreenToWorldPoint(InputManager.m_mousePosition));
            RightClick();
        }
        if (InputManager.m_GetMouseButtonRight)
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
                UIManager.instance.ClickShop();
            }
            else if (r.collider.GetComponent<Creep>())
            {
                UIManager.instance.SetInfoPanel(r.collider.gameObject);
            }
            else if (r.collider.GetComponent<Turret>())
            {
                UIManager.instance.SetInfoPanel(r.collider.gameObject);
            }
        }
        else
        {
            UIManager.instance.SetInfoPanel(null);
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
        if (r.collider && r.collider.GetComponent<Creep>())
        {
            if (r.collider.GetComponent<Creep>().team != champion.team && champion.canAttack)
            {
                champion.targetAttack = r.collider.gameObject;
                champion.state = Champion.State.WalkToAttack;

                Vector2 dir = (champion.targetAttack.transform.position - champion.transform.position).normalized;
                champion.anim.SetFloat("VelocityX", dir.x);
                champion.anim.SetFloat("VelocityY", dir.y);

                champion.StopReCall();
            }
        }
        else if (r.collider && r.collider.GetComponent<Turret>())
        {
            if (r.collider.GetComponent<Turret>().team != champion.team && champion.canAttack)
            {
                champion.targetAttack = r.collider.gameObject;
                champion.state = Champion.State.WalkToAttack;

                Vector2 dir = (champion.targetAttack.transform.position - champion.transform.position).normalized;
                champion.anim.SetFloat("VelocityX", dir.x);
                champion.anim.SetFloat("VelocityY", dir.y);

                champion.StopReCall();
            }
        }
        else if (r.collider && r.collider.GetComponent<Champion>()&& r.collider.GetComponent<Champion>().team != champion.team)
        {
            if (r.collider.GetComponent<Champion>().team != champion.team && champion.canAttack)
            {
                champion.targetAttack = r.collider.gameObject;
                champion.state = Champion.State.WalkToAttack;

                Vector2 dir = (champion.targetAttack.transform.position - champion.transform.position).normalized;
                champion.anim.SetFloat("VelocityX", dir.x);
                champion.anim.SetFloat("VelocityY", dir.y);

                champion.StopReCall();
            }
        }
        else
        {
            attacking = false;
            champion.state = Champion.State.Walk;
            champion.targetAttack = null;
            champion.targetPos = Camera.main.ScreenToWorldPoint(InputManager.m_mousePosition);

            champion.StopReCall();
        }
    }
}
