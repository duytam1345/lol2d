using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Potion : Item
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
        champion.propertyChampion.healthRegenExtra.Add(nameItem + (i.ToString()), hp);
        champion.propertyChampion.UpdateValue();
    }
    public override void Sell()
    {
        champion.propertyChampion.healthRegenExtra.Remove(nameItem + indexInventory);
        champion.propertyChampion.UpdateValue();
    }
}
