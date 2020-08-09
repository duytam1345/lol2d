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

    [SerializeField]
    Vector2 targetPos;

    void Update()
    {
        if (InputManager.m_GetMouseButtonDownRight)
        {
            targetPos = Camera.main.ScreenToWorldPoint(InputManager.m_mousePosition);
        }

        if (InputManager.m_GetMouseButtonDownLeft)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(InputManager.m_mousePosition);

            RaycastHit2D r = Physics2D.Raycast(mousePos, Vector2.zero);
            if (r.collider)
            {
                if(r.collider.name== "Ong chu tiem trang bi")
                {

                }
            }

        }

        Move();
    }

    void Move()
    {
        if (targetPos != Vector2.zero)
        {
            if (Vector2.Distance(transform.position, targetPos) >= .2f)
            {
                Vector2 dir = targetPos - (Vector2)transform.position;

                rb2d.velocity = (dir.normalized) * speed;
            }
            else
            {
                targetPos = Vector2.zero;
                rb2d.velocity = Vector2.zero;
            }
        }
    }
}
