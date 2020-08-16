using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chain_Vest : Item
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
        print("Add: " + nameItem + (i.ToString()));
        champion.propertyChampion.arrmorExtra.Add(nameItem + (i.ToString()), arrmor);
        champion.propertyChampion.UpdateValue();
    }
    public override void Sell()
    {
        print("Remove: " + nameItem + indexInventory);
        champion.propertyChampion.arrmorExtra.Remove(nameItem + indexInventory); 
        champion.propertyChampion.UpdateValue();
    }
}
