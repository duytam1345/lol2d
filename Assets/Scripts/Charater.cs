using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charater : MonoBehaviour
{
    public float speed;

    void Update()
    {
        Mananger.instance.m_Input();
    }
}
