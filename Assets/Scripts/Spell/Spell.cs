using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell:MonoBehaviour
{
    public Sprite sprite;

    public float timeCooldown;
    public float timeCooldownSecond;

    public GameObject effect;

    public GameObject iconEffect;

    public virtual void Use(Champion champion) { }
}
