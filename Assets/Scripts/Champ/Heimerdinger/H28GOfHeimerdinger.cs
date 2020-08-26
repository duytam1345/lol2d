using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class H28GOfHeimerdinger : MonoBehaviour
{
    public Team team;

    public Champion c;

    public GameObject bullet;
    public GameObject bulletSecond;

    public float timeAttack;
    public float timeAttackSecond;

    public float range;

    public GameObject targetAttack;

    public float health;
    public float healthSecond;
    public float mana;
    public float manaSecond;

    public float damage;

    public Image imageHealth;
    public Image imageMana;

    Transform childTrans;
    Transform childShot;

    public GameObject PrefabUI;
    public UIFollowTarget ui;

    public bool isUpgrade;

    private void Start()
    {
        childTrans = transform.GetChild(0).transform;
        childShot = childTrans.GetChild(0).transform;

        healthSecond = health;

        ui = Instantiate(PrefabUI, GameObject.Find("Canvas").transform).GetComponent<UIFollowTarget>();
        ui.gTarget = gameObject;

        if (isUpgrade)
        {
            if (ui)
            {
                Destroy(ui.gameObject, 8);
            }
            Destroy(gameObject, 8);
        }
    }

    void Update()
    {
        RecoveryMana(Time.deltaTime);

        if (ui)
        {
            ui.transform.GetChild(2).GetComponent<Image>().fillAmount = healthSecond / health;
        }

        if (!targetAttack)
        {
            FindEnemy();
        }
        else
        {
            Attack();
        }

        SetRotate();
    }

    void SetRotate()
    {
        if (targetAttack)
        {
            Vector3 dir = targetAttack.transform.position - childTrans.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Vector3 q = Quaternion.AngleAxis(angle, Vector3.forward).eulerAngles;
            childTrans.eulerAngles = new Vector3(0, 0, q.z + 90);
        }
    }

    void FindEnemy()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(transform.position, range - .25f);
        foreach (var item in collider2Ds)
        {
            if (item.GetComponent<Creep>() && item.GetComponent<Creep>().team != team)
            {
                targetAttack = item.gameObject;
                return;
            }
            if (item.GetComponent<Turret>() && item.GetComponent<Turret>().team != team)
            {
                targetAttack = item.gameObject;
                return;
            }
        }
    }

    void Attack()
    {
        if (targetAttack == null || Vector3.Distance(transform.position, targetAttack.transform.position) > range)
        {
            targetAttack = null;
        }

        if (targetAttack)
        {
            timeAttackSecond -= Time.deltaTime;
            if (timeAttackSecond <= 0)
            {
                if (manaSecond < mana)
                {
                    GameObject a = Instantiate(bullet, transform.position, Quaternion.identity);
                    a.GetComponent<BulletOfHeimerdinger>().c = c;
                    a.GetComponent<BulletOfHeimerdinger>().dmg =
                        (!isUpgrade ? 6 + damage / 100 * 30 : 80 + damage / 100 * 45);
                    a.GetComponent<BulletOfHeimerdinger>().target = targetAttack;
                    timeAttackSecond = 1/timeAttack;

                    Vector3 dir = targetAttack.transform.position - transform.transform.position;
                    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    Vector3 q = Quaternion.AngleAxis(angle, Vector3.forward).eulerAngles;
                    a.transform.eulerAngles = new Vector3(0, 0, q.z-90);

                    RecoveryMana(1);
                }
                else
                {
                    GameObject a = Instantiate(bulletSecond, transform.position, Quaternion.identity);
                    a.GetComponent<BulletOfHeimerdingerBeam>().c = c;
                    a.GetComponent<BulletOfHeimerdingerBeam>().team = team;
                    a.GetComponent<BulletOfHeimerdingerBeam>().damage =
                        (!isUpgrade ? 40 + damage / 100 * 55 : 100 + damage / 100 * 70);
                    a.GetComponent<BulletOfHeimerdingerBeam>().dir = (targetAttack.transform.position - transform.position);
                    timeAttackSecond = 1 / timeAttack;

                    Vector3 dir = targetAttack.transform.position - transform.transform.position;
                    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    Vector3 q = Quaternion.AngleAxis(angle, Vector3.forward).eulerAngles;
                    a.transform.eulerAngles = new Vector3(0, 0, q.z - 90);

                    manaSecond = 0;
                }
            }
        }

    }

    void RecoveryMana(float amount)
    {
        manaSecond = Mathf.Clamp(manaSecond + amount, 0, mana);

        if (ui)
        {
            ui.transform.GetChild(4).GetComponent<Image>().fillAmount = manaSecond / mana;
        }
    }

    public void TakeDamage(GameObject g, int dmg)
    {
        healthSecond -= dmg;
        UIManager.instance.MakeTextDamage(transform.position, dmg.ToString());

        if (healthSecond <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        if (ui)
        {
            Destroy(ui.gameObject);
        }
        if (gameObject)
        {
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
