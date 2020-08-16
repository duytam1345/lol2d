using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blasting_Wand : Item
{
    public int mg;
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
        champion.propertyChampion.magicDamageExtra.Add(nameItem + (i.ToString()), mg);
        champion.propertyChampion.UpdateValue();
        indexInventory = i;
    }
    public override void Sell()
    {
        champion.propertyChampion.magicDamageExtra.Remove(nameItem + indexInventory);
        champion.propertyChampion.UpdateValue();
    }
}
