using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconEffectSkillWKogMaw : IconEffect
{
    public override void UpdateValue()
    {
        timeCooldownSecond -= Time.deltaTime;

        if (timeCooldownSecond <= 0)
        {
            FindObjectOfType<Charater>().champion.propertyChampion.attackRangeExtra.Remove("Skill W KogMaw");

            Destroy(UIManager.instance.listEffect["Skill W KogMaw"].gameObject);
            UIManager.instance.listEffect.Remove("Skill W KogMaw");
        }
    }
}
