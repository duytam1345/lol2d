using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconCooldownSkillQHeimerdinger : IconEffect
{
    public HeimerdingerChampion heimerdinger;

    public override void UpdateValue()
    {
        intValue = heimerdinger.leftKit;
        timeCooldown = 11;
        if (heimerdinger.leftKit == 3)
        {
            timeCooldownSecond = 0;
        }
        else
        {
            timeCooldownSecond = heimerdinger.timeCooldownKit;

        }
    }
}
