using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletOfHeimerdinger : MonoBehaviour
{
    public GameObject target;
    public float speed;
    public float dmg;

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
                Dmg();
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Dmg()
    {
        if (target)
        {
            if (target.GetComponent<Creep>())
            {
                target.GetComponent<Creep>().TakeDamage(gameObject, (int)dmg);
            }
        }

        Destroy(gameObject);
    }
}
