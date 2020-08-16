using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sapphire_Crystal : Item
{
    public int mp;
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
        champion.propertyChampion.manaPointExtra.Add(nameItem + (i.ToString()), mp);
        champion.propertyChampion.UpdateValue();
        indexInventory = i;
    }
    public override void Sell()
    {
        champion.propertyChampion.manaPointExtra.Remove(nameItem + indexInventory);
        champion.propertyChampion.UpdateValue();
    }
}
