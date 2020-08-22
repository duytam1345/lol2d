using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeimerdingerChampion : Champion
{
    public GameObject objectAttack;

    [Header("Chiêu Q")]
    public Sprite spriteUpgradeQ;
    public Sprite spriteQ;
    public GameObject turretQ;
    public int costQ;
    public List<H28GOfHeimerdinger> h28GOfHeimerdingers;
    public float timeCooldownKit;
    public int leftKit;
    public GameObject iconEffectQ;


    [Header("Chiêu W")]
    public Sprite spriteUpgradeW;
    public Sprite spriteW;
    public GameObject prefabSkillW;
    public int costW;

    [Header("Chiêu E")]
    public Sprite spriteUpgradeE;
    public Sprite spriteE;
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

        UIManager.instace.CreateSlotEffect("Skill Q Heimerdinger", GetComponent<IconEffect>());

        h28GOfHeimerdingers.Add(null);
        h28GOfHeimerdingers.Add(null);
        h28GOfHeimerdingers.Add(null);
    }

    void Update()
    {
        if (isDeath)
        {
            timeLeftToRespawn -= Time.deltaTime;
            UIManager.instace.imageAvatar.transform.GetChild(0).GetComponent<Text>().text = ((int)timeLeftToRespawn).ToString();
            if (timeLeftToRespawn <= 0)
            {
                UIManager.instace.imageAvatar.transform.GetChild(0).GetComponent<Text>().text = "";
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

    public override void SkillQ()
    {
        if (timeCoolDownSkillQSecond <= 0 && propertyChampion.manaPointSecond >= costQ)
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
                    Destroy(UIManager.instace.listEffect["Skill R Heimerdinger"].gameObject);
                    UIManager.instace.listEffect.Remove("Skill R Heimerdinger");
                    SetSpriteSkillNormal();
                }
            }
        }
    }

    public override void SkillW()
    {
        if (timeCoolDownSkillWSecond <= 0 && propertyChampion.manaPointSecond >= costW)
        {
            propertyChampion.manaPointSecond -= costW;
            if (!inUpgrade)
            {
                StartCoroutine(SpawnW(1));
            }
            else
            {
                StartCoroutine(SpawnW(4));
                inUpgrade = false;
                timeCoolDownSkillRSecond = timeCoolDownSkillR;
                SetSpriteSkillNormal();

                Destroy(UIManager.instace.listEffect["Skill R Heimerdinger"].gameObject);
                UIManager.instace.listEffect.Remove("Skill R Heimerdinger");
            }
            SetSpriteSkillNormal();
        }
    }

    IEnumerator SpawnW(int index)
    {
        for (int c = 0; c < index; c++)
        {
            Vector3 p = Camera.main.ScreenToWorldPoint(InputManager.m_mousePosition);

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
                g.GetComponent<RocketOfHeimerdinger>().dmg = propertyChampion.physicsDamage_Real;
                g.GetComponent<RocketOfHeimerdinger>().team = team;

                Destroy(newG);
            }

            timeCoolDownSkillWSecond = timeCoolDownSkillW;

            yield return new WaitForSeconds(.1f);
        }
    }

    public override void SkillE()
    {
        if (timeCoolDownSkillESecond <= 0 && propertyChampion.manaPointSecond >= costE)
        {
            Vector2 p = Camera.main.ScreenToWorldPoint(InputManager.m_mousePosition);

            if (Vector2.Distance(p, transform.position) <= 4)
            {
                GameObject g = Instantiate(prefabE, transform.position, Quaternion.identity);
                g.GetComponent<SkillEHeimerdinger>().damage = propertyChampion.magicDamage_Real;
                g.GetComponent<SkillEHeimerdinger>().target = p;
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

                    Destroy(UIManager.instace.listEffect["Skill R Heimerdinger"].gameObject);
                    UIManager.instace.listEffect.Remove("Skill R Heimerdinger");
                    SetSpriteSkillNormal();
                }

                timeCoolDownSkillESecond = 0;
            }
        }
    }

    public override void SkillR()
    {
        if (timeCoolDownSkillRSecond <= 0)
        {
            if (!inUpgrade)
            {
                inUpgrade = true;
                SetSpriteSkillUpgrade();

                UIManager.instace.CreateSlotEffect("Skill R Heimerdinger", iconEffectR.GetComponent<IconEffect>());
            }
            else
            {
                inUpgrade = false;
                timeCoolDownSkillRSecond = timeCoolDownSkillR;
                SetSpriteSkillNormal();

                Destroy(UIManager.instace.listEffect["Skill R Heimerdinger"].gameObject);
                UIManager.instace.listEffect.Remove("Skill R Heimerdinger");
            }
        }
    }

    void SetSpriteSkillUpgrade()
    {
        UIManager.instace.imageSkillQBg.sprite = spriteUpgradeQ;
        UIManager.instace.imageSkillQ.sprite = spriteUpgradeQ;
        UIManager.instace.imageSkillWBg.sprite = spriteUpgradeW;
        UIManager.instace.imageSkillW.sprite = spriteUpgradeW;
        UIManager.instace.imageSkillEBg.sprite = spriteUpgradeE;
        UIManager.instace.imageSkillE.sprite = spriteUpgradeE;
    }

    void SetSpriteSkillNormal()
    {
        UIManager.instace.imageSkillQBg.sprite = spriteQ;
        UIManager.instace.imageSkillQ.sprite = spriteQ;
        UIManager.instace.imageSkillWBg.sprite = spriteW;
        UIManager.instace.imageSkillW.sprite = spriteW;
        UIManager.instace.imageSkillEBg.sprite = spriteE;
        UIManager.instace.imageSkillE.sprite = spriteE;
    }

    public override void Death()
    {
        rb2d.velocity = Vector2.zero;
        timeLeftToRespawn = CongThuc.TimeToReSpawn(this);
        isDeath = true;
        canMove = false;
        rend.SetActive(false);
        UIManager.instace.imageAvatar.color = new Color(.3f, .3f, .3f, 1);
        UIManager.instace.barTopHealth.SetActive(false);
        state = State.Death;
    }
}
