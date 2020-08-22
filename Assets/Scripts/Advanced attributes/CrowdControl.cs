using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdControl : MonoBehaviour
{
    public Team team;

    public float timeLeft;


    public virtual void StartEffect() { }
    public virtual void EndEffect() { }
}
