using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotController : MonoBehaviour
{
    public Champion c;

    public float t = 1;

    public Turret nearestEnemyTurret;
    public Turret ownTurret;

    private void Start()
    {
        nearestEnemyTurret = GameObject.Find("Turret Blue 1").GetComponent<Turret>();
        ownTurret = GameObject.Find("Turret Red 1").GetComponent<Turret>();
    }

    public virtual void Act() { }

    public List<GameObject> GameObjectsInRadius(float radius)
    {
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(transform.position, radius);

        List<GameObject> gameObjects = new List<GameObject>();

        foreach (var item in collider2Ds)
        {
            if (item.GetComponent<Creep>())
            {
                gameObjects.Add(item.gameObject);
            }
            if (item.GetComponent<Turret>())
            {
                gameObjects.Add(item.gameObject);
            }
            if (item.GetComponent<Champion>())
            {
                gameObjects.Add(item.gameObject);
            }
        }

        return gameObjects;
    }

    public virtual void UpdateImage() { }
}
