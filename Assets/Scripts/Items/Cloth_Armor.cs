using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloth_Armor : Item
{
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
        champion.propertyChampion.arrmorExtra.Add(nameItem + (i.ToString()), arrmor);
        champion.propertyChampion.UpdateValue();
        indexInventory = i;
    }
    public override void Sell()
    {
        champion.propertyChampion.arrmorExtra.Remove(nameItem + indexInventory);
        champion.propertyChampion.UpdateValue();
    }
}
