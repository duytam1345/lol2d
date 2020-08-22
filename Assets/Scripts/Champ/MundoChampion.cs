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
    public int amountDamageW;
    public int costW;
    public GameObject effectSkillW;
    public CircleCollider2D circleDetectW;
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
    public int amountHeal;
    public bool OnR;

    public override void Attack()
    {
        if (targetAttack.GetComponent<Creep>())
        {
            targetAttack.GetComponent<Creep>().TakeDamage(gameObject, propertyChampion.physicsDamage_Real);
            UIManager.instace.MakeTextDamage(targetAttack.transform.position, propertyChampion.physicsDamage_Real.ToString());
        }
        else if (targetAttack.GetComponent<Turret>())
        {
            targetAttack.GetComponent<Turret>().TakeDamage(gameObject, propertyChampion.physicsDamage_Real);
            UIManager.instace.MakeTextDamage(targetAttack.transform.position, propertyChampion.physicsDamage_Real.ToString());
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
                    effectSkillW.SetActive(false);
                }

                int amount = amountDamageW;

                Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(transform.position, circleDetectW.radius);
                foreach (var item in collider2Ds)
                {
                    if (item.GetComponent<Creep>() && item.GetComponent<Creep>().team != team)
                    {
                        item.GetComponent<Creep>().TakeDamage(gameObject, amount);
                        UIManager.instace.MakeTextDamage(transform.position, amount.ToString());
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

                propertyChampion.physicsDamageExtra.Remove("SkillE");
                propertyChampion.UpdateValue();
            }
        }

        if (OnR)
        {
            if (timeEffectRSecond <= 0)
            {
                timeEffectRSecond = 1;

                int amount = (int)(propertyChampion.healthPoint_Real / 100 * amountHeal) / 12;

                propertyChampion.healthPointSecond += amount;

                UIManager.instace.MakeTextHeal(transform.position, amount.ToString());

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

    public override void SkillPassive()
    {
        propertyChampion.healthRegenExtra.Remove("Passive");
        float amount = (propertyChampion.healthPoint_Real / 100) * percentHealth;
        propertyChampion.healthRegenExtra.Add("Passive", amount);
        propertyChampion.UpdateValue();
    }

    public override void SkillQ()
    {
        if (timeCoolDownSkillQSecond <= 0 && propertyChampion.healthPointSecond > costQ)
        {
            GameObject gS = Instantiate(prefabSkillQ, transform.position, Quaternion.identity);
            Vector2 dir = (Camera.main.ScreenToWorldPoint(InputManager.m_mousePosition)) - transform.position;
            gS.GetComponent<BoxDamageToEnemy>().dir = dir.normalized;

            timeCoolDownSkillQSecond = timeCoolDownSkillQ;

            propertyChampion.healthPointSecond -= costQ;
        }
    }
    public override void SkillW()
    {
        if (timeCoolDownSkillWSecond <= 0)
        {
            OnW = !OnW;
            timeCoolDownSkillWSecond = timeCoolDownSkillW;

            if (!OnW)
            {
                effectSkillW.SetActive(false);
            }
            else
            {
                effectSkillW.SetActive(true);
            }
        }
    }
    public override void SkillE()
    {
        if (timeCoolDownSkillESecond <= 0 && propertyChampion.healthPointSecond > costE)
        {
            OnE = true;

            propertyChampion.healthPointSecond -= costE;
            timeCoolDownSkillESecond = timeCoolDownSkillE;

            propertyChampion.physicsDamageExtra.Add("SkillE", amountDamageE);
            propertyChampion.UpdateValue();
        }
    }
    public override void SkillR()
    {
        if (timeCoolDownSkillRSecond <= 0 && propertyChampion.healthPoint > costR)
        {
            propertyChampion.healthPointSecond -= (propertyChampion.healthPointSecond / 100 * 25);
            timeCoolDownSkillRSecond = timeCoolDownSkillR;

            OnR = true;
        }
    }
}
