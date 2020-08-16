﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CongThuc : MonoBehaviour
{
    static int[] exp = new int[18] { 0, 280, 660, 1140, 1720, 2400, 3180, 4060, 5040, 6120, 7300, 8580, 9960, 11440, 13020, 14700, 16480, 18360 };

    public static float LayDamage(float damage, float resitance)
    {
        float percent = 100 / (100 + resitance);
        return damage / 1 * percent;
    }

    public static int ExpNextLevel(int level)
    {
        return exp[level - 1];
    }

    public static int ExpOfCreep(Creep.Type typeCreep)
    {
        switch (typeCreep)
        {
            case Creep.Type.melee:
                return 59;
            case Creep.Type.Caster:
                return 29;
            case Creep.Type.Siege:
                return 92;
            case Creep.Type.Super:
                return 97;
        }
        return 0;
    }

    public static int MoneyOfCreep(Creep.Type typeCreep)
    {
        switch (typeCreep)
        {
            case Creep.Type.melee:
                return 19;
            case Creep.Type.Caster:
                return 14;
            case Creep.Type.Siege:
                return 40;
            case Creep.Type.Super:
                return 40;
        }
        return 0;
    }
}