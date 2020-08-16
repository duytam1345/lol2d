using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rejuvenation_Bead : Item
{
    public int regenPercent;
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
        float amount = champion.propertyChampion.healthRegen / 100 * regenPercent;

        champion.propertyChampion.healthRegenExtra.Add(nameItem + (i.ToString()), amount);
        champion.propertyChampion.UpdateValue();
        indexInventory = i;
    }
    public override void Sell()
    {
        champion.propertyChampion.healthRegenExtra.Remove(nameItem + indexInventory);
        champion.propertyChampion.UpdateValue();
    }
}
