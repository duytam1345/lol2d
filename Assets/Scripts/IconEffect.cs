using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconEffect : MonoBehaviour
{
    public Sprite icon;
    public float timeCooldown;
    public float timeCooldownSecond;
    public int intValue;

    public virtual void UpdateValue() { }
}
