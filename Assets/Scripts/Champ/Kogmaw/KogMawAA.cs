using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KogMawAA : MonoBehaviour
{
    public GameObject effectHit;

    public Champion baseChamp;
    public GameObject target;

    public float damage;
    public int critRate;
    public int lifeSteel;
    public float speed;

    public bool onW;

    private void Update()
    {
        if (target)
        {
            if (transform.position != target.transform.position)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            }
            else
            {
                Damage();
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Damage()
    {
        if (target.GetComponent<Creep>())
        {
            float dmg = damage;

            int r = Random.Range(0, 100);
            if (r <= critRate)
            {
                dmg *= 2;
            }

            if (onW)
            {
                dmg += (int)(target.GetComponent<Creep>().property.healthPoint / 100 *
                    ((1.25f+(.75*baseChamp.levelSkillW))+baseChamp.propertyChampion.magicDamage_Real/100));
            }

            int d = target.GetComponent<Creep>().TakeDamage(baseChamp.gameObject, (int)dmg);
            baseChamp.TakeHealth(d / 100 * lifeSteel);
        }
        else if (target.GetComponent<Turret>())
        {
            target.GetComponent<Turret>().TakeDamage(baseChamp.gameObject, (int)damage);
            UIManager.instance.MakeTextDamage(target.transform.position, damage.ToString());
        }
        else if (target.GetComponent<Champion>())
        {
            target.GetComponent<Champion>().TakeDamage(baseChamp.gameObject, (int)damage,target.transform.position);
            UIManager.instance.MakeTextDamage(target.transform.position, damage.ToString());
        }


        Vector2 pos = new Vector2(
            Random.Range(target.transform.position.x - .5f, target.transform.position.x + .5f),
            Random.Range(target.transform.position.y - .5f, target.transform.position.y + .5f));

        Vector3 rot = new Vector3(0, 0, Random.Range(0, 360));

        GameObject e = Instantiate(effectHit, pos, Quaternion.Euler(rot.x, rot.y, rot.z));

        Destroy(gameObject);
    }
}
