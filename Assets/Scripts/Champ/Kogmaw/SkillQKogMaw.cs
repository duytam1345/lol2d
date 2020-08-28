using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillQKogMaw : MonoBehaviour
{
    public Team team;
    public Champion c;
    public Vector2 dir;
    public float dmg;
    public float speed;

    Vector2 startPos;

    private void Start()
    {
        startPos = transform.position;

        Vector3 direction = transform.position + (Vector3)dir - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Vector3 q = Quaternion.AngleAxis(angle, Vector3.forward).eulerAngles;
        transform.eulerAngles = new Vector3(0, 0, q.z + 90);
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, (Vector2)transform.position + dir, speed * Time.deltaTime);

        BoxCollider2D box = GetComponent<BoxCollider2D>();
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(box.transform.position, box.size, 0);

        foreach (var item in collider2Ds)
        {
            if (item.GetComponent<Creep>() && item.GetComponent<Creep>().team != team)
            {
                //Sound
                GameObject s = Instantiate(Resources.Load("Kog'Maw/Kog'Maw QHit")) as GameObject;

                item.GetComponent<Creep>().TakeDamage(c.gameObject, (int)dmg);
                Destroy(gameObject);
                return;
            }
            else if (item.GetComponent<Champion>() && item.GetComponent<Champion>().team != team)
            {
                //Sound
                GameObject s = Instantiate(Resources.Load("Kog'Maw/Kog'Maw QHit")) as GameObject;

                item.GetComponent<Champion>().TakeDamage(c.gameObject, (int)dmg, item.transform.position);
                Destroy(gameObject);
                return;
            }
        }

        if (Vector2.Distance(transform.position, startPos) >= 7)
        {
            Destroy(gameObject);
        }
    }
}
