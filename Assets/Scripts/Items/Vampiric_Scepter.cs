using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vampiric_Scepter : Item
{
    public int lifeSteel;
    public int damage;
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
        champion.propertyChampion.lifeSteelExtra.Add(nameItem + (i.ToString()), lifeSteel);
        champion.propertyChampion.physicsDamageExtra.Add(nameItem + (i.ToString()), damage);
        champion.propertyChampion.UpdateValue();
    }
    public override void Sell()
    {
        champion.propertyChampion.lifeSteelExtra.Remove(nameItem + indexInventory);
        champion.propertyChampion.physicsDamageExtra.Remove(nameItem + indexInventory);
        champion.propertyChampion.UpdateValue();
    }
}
