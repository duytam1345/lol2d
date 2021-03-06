﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KogMawChampion : Champion
{
    public GameObject objectAttack;
    public bool isPassive;
    public float timePassive;

    [Header("Chiêu Q")]
    public GameObject prefabSkillQ;
    public int costQ;

    [Header("Chiêu W")]
    public GameObject iconEffectW;
    public int costW;
    public float timeEffectW;
    public bool onW;

    [Header("Chiêu E")]
    public int costE;
    public GameObject prefabSkillE;

    [Header("Chiêu R")]
    public int costR;
    public GameObject prefabSkillR;

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

    public override void Start()
    {
        base.Start();
        propertyChampion.attackSpeedExtra.Add("PassiveQ", 0);
        LevelUp();
    }

    public override void Attack()
    {
        //int r = Random.Range(0, 3);
        //if (r == 0)
        //{
        //    GameObject g = Instantiate(Resources.Load("Kog'Maw/Kog'Maw Move") as GameObject);
        //}


        //Sound
        GameObject s = Instantiate(Resources.Load("Kog'Maw/Kog'Maw AA")) as GameObject;

        GameObject a = Instantiate(objectAttack, transform.position, Quaternion.identity);
        a.GetComponent<KogMawAA>().baseChamp = this;
        a.GetComponent<KogMawAA>().target = targetAttack;
        a.GetComponent<KogMawAA>().damage = propertyChampion.physicsDamage_Real;
        a.GetComponent<KogMawAA>().critRate = propertyChampion.critRate_Real;
        a.GetComponent<KogMawAA>().lifeSteel = propertyChampion.lifeSteel_Real;
        a.GetComponent<KogMawAA>().onW = onW;
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

        if (Input.GetKeyDown(KeyCode.U) && !isBot)
        {
            LevelUp();
        }

        PerSecond();

        if (isPassive)
        {
            timePassive -= Time.deltaTime;
            if (timePassive <= 0)
            {
                ExplodeDeath();
            }
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

        if (onW)
        {
            timeEffectW -= Time.deltaTime;
            if (timeEffectW <= 0)
            {
                onW = false;

                propertyChampion.attackRangeExtra.Remove("Skill W");
                propertyChampion.UpdateValue();
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

    public override void SetSkill()
    {
        //Q
        int a = 0;
        if (levelSkillQ > 0)
        {
            a = 10 + (levelSkillQ * 5);
        }

        propertyChampion.attackSpeedExtra["PassiveQ"] = a;
        propertyChampion.UpdateValue();
        //

        //E
        costE = 70 + 10 * levelSkillE;
        costSkillE = costE + " năng lượng";
        //

        //R
        timeCoolDownSkillR = 2.5f - .5f * levelSkillR;
        //
    }

    public override void SkillQ(Vector3 vector)
    {
        if (timeCoolDownSkillQSecond <= 0 && propertyChampion.manaPointSecond >= costW && levelSkillQ > 0)
        {
            //Sound
            GameObject s = Instantiate(Resources.Load("Kog'Maw/Kog'Maw Q")) as GameObject;

            GameObject g = Instantiate(prefabSkillQ, transform.position, Quaternion.identity);
            g.GetComponent<SkillQKogMaw>().dmg = (40 + (50 * levelSkillQ)) + propertyChampion.magicDamage_Real / 100 * 70;
            g.GetComponent<SkillQKogMaw>().c = this;
            g.GetComponent<SkillQKogMaw>().team = team;
            g.GetComponent<SkillQKogMaw>().dir = vector - transform.position;


            propertyChampion.manaPointSecond -= costQ;
            timeCoolDownSkillQSecond = timeCoolDownSkillQ;
        }
    }

    public override void SkillW()
    {
        if (timeCoolDownSkillWSecond <= 0 && propertyChampion.manaPointSecond >= costW && levelSkillW > 0)
        {
            //Sound
            GameObject s = Instantiate(Resources.Load("Kog'Maw/Kog'Maw W")) as GameObject;

            GameObject icon = Instantiate(iconEffectW);
            UIManager.instance.CreateSlotEffect("Skill W KogMaw", icon.GetComponent<IconEffect>());

            timeEffectW = 8;
            onW = true;

            propertyChampion.attackRangeExtra.Add("Skill W", 1);
            propertyChampion.UpdateValue();

            timeCoolDownSkillWSecond = timeCoolDownSkillW;
            propertyChampion.manaPointSecond -= costW;
        }
    }

    public override void SkillE(Vector3 vector)
    {
        if (timeCoolDownSkillESecond <= 0 && propertyChampion.manaPointSecond >= costE && levelSkillE > 0)
        {
            //Sound
            GameObject s = Instantiate(Resources.Load("Kog'Maw/Kog'Maw E")) as GameObject;

            GameObject g = Instantiate(prefabSkillE, transform.position, Quaternion.identity);
            g.GetComponent<SkillEKogMaw>().baseChamp = this;
            g.GetComponent<SkillEKogMaw>().magicDamage = propertyChampion.magicDamage_Real;
            g.GetComponent<SkillEKogMaw>().team = team;

            Vector3 dir = vector - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Vector3 q = Quaternion.AngleAxis(angle, Vector3.forward).eulerAngles;
            g.transform.eulerAngles = new Vector3(0, 0, q.z - 90);

            timeCoolDownSkillESecond = timeCoolDownSkillE;
            propertyChampion.manaPointSecond -= costE;
        }
    }

    public override void SkillR(Vector3 vector)
    {
        if (timeCoolDownSkillRSecond <= 0 && propertyChampion.manaPointSecond >= costR && levelSkillR > 0)
        {
            //Sound
            GameObject s = Instantiate(Resources.Load("Kog'Maw/Kog'Maw R")) as GameObject;

            Vector2 v = vector;
            GameObject g = Instantiate(prefabSkillR, v, Quaternion.identity);
            g.GetComponent<SkillRKogMaw>().team = team;
            g.GetComponent<SkillRKogMaw>().c = this;
            g.GetComponent<SkillRKogMaw>().physicsDamage = propertyChampion.physicsDamage_Real;
            g.GetComponent<SkillRKogMaw>().magicDamage = propertyChampion.magicDamage_Real;

            propertyChampion.manaPointSecond -= costR;
            timeCoolDownSkillRSecond = timeCoolDownSkillR;
        }
    }

    public override void Death()
    {
        death++;
        //UIManager.instace.imageAvatar.color = new Color(.3f, .3f, .3f, 1);
        //UIManager.instace.barTopHealth.SetActive(false);
        //canAttack = false;
        //state = State.Death;
        //timePassive = 4;
        //isPassive = true;
        ExplodeDeath();
    }

    public void ExplodeDeath()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(transform.position, 5);
        foreach (var item in collider2Ds)
        {
            if (item.GetComponent<Creep>() && item.GetComponent<Creep>().team != team)
            {
                item.GetComponent<Creep>().TakeDamage(gameObject, 100 + (25 * propertyChampion.level));
            }
        }

        state = State.Death;
        rb2d.velocity = Vector2.zero;
        canMove = false;
        isPassive = false;
        timeLeftToRespawn = CongThuc.TimeToReSpawn(this);
        isDeath = true;
        rend.SetActive(false);
    }
}
