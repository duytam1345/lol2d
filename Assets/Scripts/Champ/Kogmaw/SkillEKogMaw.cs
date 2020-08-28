using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEKogMaw : MonoBehaviour
{
    public Champion baseChamp;
    public Team team;
    public Transform child;
    public float magicDamage;

    private void Start()
    {
        child = transform.GetChild(0);
    }

    private void Update()
    {
        if (transform.localScale.y < 2)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y + Time.deltaTime * 8, transform.localScale.z);
        }
        else if (transform.localScale.y >= 2 && transform.localScale.y < 3)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y + Time.deltaTime * 6, transform.localScale.z);
        }
        else if (transform.localScale.y >= 3 && transform.localScale.y < 4)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y + Time.deltaTime * 4, transform.localScale.z);
        }
        else if (transform.localScale.y >= 4 && transform.localScale.y < 5)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y + Time.deltaTime * 2, transform.localScale.z);
        }
        else if (transform.localScale.y >= 5)
        {
            Fade();
        }
    }

    void Fade()
    {
        Color c = child.GetComponent<SpriteRenderer>().color;
        if (c.a > 0)
        {
            c.a -= Time.deltaTime * .4f;
        }
        child.GetComponent<SpriteRenderer>().color = c;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Creep>() && collision.GetComponent<Creep>().team != team)
        {
            if (transform.localScale.y < 5)
            {
                collision.GetComponent<Creep>().TakeDamage(baseChamp.gameObject, (int)((30+(45*baseChamp.levelSkillE)) + (magicDamage / 100 * 50)));
            }
        }
        else if (collision.GetComponent<Champion>() && collision.GetComponent<Champion>().team != team)
        {
            if (transform.localScale.y < 5)
            {
                collision.GetComponent<Champion>().TakeDamage(baseChamp.gameObject, (int)((30 + (45 * baseChamp.levelSkillE)) + (magicDamage / 100 * 50)), collision.transform.position);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Creep>() && collision.GetComponent<Creep>().team != team)
        {
            bool exsist = false;
            foreach (var item in collision.GetComponent<Creep>().property.moveSpeedExtra.Keys)
            {
                if (item == "Skill E KogMaw")
                {
                    exsist = true;
                    collision.GetComponent<Creep>().property.moveSpeedExtra[item].timeLeft = .25f;
                }
            }

            if (!exsist)
            {
                float valueSlow = collision.GetComponent<Creep>().property.moveSpeedCurrent / 100 * (12+(8*baseChamp.levelSkillE));

                ValueExtra valueExtra = new ValueExtra();
                valueExtra.timeLeft = .25f;
                valueExtra.value = -(valueSlow);
                collision.GetComponent<Creep>().property.moveSpeedExtra.Add("Skill E KogMaw", valueExtra);
            }
        }
        //else if (collision.GetComponent<Champion>() && collision.GetComponent<Champion>().team != team)
        //{
        //    bool exsist = false;
        //    foreach (var item in collision.GetComponent<Champion>().propertyChampion.moveSpeedExtra.Keys)
        //    {
        //        if (item == "Skill E KogMaw")
        //        {
        //            exsist = true;
        //            collision.GetComponent<Champion>().propertyChampion.moveSpeedExtra[item].timeLeft = .25f;
        //        }
        //    }

        //    if (!exsist)
        //    {
        //        float valueSlow = collision.GetComponent<Champion>().propertyChampion.moveSpeed / 100 * (12 + (8 * baseChamp.levelSkillE));

        //        ValueExtra valueExtra = new ValueExtra();
        //        valueExtra.timeLeft = .25f;
        //        valueExtra.value = -(valueSlow);
        //        collision.GetComponent<Champion>().propertyChampion.moveSpeedExtra.Add("Skill E KogMaw", valueExtra);
        //    }
        //}
    }
}
