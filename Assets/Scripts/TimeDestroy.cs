using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDestroy : MonoBehaviour
{
    public float timeToDestroyGameObject;
    void Start()
    {
        Destroy(gameObject, timeToDestroyGameObject);
    }

}
