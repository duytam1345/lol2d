using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MundoChampion : Champion
{

    [Header("Nội Tại")]
    public float percentHealth;

    [Header("Chiêu Q")]
    public int costQ;
    public GameObject prefabSkillQ;

    [Header("Chiêu W")]
    public int costW;
    public GameObject effectSkillW;
    public float timeEffectW;
    public bool OnW;

    [Header("Chiêu E")]
    public int costE;
    public int amountDamageE;
    public float timeEffectE;
    public float timeEffectESecond;
    public bool OnE;

    [Header("Chiêu R")]
    public int costR;
    public int timeEffectR;
    public float timeEffectRSecond;
    public bool OnR;

    public override void Start()
    {
        base.Start();
        LevelUp();
    }

    public override void SetSkill()
    {
        //Q
        costQ = 40 + 10 * levelSkillQ;
        costSkillQ = costQ + " máu";
        //

        //W
        costW = 5 + 5 * levelSkillW;
        costSkillW = costW + " máu";
        //

        //E
        costE = 15 + 10 * levelSkillE;
        costSkillE = costE + " máu";

        timeCoolDownSkillE = 6.5f - .5f * levelSkillE;
        //

        //R
        timeCoolDownSkillR = 120 - 10 * levelSkillR;
        //
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

    public override void Attack()
    {
        if (targetAttack.GetComponent<Creep>())
        {
            targetAttack.GetComponent<Creep>().TakeDamage(gameObject, propertyChampion.physicsDamage_Real);
            UIManager.instance.MakeTextDamage(targetAttack.transform.position, propertyChampion.physicsDamage_Real.ToString());
        }
        else if (targetAttack.GetComponent<Turret>())
        {
            targetAttack.GetComponent<Turret>().TakeDamage(gameObject, propertyChampion.physicsDamage_Real);
            UIManager.instance.MakeTextDamage(targetAttack.transform.position, propertyChampion.physicsDamage_Real.ToString());
        }

        attackSpeedSecond = 1 / propertyChampion.attackSpeed_Real;

        Vector2 pos = new Vector2(
            Random.Range(targetAttack.transform.position.x - .5f, targetAttack.transform.position.x + .5f),
            Random.Range(targetAttack.transform.position.y - .5f, targetAttack.transform.position.y + .5f));

        Vector3 rot = new Vector3(0, 0, Random.Range(0, 360));

        GameObject e = Instantiate(prefabHit, pos, Quaternion.Euler(rot.x, rot.y, rot.z));
    }

    private void Update()
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
        if (Input.GetKeyDown(KeyCode.U))
        {
            LevelUp();
        }

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

        if (timeCoolDownSkillQSecond > 0)
        {
            timeCoolDownSkillQSecond -= Time.deltaTime;
        }
        if (timeCoolDownSkillWSecond > 0)
        {
            timeCoolDownSkillWSecond -= Time.deltaTime;
        }
        if (timeCoolDownSkillESecond > 0 && !OnE)
        {
            timeCoolDownSkillESecond -= Time.deltaTime;
        }
        if (timeCoolDownSkillRSecond > 0)
        {
            timeCoolDownSkillRSecond -= Time.deltaTime;
        }

        if (OnW)
        {
            if (timeEffectW <= 0)
            {
                timeEffectW = 1;

                propertyChampion.healthPointSecond -= costW;
                if (propertyChampion.healthPointSecond <= costW)
                {
                    OnW = false;
                    if (GameObject.Find("Effect Skill W Mundo"))
                    {
                        Destroy(GameObject.Find("Effect Skill W Mundo"));
                    }
                }

                float amount = 6.25f + (3.75f * levelSkillW) + propertyChampion.magicDamage_Real / 100 * 5;

                Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(transform.position, 2);
                foreach (var item in collider2Ds)
                {
                    if (item.GetComponent<Creep>() && item.GetComponent<Creep>().team != team)
                    {
                        item.GetComponent<Creep>().TakeDamage(gameObject, (int)amount);
                        UIManager.instance.MakeTextDamage(transform.position, amount.ToString());
                    }
                }

            }
            timeEffectW -= Time.deltaTime;
        }

        if (OnE)
        {
            timeEffectESecond -= Time.deltaTime;

            if (timeEffectESecond <= 0)
            {
                OnE = false;
                timeEffectESecond = timeEffectE;

                propertyChampion.physicsDamageExtra.Remove("Skill E MunDo");
                propertyChampion.UpdateValue();
            }
        }

        if (OnR)
        {
            if (timeEffectRSecond <= 0)
            {
                timeEffectRSecond = 1;

                int amount = (int)(propertyChampion.healthPoint_Real / 100 * 25 + (25 * levelSkillR)) / 12;

                propertyChampion.healthPointSecond += amount;

                UIManager.instance.MakeTextHeal(transform.position, amount.ToString());

                timeEffectR -= 1;
                if (timeEffectR <= 0)
                {
                    OnR = false;
                    timeEffectR = 12;
                }
            }
            timeEffectRSecond -= Time.deltaTime;
        }
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

    public override void SkillPassive()
    {
        propertyChampion.healthRegenExtra.Remove("Passive");
        float amount = (propertyChampion.healthPoint_Real / 100) * percentHealth;
        propertyChampion.healthRegenExtra.Add("Passive", amount);
        propertyChampion.UpdateValue();
    }

    public override void SkillQ(Vector3 vector)
    {
        if (timeCoolDownSkillQSecond <= 0 && propertyChampion.healthPointSecond > costQ && levelSkillQ > 0)
        {
            GameObject g = Instantiate(prefabSkillQ, transform.position, Quaternion.identity);
            g.GetComponent<SkillQMundo>().dmg = (17.5f + (2.5f * levelSkillQ));
            g.GetComponent<SkillQMundo>().c = this;
            g.GetComponent<SkillQMundo>().team = team;

            propertyChampion.healthPointSecond -= costQ;
            timeCoolDownSkillQSecond = timeCoolDownSkillQ;
        }
    }
    public override void SkillW()
    {
        if (timeCoolDownSkillWSecond <= 0 && propertyChampion.healthPointSecond > costW && levelSkillW > 0)
        {
            OnW = !OnW;
            timeCoolDownSkillWSecond = timeCoolDownSkillW;

            if (!OnW)
            {
                if (GameObject.Find("Effect Skill W Mundo"))
                {
                    Destroy(GameObject.Find("Effect Skill W Mundo"));
                }
            }
            else
            {
                GameObject g = Instantiate(effectSkillW, transform.position, Quaternion.identity, transform);
                g.name = "Effect Skill W Mundo";
            }
        }
    }
    public override void SkillE(Vector3 vector)
    {
        if (timeCoolDownSkillESecond <= 0 && propertyChampion.healthPointSecond > costE && levelSkillE > 0)
        {
            OnE = true;

            propertyChampion.healthPointSecond -= costE;
            timeCoolDownSkillESecond = timeCoolDownSkillE;

            propertyChampion.physicsDamageExtra.Add("Skill E MunDo", 25 + 15 * levelSkillE);
            propertyChampion.UpdateValue();

            attackSpeedSecond = 0;
        }
    }
    public override void SkillR(Vector3 vector)
    {
        if (timeCoolDownSkillRSecond <= 0 && propertyChampion.healthPoint > costR && levelSkillR > 0)
        {
            propertyChampion.healthPointSecond -= (propertyChampion.healthPointSecond / 100 * 25);
            timeCoolDownSkillRSecond = timeCoolDownSkillR;

            OnR = true;
        }
    }
}
