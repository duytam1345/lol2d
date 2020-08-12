using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static bool m_GetMouseButtonDownLeft
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
    public static bool m_GetMouseButtonDownRight
    {
        get
        {
            return Input.GetMouseButtonDown(1);
        }
    }

    public static Vector2 m_mousePosition
    {
        get
        {
            return Input.mousePosition;
        }
    }

    public static bool m_KeyDownQ
    {
        get
        {
            return Input.GetKeyDown(KeyCode.Q);
        }
    }

    public static bool m_KeyDownW
    {
        get
        {
            return Input.GetKeyDown(KeyCode.Q);
        }
    }

    public static bool m_KeyDownE
    {
        get
        {
            return Input.GetKeyDown(KeyCode.Q);
        }
    }

    public static bool m_KeyDownR
    {
        get
        {
            return Input.GetKeyDown(KeyCode.Q);
        }
    }

    public static bool m_KeyDownP
    {
        get
        {
            return Input.GetKeyDown(KeyCode.Q);
        }
    }

    public static bool SwiftCamMode
    {
        get
        {
            return Input.GetKeyDown(KeyCode.Y);
        }
    }

    public static bool m_KeyDownS
    {
        get
        {
            return Input.GetKeyDown(KeyCode.S);
        }
    }
}
