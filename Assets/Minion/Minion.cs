using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour
{
    [SerializeField]
    float speedMoveToTarget = 1f;
    [SerializeField]
    float radiusAttach = 1f;
    [SerializeField]
    float timeSpawWeapon = 0.1f;
    GameObject weapon;
    public static System.Action OnSpaw;
    public static System.Action<Minion> OnFindEnemy;
    Transform enemy;
    enum eState
    {
        FindTarget,
        MoveToTarget,
        Fighting
    }
    eState state = eState.FindTarget;
    bool canSpawn;
    public void SetEnemy(Transform enemy)
    {
        this.enemy = enemy;
    }
    void Start()
    {
        OnSpaw?.Invoke();
        weapon = Resources.Load("WeaponMinion") as GameObject;
    }
    void Update()
    {
        switch (state)
        {
            case eState.FindTarget:
                FindTarget();
                break;
            case eState.MoveToTarget:
                MoveToTarget();
                break;
            case eState.Fighting:
                Fighting();
                break;
        }
    }
    private void FindTarget()
    {
        OnFindEnemy?.Invoke(this);
        if (enemy != null)
        {
            state = eState.MoveToTarget;
        }
    }
    private void MoveToTarget()
    {
        var d = Vector3.Distance(transform.position, enemy.transform.position);
        if (d < radiusAttach)
        {
            state = eState.Fighting;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, enemy.position, speedMoveToTarget * Time.deltaTime);
        }
    }
    private void Fighting()
    {
        if (!canSpawn)
        {
            canSpawn = true;
            StartCoroutine(Spawn());
        }
        //neu enemy chet thi state = find...
    }
    private IEnumerator Spawn()
    {
        while (canSpawn)
        {
            if (timeSpawWeapon < 0.1f)
            {
                timeSpawWeapon = 0.1f;
            }
            yield return new WaitForSeconds(timeSpawWeapon);
            SpawWeapon();
        }
    }
    private void SpawWeapon()
    {
        var w = GameObject.Instantiate(weapon);
        w.transform.position = transform.position;
        w.GetComponent<WeaponMinion>().SetTarget(enemy);
    }
}
