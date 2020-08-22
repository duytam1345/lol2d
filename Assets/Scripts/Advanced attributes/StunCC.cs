using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunCC : CrowdControl
{

    private void Start()
    {
        StartEffect();
    }

    private void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            EndEffect();
        }
    }

    public override void StartEffect()
    {
        if (GetComponent<Creep>() && GetComponent<Creep>().team != team)
        {
            GetComponent<Creep>().state = Creep.State.Stun;
        }
    }

    public override void EndEffect()
    {
        if (GetComponent<Creep>() && GetComponent<Creep>().team != team)
        {
            GetComponent<Creep>().state = Creep.State.Idle;
            Destroy(GetComponent<StunCC>());
        }
    }
}
