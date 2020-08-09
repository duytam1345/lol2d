using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mananger : MonoBehaviour
{
    public static Mananger instance;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public bool m_GetMouseButtonDownLeft
    {
        get
        {
            if (Input.GetMouseButtonDown(0))
            {
                return true;
            }
            return false;
        }
    }
    public bool m_GetMouseButtonDownRight
    {
        get
        {
            return Input.GetMouseButtonDown(1);
        }
    }

    public Vector2 m_mousePosition
    {
        get
        {
            return Input.mousePosition;
        }
    }
}
