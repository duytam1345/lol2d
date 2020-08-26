using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconEffectGhostSpell : IconEffect
{
    public override void UpdateValue()
    {
        timeCooldownSecond -= Time.deltaTime;

        if (timeCooldownSecond <= 0)
        {
            FindObjectOfType<Charater>().champion.propertyChampion.moveSpeedExtra.Remove("Ghost");
            FindObjectOfType<Charater>().champion.propertyChampion.UpdateValue();

            Destroy(UIManager.instance.listEffect["Ghost Spell"].gameObject);
            Destroy(gameObject);
        }
    }
}
