﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boots_of_Speed : Item
{
    public int speed;
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
        champion.propertyChampion.attackSpeedExtra.Add(nameItem + (i.ToString()), speed);
        champion.propertyChampion.UpdateValue();
        indexInventory = i;
    }
    public override void Sell()
    {
        champion.propertyChampion.attackSpeedExtra.Remove(nameItem + indexInventory);
        champion.propertyChampion.UpdateValue();
    }
}
