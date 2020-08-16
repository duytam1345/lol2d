using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDamageToEnemy : MonoBehaviour
{
    [SerializeField]
    bool damageCreep;
    [SerializeField]
    bool damageChampion;
    [SerializeField]
    bool penetration;

    [SerializeField]
    int damage;

    public Vector2 dir;

    [SerializeField]
    float speed;

    [SerializeField]
    int range;

    Vector2 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, (Vector2)transform.position + dir, speed * Time.deltaTime);

        if(Vector2.Distance(transform.position, startPos) >= range)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (damageCreep && collision.GetComponent<Creep>() && collision.GetComponent<Creep>().team != FindObjectOfType<Charater>().champion.team)
        {
            collision.GetComponent<Creep>().TakeDamage(FindObjectOfType<Charater>().gameObject, damage);

            if (!penetration)
            {
                Destroy(gameObject);
            }
        }
    }
}
