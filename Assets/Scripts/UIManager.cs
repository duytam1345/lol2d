using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instace;

    private void Awake()
    {
        if (!instace)
        {
            instace = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public GameObject panelShop;

    public void ClickShop()
    {
        panelShop.SetActive(!panelShop.activeInHierarchy);
    }
}
