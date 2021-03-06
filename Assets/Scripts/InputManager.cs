﻿using System.Collections;
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

    public static bool m_GetMouseButtonRight
    {
        get
        {
            return Input.GetMouseButton(1);
        }
    }

    public static Vector2 m_mousePosition
    {
        get
        {
            return Input.mousePosition;
        }
    }

    public static bool m_KeyCtrl
    {
        get
        {
            return Input.GetKey(KeyCode.LeftControl);
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
            return Input.GetKeyDown(KeyCode.W);
        }
    }

    public static bool m_KeyDownE
    {
        get
        {
            return Input.GetKeyDown(KeyCode.E);
        }
    }

    public static bool m_KeyDownR
    {
        get
        {
            return Input.GetKeyDown(KeyCode.R);
        }
    }

    public static bool m_KeyDownD
    {
        get
        {
            return Input.GetKeyDown(KeyCode.D);
        }
    }

    public static bool m_KeyDownF
    {
        get
        {
            return Input.GetKeyDown(KeyCode.F);
        }
    }

    public static bool m_KeyDownP
    {
        get
        {
            return Input.GetKeyDown(KeyCode.P);
        }
    }

    public static bool SwiftCamMode
    {
        get
        {
            return Input.GetKeyDown(KeyCode.Space);
        }
    }

    public static bool m_KeyDownS
    {
        get
        {
            return Input.GetKeyDown(KeyCode.S);
        }
    }

    public static bool m_KeyDownB
    {
        get
        {
            return Input.GetKeyDown(KeyCode.B);
        }
    }

    public static bool m_KeyDownTab
    {
        get
        {
            return Input.GetKeyDown(KeyCode.Tab);
        }
    }
    public static bool m_KeyUpTab
    {
        get
        {
            return Input.GetKeyUp(KeyCode.Tab);
        }
    }
    
    public static bool m_KeyDownC
    {
        get
        {
            return Input.GetKeyDown(KeyCode.C);
        }
    }
    public static bool m_KeyUpC
    {
        get
        {
            return Input.GetKeyUp(KeyCode.C);
        }
    }

    public static bool m_KeyDownA
    {
        get
        {
            return Input.GetKeyDown(KeyCode.A);
        }
    }

    public static bool m_KeydownEnter
    {
        get
        {
            return Input.GetKeyDown(KeyCode.Return);
        }
    }
}
