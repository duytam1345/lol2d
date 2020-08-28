using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSpell : Spell
{
    public override void Use(Champion champion)
    {
        if (timeCooldownSecond <= 0)
        {
            Instantiate(effect, transform.position, Quaternion.identity, champion.transform);

            if (!champion.isBot)
            {
                GameObject icon = Instantiate(iconEffect);
                UIManager.instance.CreateSlotEffect("Heal Spell", icon.GetComponent<IconEffect>());
            }
            champion.TakeHealth(CongThuc.Heal(champion));
            SoundBase.Sound.Heal();
            timeCooldownSecond = 240;
        }
    }
}
