using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBase : MonoBehaviour
{
    public static SoundBase Sound;

    private void Awake()
    {
        if (Sound == null)
        {
            Sound = this;
        }
    }

    public GameObject heal;
    public void Heal()
    {
        Instantiate(heal);
    }

    public GameObject ghost;
    public void Ghost()
    {
        Instantiate(ghost);
    }
}
