using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillRKogMaw : MonoBehaviour
{
    public Team team;
    public float physicsDamage;
    public float magicDamage;
    public GameObject sprite;

    Vector3 target;

    public float speed;

    private void Start()
    {
        target = transform.position;
    }

    private void Update()
    {
        if (sprite.transform.position != transform.position)
        {
            sprite.transform.position = Vector3.MoveTowards(sprite.transform.position, transform.position, speed * Time.deltaTime);
        }
        else
        {
            Damage();
        }
    }


    void Damage()
    {
        BoxCollider2D box = GetComponent<BoxCollider2D>();
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(box.transform.position, box.size, 0);
        foreach (var item in collider2Ds)
        {
            if (item.GetComponent<Creep>() && item.GetComponent<Creep>().team != team)
            {
                float percentMissingHeal =
                    (item.GetComponent<Creep>().property.healthPoint - item.GetComponent<Creep>().currentHealth)
                   * 100 / item.GetComponent<Creep>().property.healthPoint;

                float damage = 0;

                if (percentMissingHeal > 60)
                {
                    damage = 200 + (physicsDamage / 100 * 130) + (magicDamage / 100 * 70);
                }
                else
                {
                    float increamentPercent = percentMissingHeal * 60 / 50;
                    damage = (100 + (100 / 100 * increamentPercent)) +
                        physicsDamage / 100 * (65 + (65 / 100 * increamentPercent)) +
                        magicDamage / 100 * (35 + (35 / 100 * increamentPercent));
                }

                item.GetComponent<Creep>().TakeDamage(gameObject, (int)damage);
            }
        }

        Destroy(gameObject);
    }
}
