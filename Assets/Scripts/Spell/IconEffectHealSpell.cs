using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconEffectHealSpell : IconEffect
{
    public override void UpdateValue()
    {
        timeCooldownSecond -= Time.deltaTime;

        if (timeCooldownSecond <= 0)
        {
            Destroy(UIManager.instace.listEffect["Heal Spell"].gameObject);
            Destroy(gameObject);
        }
    }
}
