using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconUseSkillRHeimerdinger : IconEffect
{
    public override void UpdateValue()
    {
        timeCooldownSecond = 0;
        timeCooldown = 1;
    }
}
