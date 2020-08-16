using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public enum Type
    {
        Normal,
        Active,
        Ward,
    }
    public enum Where
    {
        Inventory,
        ListItem,
        BuildInto,
        Requires
    }
    public enum TypeRequires
    {
        T1,
        T11,
        T12,
        T13,
        T12_11,
        T12_12,
        T12_13,
        T12_1,
        T11_12,
        T1_11_1,
        T13_12,
        T1_1_11,
        T12_1_1,
        T12_1_12,
        T13_11_1,
        T11_11
    }

    public GameObject imgTarget;
    public Item[] childItem;
    public Item[] parentItem;

    public Where where;
    public Type type;
    public TypeRequires typeRequires;
    public string nameItem;
    public string infoItem;
    public int costItem;
    public Sprite sprite;

    public Champion champion;

    public GameObject prefabItem;

    public int indexInventory = 0;

    private void Start()
    {
        champion = FindObjectOfType<Charater>().GetComponentInChildren<Champion>();

        if (transform.childCount >= 3)
        {
            switch (where)
            {
                case Where.Inventory:
                    break;
                case Where.ListItem:
                    transform.GetChild(1).GetComponent<Image>().sprite = sprite;
                    transform.GetChild(2).GetComponent<Text>().text = GetAllCost().ToString();
                    break;
                case Where.BuildInto:
                    transform.GetChild(1).GetComponent<Image>().sprite = sprite;

                    transform.GetChild(0).gameObject.SetActive(false);
                    transform.GetChild(2).gameObject.SetActive(false);
                    break;
                case Where.Requires:
                    GetComponent<RectTransform>().anchorMin = new Vector2(.5f, 1f);
                    GetComponent<RectTransform>().anchorMax = new Vector2(.5f, 1f);

                    transform.GetChild(1).GetComponent<Image>().sprite = sprite;

                    transform.GetChild(2).gameObject.SetActive(false);
                    break;
                default:
                    break;
            }
        }
    }

    public virtual Item Buy() { return null; }
    public virtual void Use(int i) { }
    public virtual void Sell() { }

    public int GetAllCost()
    {
        int c = costItem;
        for (int i = 0; i < childItem.Length; i++)
        {
            c += childItem[i].GetAllCost();
        }
        return c;
    }

    public void GetName(int i)
    {
        print(i + "-" + nameItem);
        i++;
        foreach (Item item in childItem)
        {
            //item.GetName(i);
        }
    }
}