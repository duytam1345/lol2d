using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeimerdingerChampion : Champion
{
    public GameObject objectAttack;

    [Header("Chiêu Q")]
    public Sprite spriteUpgradeQ;
    public GameObject turretQ;
    public int costQ;
    public List<H28GOfHeimerdinger> h28GOfHeimerdingers;
    public float timeCooldownKit;
    public int leftKit;
    public GameObject iconEffectQ;


    [Header("Chiêu W")]
    public Sprite spriteUpgradeW;
    public GameObject prefabSkillW;
    public int costW;

    [Header("Chiêu E")]
    public Sprite spriteUpgradeE;
    public GameObject prefabE;
    public int costE;

    [Header("Chiêu R")]

    bool inUpgrade;
    public GameObject iconEffectR;


    public override void Start()
    {
        base.Start();

        gameObject.AddComponent(iconEffectQ.GetComponent<IconEffect>().GetType());


        GetComponent<IconCooldownSkillQHeimerdinger>().icon = iconEffectQ.GetComponent<IconEffect>().icon;
        GetComponent<IconCooldownSkillQHeimerdinger>().heimerdinger = this;

        UIManager.instance.CreateSlotEffect("Skill Q Heimerdinger", GetComponent<IconEffect>());

        h28GOfHeimerdingers.Add(null);
        h28GOfHeimerdingers.Add(null);
        h28GOfHeimerdingers.Add(null);
        LevelUp();
    }

    void Update()
    {
        //set ainm
        if (anim.GetFloat("VelocityX") < 0)
        {
            anim.GetComponent<SpriteRenderer>().flipX = false;
            anim.transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (anim.GetFloat("VelocityX") > 0)
        {
            anim.GetComponent<SpriteRenderer>().flipX = true;
            anim.transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = true;
        }

        if (state == State.Walk || state == State.WalkToAttack)
        {
            anim.SetBool("Move", true);
        }
        else
        {
            anim.SetBool("Move", false);
        }
        //

        if (isDeath)
        {
            timeLeftToRespawn -= Time.deltaTime;
            UIManager.instance.imageAvatar.transform.GetChild(0).GetComponent<Text>().text = ((int)timeLeftToRespawn).ToString();
            if (timeLeftToRespawn <= 0)
            {
                UIManager.instance.imageAvatar.transform.GetChild(0).GetComponent<Text>().text = "";
                SpawnAtFountain();
            }
        }

        if (recalling)
        {
            timeRecall += Time.deltaTime;
            if (timeRecall >= 4.5f)
            {
                recalling = false;
                timeRecall = 0;
                if (GameObject.Find("Effect Recall"))
                {
                    Destroy(GameObject.Find("Effect Recall"));
                }
                SpawnAtFountain();
            }
        }

        PerSecond();

        if (leftKit < 3)
        {
            timeCooldownKit -= Time.deltaTime;
            if (timeCooldownKit <= 0)
            {
                timeCooldownKit = 11;
                leftKit++;
            }
        }

        if (timeCoolDownSkillQSecond > 0)
        {
            timeCoolDownSkillQSecond -= Time.deltaTime;
        }
        if (timeCoolDownSkillWSecond > 0)
        {
            timeCoolDownSkillWSecond -= Time.deltaTime;
        }
        if (timeCoolDownSkillESecond > 0)
        {
            timeCoolDownSkillESecond -= Time.deltaTime;
        }
        if (timeCoolDownSkillRSecond > 0)
        {
            timeCoolDownSkillRSecond -= Time.deltaTime;
        }
    }

    public override void Move()
    {
        base.Move();
        if (rb2d.velocity.x != 0)
        {
            anim.SetFloat("VelocityX", rb2d.velocity.normalized.x);
        }
        if (rb2d.velocity.y != 0)
        {
            anim.SetFloat("VelocityY", rb2d.velocity.normalized.y);
        }
    }

    public override void SetSkill()
    {
        //W
        costW = 40 + 10 * levelSkillW;
        costSkillW = costW + " năng lượng";

        timeCoolDownSkillW = 12 - 1 * levelSkillW;
        //

        //R
        timeCoolDownSkillR -= 115 - levelSkillR * 15;
        //
    }

    public override void Attack()
    {
        GameObject a = Instantiate(objectAttack, transform.position, Quaternion.identity);
        a.GetComponent<ObjectAttack>().baseChamp = this;
        a.GetComponent<ObjectAttack>().target = targetAttack;
        a.GetComponent<ObjectAttack>().damage = propertyChampion.physicsDamage_Real;
    }

    public override void SkillPassive()
    {

    }

    public override void SkillQ(Vector3 vector)
    {
        if (timeCoolDownSkillQSecond <= 0 && propertyChampion.manaPointSecond >= costQ && levelSkillQ > 0)
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(InputManager.m_mousePosition);

            if (Vector2.Distance(transform.position, pos) <= propertyChampion.attackRange_Real)
            {
                if (!inUpgrade)
                {
                    if (leftKit > 0)
                    {
                        leftKit--;

                        GameObject g = Instantiate(turretQ, pos, Quaternion.identity);
                        g.GetComponent<H28GOfHeimerdinger>().c = this;
                        g.GetComponent<H28GOfHeimerdinger>().health = 125 + propertyChampion.level * 15 + 16;
                        g.GetComponent<H28GOfHeimerdinger>().damage = propertyChampion.magicDamage_Real;

                        for (int i = 2; i > 0; i--)
                        {
                            if (h28GOfHeimerdingers[i])
                            {
                                if (!h28GOfHeimerdingers[i - 1])
                                {
                                    h28GOfHeimerdingers[i - 1] = h28GOfHeimerdingers[i];
                                    h28GOfHeimerdingers[i] = null;
                                }
                            }
                        }

                        for (int i = 0; i < 3; i++)
                        {
                            if (!h28GOfHeimerdingers[i])
                            {
                                h28GOfHeimerdingers[i] = g.GetComponent<H28GOfHeimerdinger>();

                                timeCoolDownSkillQSecond = timeCoolDownSkillQ;
                                propertyChampion.manaPointSecond -= costQ;

                                return;
                            }
                        }

                        h28GOfHeimerdingers[0].Death();
                        h28GOfHeimerdingers[0] = h28GOfHeimerdingers[1];
                        h28GOfHeimerdingers[1] = h28GOfHeimerdingers[2];
                        h28GOfHeimerdingers[2] = g.GetComponent<H28GOfHeimerdinger>();

                        timeCoolDownSkillQSecond = timeCoolDownSkillQ;
                        propertyChampion.manaPointSecond -= costQ;
                    }
                }
                else
                {
                    GameObject g = Instantiate(turretQ, pos, Quaternion.identity);
                    g.GetComponent<H28GOfHeimerdinger>().health = 125 + propertyChampion.level * 15 + 16;
                    g.GetComponent<H28GOfHeimerdinger>().damage = propertyChampion.magicDamage_Real;
                    g.GetComponent<H28GOfHeimerdinger>().isUpgrade = true;

                    inUpgrade = false;
                    timeCoolDownSkillRSecond = timeCoolDownSkillR;
                    Destroy(UIManager.instance.listEffect["Skill R Heimerdinger"].gameObject);
                    UIManager.instance.listEffect.Remove("Skill R Heimerdinger");
                    SetSpriteSkillNormal();
                }
            }
        }
    }

    public override void SkillW()
    {
        if (timeCoolDownSkillWSecond <= 0 && propertyChampion.manaPointSecond >= costW && levelSkillW > 0)
        {
            propertyChampion.manaPointSecond -= costW;
            Vector3 p = Camera.main.ScreenToWorldPoint(InputManager.m_mousePosition);
            if (!inUpgrade)
            {
                StartCoroutine(SpawnW(1, p));
            }
            else
            {
                StartCoroutine(SpawnW(4, p));
                inUpgrade = false;
                timeCoolDownSkillRSecond = timeCoolDownSkillR;
                SetSpriteSkillNormal();

                Destroy(UIManager.instance.listEffect["Skill R Heimerdinger"].gameObject);
                UIManager.instance.listEffect.Remove("Skill R Heimerdinger");
            }
            SetSpriteSkillNormal();
        }
    }

    IEnumerator SpawnW(int index, Vector3 p)
    {
        bool isUpgrade = inUpgrade;
        for (int c = 0; c < index; c++)
        {
            for (int i = -2; i <= 2; i++)
            {
                GameObject newG = new GameObject();
                newG.transform.position = transform.position;
                Vector3 dir = p - transform.position;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                Vector3 q = Quaternion.AngleAxis(angle, Vector3.forward).eulerAngles;

                int rot = 0;

                if (i < 0)
                {
                    rot = -90;
                }
                else if (i > 0)
                {
                    rot = 90;
                }

                newG.transform.eulerAngles = new Vector3(0, 0, q.z + rot);

                if (rot != 0)
                {
                    newG.transform.position += newG.transform.right * Mathf.Abs(i);
                }

                GameObject g = Instantiate(prefabSkillW, newG.transform.position, Quaternion.identity);
                g.GetComponent<RocketOfHeimerdinger>().dir = p - g.transform.position;
                g.GetComponent<RocketOfHeimerdinger>().isUpgrade = isUpgrade;
                g.GetComponent<RocketOfHeimerdinger>().c = this;
                g.GetComponent<RocketOfHeimerdinger>().team = team;

                Destroy(newG);
            }

            timeCoolDownSkillWSecond = timeCoolDownSkillW;

            yield return new WaitForSeconds(.1f);
        }
    }

    public override void SkillE(Vector3 vector)
    {
        if (timeCoolDownSkillESecond <= 0 && propertyChampion.manaPointSecond >= costE && levelSkillE > 0)
        {
            Vector2 p = vector;

            if (Vector2.Distance(p, transform.position) <= 4)
            {
                GameObject g = Instantiate(prefabE, transform.position, Quaternion.identity);
                g.GetComponent<SkillEHeimerdinger>().damage = propertyChampion.magicDamage_Real;
                g.GetComponent<SkillEHeimerdinger>().target = p;
                g.GetComponent<SkillEHeimerdinger>().isUpgrade = inUpgrade;
                if (!inUpgrade)
                {
                    g.GetComponent<SkillEHeimerdinger>().intLeft = 1;
                    propertyChampion.manaPointSecond -= costE;

                }
                else
                {
                    g.GetComponent<SkillEHeimerdinger>().intLeft = 3;
                    inUpgrade = false;
                    timeCoolDownSkillRSecond = timeCoolDownSkillR;
                    SetSpriteSkillNormal();

                    Destroy(UIManager.instance.listEffect["Skill R Heimerdinger"].gameObject);
                    UIManager.instance.listEffect.Remove("Skill R Heimerdinger");
                    SetSpriteSkillNormal();
                }

                timeCoolDownSkillESecond = 0;
            }
        }
    }

    public override void SkillR(Vector3 vector)
    {
        if (timeCoolDownSkillRSecond <= 0 && propertyChampion.manaPointSecond >= 100 && levelSkillQ > 0)
        {
            if (!inUpgrade)
            {
                inUpgrade = true;
                SetSpriteSkillUpgrade();

                UIManager.instance.CreateSlotEffect("Skill R Heimerdinger", iconEffectR.GetComponent<IconEffect>());
            }
            else
            {
                inUpgrade = false;
                timeCoolDownSkillRSecond = timeCoolDownSkillR;
                SetSpriteSkillNormal();

                Destroy(UIManager.instance.listEffect["Skill R Heimerdinger"].gameObject);
                UIManager.instance.listEffect.Remove("Skill R Heimerdinger");
            }
        }
    }

    void SetSpriteSkillUpgrade()
    {
        UIManager.instance.imageSkillQBg.sprite = spriteUpgradeQ;
        UIManager.instance.imageSkillQ.sprite = spriteUpgradeQ;
        UIManager.instance.imageSkillWBg.sprite = spriteUpgradeW;
        UIManager.instance.imageSkillW.sprite = spriteUpgradeW;
        UIManager.instance.imageSkillEBg.sprite = spriteUpgradeE;
        UIManager.instance.imageSkillE.sprite = spriteUpgradeE;
    }

    void SetSpriteSkillNormal()
    {
        UIManager.instance.imageSkillQBg.sprite = spriteQ;
        UIManager.instance.imageSkillQ.sprite = spriteQ;
        UIManager.instance.imageSkillWBg.sprite = spriteW;
        UIManager.instance.imageSkillW.sprite = spriteW;
        UIManager.instance.imageSkillEBg.sprite = spriteE;
        UIManager.instance.imageSkillE.sprite = spriteE;
    }

    public override void Death()
    {
        death++;

        rb2d.velocity = Vector2.zero;
        timeLeftToRespawn = CongThuc.TimeToReSpawn(this);
        isDeath = true;
        canMove = false;
        rend.SetActive(false);
        UIManager.instance.imageAvatar.color = new Color(.3f, .3f, .3f, 1);
        UIManager.instance.barTopHealth.SetActive(false);
        state = State.Death;
    }
}
