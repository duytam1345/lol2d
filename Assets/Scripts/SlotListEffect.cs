using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotListEffect : MonoBehaviour
{
    public Image imageBg;
    public Image imageCooldown;
    public Text textInt;

    public IconEffect iconEffect;

    private void Start()
    {
        imageBg.sprite = iconEffect.icon;
        imageCooldown.sprite = iconEffect.icon;
    }

    private void Update()
    {
        imageBg.sprite = iconEffect.icon;
        imageCooldown.sprite = iconEffect.icon;

        imageCooldown.fillAmount = iconEffect.timeCooldownSecond / iconEffect.timeCooldown;
        textInt.text = iconEffect.intValue.ToString();

        iconEffect.UpdateValue();
    }
}
