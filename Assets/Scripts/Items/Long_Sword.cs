using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Long_Sword : Item
{
    public int dmg;
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
        champion.propertyChampion.physicsDamageExtra.Add(nameItem + (i.ToString()), dmg);
        champion.propertyChampion.UpdateValue();
        indexInventory = i;
    }
    public override void Sell()
    {
        champion.propertyChampion.physicsDamageExtra.Remove(nameItem + indexInventory);
        champion.propertyChampion.UpdateValue();
    }
}
