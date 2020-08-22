using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketOfHeimerdinger : MonoBehaviour
{
    public Team team;

    public Vector2 dir;
    public float dmg;
    public float speed;

    Vector2 startPos;

    private void Start()
    {
        Vector3 p = Camera.main.ScreenToWorldPoint(InputManager.m_mousePosition);
        dir = p - transform.position;

        startPos = transform.position;

        Vector3 direction = transform.position + (Vector3)dir - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Vector3 q = Quaternion.AngleAxis(angle, Vector3.forward).eulerAngles;
        transform.eulerAngles = new Vector3(0, 0, q.z - 90);
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
                item.GetComponent<Creep>().TakeDamage(gameObject, 50);
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
