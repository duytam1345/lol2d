using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMinion : MonoBehaviour
{
    [SerializeField]
    float speed = 1f;
    Transform target;
    public void SetTarget(Transform target)
    {
        this.target = target;
    }
    void Update()
    {
        if (target == null)
        {
            return;
        }
        var d = Vector3.Distance(target.position, transform.position);
        if (d < 0.1f)
        {
            GameObject.Destroy(gameObject);
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }
}
