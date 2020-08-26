using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAttack : MonoBehaviour
{
    public GameObject effectHit;

    public Champion baseChamp;
    public GameObject target;

    public float damage;
    public float speed;

    private void Update()
    {
        transform.Rotate(Vector3.forward * 10);

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
            target.GetComponent<Creep>().TakeDamage(baseChamp.gameObject, (int)damage);
        }
        else if (target.GetComponent<Turret>())
        {
            target.GetComponent<Turret>().TakeDamage(baseChamp.gameObject, (int)damage);
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
