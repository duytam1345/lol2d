using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEHeimerdinger : MonoBehaviour
{
    public Team team;

    public int intLeft;

    public Vector3 target;
    public float damage;
    public float speed;
    Vector3 dir;

    private void Start()
    {
        dir = target - transform.position;
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, target) > .01f)
        {
            Vector2 v = (target - transform.position) * speed * Time.deltaTime;
            transform.Translate(v);
        }
        else
        {
            Explode();
        }
    }

    void Explode()
    {
        CircleCollider2D circleCollider2D = GetComponent<CircleCollider2D>();

        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(circleCollider2D.transform.position, circleCollider2D.radius);
        foreach (var item in collider2Ds)
        {
            if (item.GetComponent<Creep>() && item.GetComponent<Creep>().team != team)
            {
                item.GetComponent<Creep>().TakeDamage(gameObject, (int)damage);
                item.gameObject.AddComponent<StunCC>();
                item.gameObject.GetComponent<StunCC>().team = team;
                item.gameObject.GetComponent<StunCC>().timeLeft = 1.5f;
            }
        }

        intLeft--;

        if (intLeft > 0)
        {
            GameObject g = Instantiate(gameObject, transform.position, Quaternion.identity);
            g.GetComponent<SkillEHeimerdinger>().damage = damage;
            g.GetComponent<SkillEHeimerdinger>().target = transform.position + dir;
            g.GetComponent<SkillEHeimerdinger>().intLeft = intLeft;

            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
