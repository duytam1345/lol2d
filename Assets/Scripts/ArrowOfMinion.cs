using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowOfMinion : MonoBehaviour
{
    public GameObject g;

    [SerializeField]
    float speed;

    public float damage;

    public GameObject target;

    private void Start()
    {
        if (transform.position.x > target.transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    void Update()
    {
        if (target)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, target.transform.position) <= 1)
            {
                if (target.tag == "Player")
                {
                    target.GetComponent<Charater>().TakeDamage(g,(int)damage, target.transform.position);

                    Destroy(gameObject);
                }
                else if (target.tag == "Minion")
                {
                    target.GetComponent<Creep>().TakeDamage(g, (int)damage);

                    Vector2 rectPos = target.transform.position;
                    rectPos = new Vector2(
                        (float)Random.Range(rectPos.x - .5f, rectPos.x + .5f),
                        (float)Random.Range(rectPos.y - .5f, rectPos.y + .5f));

                    UIManager.instace.MakeTextDamage(rectPos, damage.ToString());
                    Destroy(gameObject);
                }
                else if (target.tag == "Turret")
                {
                    target.GetComponent<Turret>().TakeDamage(g, (int)damage);

                    Vector2 rectPos = target.transform.position;
                    rectPos = new Vector2(
                        (float)Random.Range(rectPos.x - .5f, rectPos.x + .5f),
                        (float)Random.Range(rectPos.y - .5f, rectPos.y + .5f));

                    UIManager.instace.MakeTextDamage(rectPos, damage.ToString());
                    Destroy(gameObject);
                }
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
