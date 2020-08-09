using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    void Awake()
    {
        Minion.OnSpaw += OnMinionSpawn;
    }
    void OnMinionSpawn()
    {

    }
    void Update()
    {

    }
}
