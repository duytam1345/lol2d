using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public enum Team
{
    Red,
    Blue,
}

public class Mananger : MonoBehaviour
{
    [SerializeField]
    int numMinion;
    [SerializeField]
    Transform spawEnemyPosition;
    public static Mananger instance;
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    private void Start()
    {
        Minion.OnFindEnemy += OnMinionFindEnemy;
        SpawMinion();
    }
    private void SpawMinion()
    {
        StartCoroutine(IESpawMinion());
    }
    private IEnumerator IESpawMinion()
    {
        var enemy = Resources.Load("Minion_type1 1") as GameObject;
        for (int i = 0; i < numMinion; i++)
        {
            yield return new WaitForSeconds(1f);
            var e = GameObject.Instantiate(enemy);
            e.transform.position = spawEnemyPosition.position;
        }
    }
    private void OnMinionFindEnemy(Minion minion)
    {
        minion.SetEnemy(GameObject.Find("Minion_type1_test").transform);
    }
}
