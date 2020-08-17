using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Needlessly_Large_Rod : Item
{
    public int magic;
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
        champion.propertyChampion.magicDamageExtra.Add(nameItem + (i.ToString()), magic);
        champion.propertyChampion.UpdateValue();
    }
    public override void Sell()
    {
        champion.propertyChampion.magicDamageExtra.Remove(nameItem + indexInventory);
        champion.propertyChampion.UpdateValue();
    }
}
