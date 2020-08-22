using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletOfHeimerdingerBeam : MonoBehaviour
{
    public Vector3 dir;
    public float speed;
    public float damage;

    Vector2 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, startPos) <= 6)
        {
            transform.position = Vector2.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Creep>())
        {
            collision.GetComponent<Creep>().TakeDamage(gameObject, (int)damage);
        }
    }
}
