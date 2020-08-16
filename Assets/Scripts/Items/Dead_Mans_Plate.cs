using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead_Mans_Plate : Item
{
    public int hp;
    public int arrmor;
    public override Item Buy()
    {
        if (prefabItem)
        {
            GameObject g = Instantiate(prefabItem);
            return g.GetComponent<Item>();
        }
        return null;
    }
    public override void Use(int i)
    {
        champion.propertyChampion.healthPointExtra.Add(nameItem + (i.ToString()), hp);
        champion.propertyChampion.arrmorExtra.Add(nameItem + (i.ToString()), arrmor);
        champion.propertyChampion.UpdateValue();
    }
    public override void Sell()
    {
        champion.propertyChampion.healthPointExtra.Remove(nameItem + indexInventory);
        champion.propertyChampion.arrmorExtra.Remove(nameItem + indexInventory);
        champion.propertyChampion.UpdateValue();
    }
}
