using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Negatron_Cloak : Item
{
    public int resistance;
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
        champion.propertyChampion.magicResistanceExtra.Add(nameItem + (i.ToString()), resistance);
        champion.propertyChampion.UpdateValue();
    }
    public override void Sell()
    {
        champion.propertyChampion.magicResistanceExtra.Remove(nameItem + indexInventory);
        champion.propertyChampion.UpdateValue();
    }
}
