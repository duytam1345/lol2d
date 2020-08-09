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
        //if (Mananger.instance.m_GetMouseButtonDownRight)
        //{
        //    //targetPos = 
        //}
        Move();
    }

    void Move()
    {
        //transform.position = Vector2.MoveTowards(transform.position,)
    }
    private void Test()
    {

    }
}
