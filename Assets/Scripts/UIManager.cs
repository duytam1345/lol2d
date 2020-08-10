using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instace;

    public GameObject uiFolowPlayer;
    [SerializeField]
    float offsetY;

    Charater charater;

    [SerializeField]
    GameObject textDamage;

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

    private void Start()
    {
        charater = FindObjectOfType<Charater>();
    }

    private void Update()
    {
        Vector2 p = Camera.main.WorldToScreenPoint(charater.transform.position);
        p.y += offsetY;

        uiFolowPlayer.GetComponent<RectTransform>().position = p;
    }

    public GameObject panelShop;

    public void ClickShop()
    {
        panelShop.SetActive(!panelShop.activeInHierarchy);
    }

    public void MakeTextDamage(Vector2 position, string dmg)
    {
        GameObject t = Instantiate(textDamage, position, Quaternion.identity, GameObject.Find("Canvas").transform);
        t.GetComponent<Text>().text = dmg;
    }
}
