using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ruby_Crytal : Item
{
    public int hp;
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
        champion.propertyChampion.UpdateValue();
    }
    public override void Sell()
    {
        champion.propertyChampion.healthPointExtra.Remove(nameItem + indexInventory);
        champion.propertyChampion.UpdateValue();
    }
}
