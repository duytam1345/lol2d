using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSpell : Spell
{
    Champion c;

    bool actived = false;
    float timeEffect;

    private void Update()
    {
        if (timeEffect > 0 && actived)
        {
            timeEffect -= Time.deltaTime;

            if (timeEffect <= 0)
            {
                c.propertyChampion.moveSpeedExtra.Remove("Ghost");
                actived = false;
            }
        }
    }

    public override void Use(Champion champion)
    {
        if (timeCooldownSecond <= 0)
        {
            c = champion;
            c.propertyChampion.moveSpeedExtra.Add("Ghost", CongThuc.Ghost(champion));
            c.propertyChampion.UpdateValue();
            SoundBase.Sound.Ghost();
            actived = true;

            GameObject icon = Instantiate(iconEffect);
            UIManager.instace.CreateSlotEffect("Ghost Spell", icon.GetComponent<IconEffect>());

            GameObject g = Instantiate(effect, transform.position, Quaternion.identity, champion.transform);

            if (c.propertyChampion.level == 1)
            {
                g.GetComponent<TimeDestroy>().timeToDestroyGameObject = 4;
                timeEffect = 4;
            }
            else if (c.propertyChampion.level > 1 && c.propertyChampion.level <= 3)
            {
                timeEffect = 5;
                g.GetComponent<TimeDestroy>().timeToDestroyGameObject = 5;
            }
            else if (c.propertyChampion.level > 3 && c.propertyChampion.level <= 10)
            {
                timeEffect = 6;
                g.GetComponent<TimeDestroy>().timeToDestroyGameObject = 6;
            }
            else
            {
                timeEffect = 7;
                g.GetComponent<TimeDestroy>().timeToDestroyGameObject = 7;
            }
            timeCooldownSecond = 210;
        }
    }
}
