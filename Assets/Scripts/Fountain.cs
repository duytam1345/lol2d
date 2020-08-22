using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fountain : MonoBehaviour
{
    [SerializeField]
    Team team;

    [SerializeField]
    CircleCollider2D circleCollider;

    float timeT;

    private void Update()
    {
        timeT += Time.deltaTime;

        if (timeT >= .25f)
        {
            timeT =0;

            Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(circleCollider.transform.position, circleCollider.radius);

            foreach (var item in collider2Ds)
            {
                if (item.GetComponent<Champion>() && item.GetComponent<Champion>().team == team)
                {
                    float amount = (item.GetComponent<Champion>().propertyChampion.healthPoint_Real)/100*2.1f;
                    item.GetComponent<Champion>().TakeHealth(amount);
                    item.GetComponent<Champion>().TakeMana(amount);
                }
            }
        }
    }
}
