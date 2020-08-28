using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotInPanelKDA : MonoBehaviour
{
    //item tren bang kda
    public GameObject imageItem1_KDA;
    public GameObject imageItem2_KDA;
    public GameObject imageItem3_KDA;
    public GameObject imageItem4_KDA;
    public GameObject imageItem5_KDA;
    public GameObject imageItem6_KDA;

    public Text textKDA;
    public Text textCreep;

    public Image avatar;

    public Champion c;

    private void Start()
    {
        Set();
    }

    public void Set()
    {
        //Set trang bi
        //Set creep
        textCreep.text = c.cr.ToString();
        //Set KDA
        textKDA.text = c.kill + "/" + c.death + "/" + c.assist;
        //Set avatar champ
        avatar.sprite = c.spriteAvatar;

        if (c.item1.item != null)
        {
            imageItem1_KDA.transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
            imageItem1_KDA.transform.GetChild(0).GetComponent<Image>().sprite = c.item1.item.sprite;
        }
        else
        {
            imageItem1_KDA.transform.GetChild(0).GetComponent<Image>().color = new Color(0, 0, 0, 1);
        }

        if (c.item2.item != null)
        {
            imageItem2_KDA.transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
            imageItem2_KDA.transform.GetChild(0).GetComponent<Image>().sprite = c.item2.item.sprite;
        }
        else
        {
            imageItem2_KDA.transform.GetChild(0).GetComponent<Image>().color = new Color(0, 0, 0, 1);
        }

        if (c.item3.item != null)
        {
            imageItem3_KDA.transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
            imageItem3_KDA.transform.GetChild(0).GetComponent<Image>().sprite = c.item3.item.sprite;
        }
        else
        {
            imageItem3_KDA.transform.GetChild(0).GetComponent<Image>().color = new Color(0, 0, 0, 1);
        }

        if (c.item4.item != null)
        {
            imageItem4_KDA.transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
            imageItem4_KDA.transform.GetChild(0).GetComponent<Image>().sprite = c.item4.item.sprite;
        }
        else
        {
            imageItem4_KDA.transform.GetChild(0).GetComponent<Image>().color = new Color(0, 0, 0, 1);
        }

        if (c.item5.item != null)
        {
            imageItem5_KDA.transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
            imageItem5_KDA.transform.GetChild(0).GetComponent<Image>().sprite = c.item5.item.sprite;
        }
        else
        {
            imageItem5_KDA.transform.GetChild(0).GetComponent<Image>().color = new Color(0, 0, 0, 1);
        }

        if (c.item6.item != null)
        {
            imageItem6_KDA.transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
            imageItem6_KDA.transform.GetChild(0).GetComponent<Image>().sprite = c.item6.item.sprite;
        }
        else
        {
            imageItem6_KDA.transform.GetChild(0).GetComponent<Image>().color = new Color(0, 0, 0, 1);
        }
    }
}
