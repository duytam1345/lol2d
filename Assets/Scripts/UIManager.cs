using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instace;

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

    public GameObject panelShop;

    public void ClickShop()
    {
        panelShop.SetActive(!panelShop.activeInHierarchy);
    }

    public void MakeTextDamage(Vector2 position, string dmg)
    {
        Vector2 rPos = Camera.main.WorldToScreenPoint(position);
        GameObject t = Instantiate(textDamage, rPos, Quaternion.identity, GameObject.Find("Canvas").transform);
        t.GetComponent<Text>().text = dmg;

        StartCoroutine(FadeOut(t.GetComponent<Text>(), 1));
    }

    public IEnumerator FadeOut(Text t, float duration)
    {
        int index = 0;

        Color c = t.color;

        while (index < 1000 && c.a > 0)
        {
            c.a -= 1/(duration/Time.deltaTime);
            if (t)
            {
                t.color = c;
            }
            index++;
            yield return null;
        }
    }
}
