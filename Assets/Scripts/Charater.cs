using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charater : MonoBehaviour
{
    public float speed;

    [SerializeField]
    Rigidbody2D rb2d;

    [SerializeField]
    Animator anim;

    Vector2 targetPos;

    void Update()
    {
        if (Mananger.instance.m_GetMouseButtonDownRight)
        {
            targetPos = Camera.main.ScreenToWorldPoint(Mananger.instance.m_mousePosition);
        }
        Move();
    }

    void Move()
    {
        if ((Vector2)transform.position != targetPos)
        {
            Vector2 dir = 

            rb2d.velocity = new Vector2();
        }
    }
}
