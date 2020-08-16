using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloak_of_Agility : Item
{
    public int crit;
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
        champion.propertyChampion.critRateExtra.Add(nameItem + (i.ToString()), crit);
        champion.propertyChampion.UpdateValue();
        indexInventory = i;
    }
    public override void Sell()
    {
        champion.propertyChampion.critRateExtra.Remove(nameItem + indexInventory);
        champion.propertyChampion.UpdateValue();
    }
}
