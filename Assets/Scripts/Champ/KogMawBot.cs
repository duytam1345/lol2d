using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KogMawBot : BotController
{
    private void Update()
    {
        Act();
        UpdateImage();
    }

    public override void Act()
    {
        //t -= Time.deltaTime;

        //if (t > 0)
        //{
        //    return;
        //}
        //else
        //{
        //    t = .25f;
        //}

        Vector2 vPosTurret = ownTurret.transform.position;
        vPosTurret.y -= 1;

        //Thời gian < 15 -> 
        //Ra trụ đứng đợi lính
        if (Time.time < 15)
        {
            if (Vector2.Distance(transform.position, vPosTurret) >= .1f)
            {
                c.targetPos = vPosTurret;
                c.Move();
            }
            else
            {
                c.targetPos = Vector2.zero;
            }
        }
        //Thời gian >= 15 -> Mức độ ưu tiên giảm dần 
        //Nếu còn máu
        //Nếu trong phạm vi có tướng địch:
        //- Trong phạm vi trụ địch: -> lùi về
        //- Trong phạm vi trụ đồng minh:
        //- Máu thấp hơn địch ->  lùi về cho đến khi thoát khỏi phạm vi
        //- Máu nhiều hơn -> chủ động tấn công
        //Nếu trong phạm vi có lính địch yếu máu -> last hit
        //Nếu lính có xu hướng bị đẩy về trụ -> chủ động tấn công lính địch
        //Di chuyển ra vị trí phía sau lính đồng minh, không có lính thì về sau trụ đầu
        //Nếu hết máu biến về
        else if (Time.time >= 15)
        {
            if (c.propertyChampion.healthPointSecond >= c.propertyChampion.healthPointSecond)
            {
                c.ReCall();
            }

            //(Lấy mốc trung điểm giữa 2 trụ)
            Vector3 centerTurret = (nearestEnemyTurret.transform.position + ownTurret.transform.position) / 2;
            //danh sách kẻ địch trong phạm vi 5
            List<GameObject> g = GameObjectsInRadius(4);
            //Lấy danh sách tướng
            List<GameObject> gChamp = new List<GameObject>();
            foreach (var item in g)
            {
                if (item.GetComponent<Champion>() && item.GetComponent<Champion>().team != c.team)
                {
                    gChamp.Add(item);
                }
            }
            //Nếu có tướng: calculate
            if (gChamp.Count <= 0)
            {
                c.rb2d.velocity = Vector2.zero;
                ActionCreep();
                return;
            }
            else if (gChamp.Count == 1)
            {
                if (transform.position.x > centerTurret.x)
                {
                    if (c.propertyChampion.healthPointSecond >= gChamp[0].GetComponent<Champion>().propertyChampion.healthPointSecond)
                    {
                        c.targetAttack = gChamp[0];
                        c.MoveToAttack();
                        return;
                    }
                    else
                    {
                        Vector2 v = transform.position;
                        v.x += 1;
                        c.targetPos = v;
                        c.Move();
                    }
                }
                else
                {
                    if (c.propertyChampion.healthPointSecond >= gChamp[0].GetComponent<Champion>().propertyChampion.healthPointSecond)
                    {
                        foreach (var item in GameObjectsInRadius(7))
                        {
                            if (item.GetComponent<Turret>() && item.GetComponent<Turret>().team != c.team)
                            {
                                Vector2 v = transform.position;
                                v.x += 1;
                                c.targetPos = v;
                                c.Move();
                                return;
                            }
                        }

                        c.targetAttack = gChamp[0];
                        c.MoveToAttack();
                        return;
                    }
                    else
                    {
                        Vector2 v = transform.position;
                        v.x += 1;
                        c.targetPos = v;
                        c.Move();
                    }
                }
            }
            else
            {
                Vector2 v = ownTurret.transform.position;
                c.targetPos = v;
                c.Move();
            }
        }
    }

    void ActionCreep()
    {
        //Cụm lính
        Bounds bounds = new Bounds();
        List<GameObject> gCr = GameObjectsInRadius(5);
        //Encapsulate for all 
        foreach (GameObject item in gCr)
        {
            if (item.GetComponent<Creep>() && item.GetComponent<Creep>().team == c.team)
            {
                if ((bounds.center != Vector3.zero && Vector3.Distance(bounds.center, item.transform.position) <= 3)
                    || bounds.center == Vector3.zero)
                {
                    bounds.Encapsulate(item.GetComponent<BoxCollider2D>().bounds);
                }
            }
        }

        foreach (GameObject item in gCr)
        {
            if (item.GetComponent<Creep>() && item.GetComponent<Creep>().team != c.team)
            {
                if (item.GetComponent<Creep>().currentHealth <= c.propertyChampion.physicsDamage_Real)
                {
                    c.targetAttack = item;
                    c.LoadAttack();
                }
            }
        }

        Vector2 v = Vector3.zero;
        if (bounds.center != Vector3.zero)
        {
            v = bounds.center;
        }
        else
        {
            v = ownTurret.transform.position;
        }

        v.x += 1.5f;

        if (Vector2.Distance(transform.position, v) > 2f)
        {

            c.targetPos = v;
            c.Move();
        }
        else
        {
            c.targetPos = Vector2.zero;
            c.state = Champion.State.Idle;
        }
    }

    public override void UpdateImage()
    {
        c.uI.transform.GetChild(3).GetComponent<Image>().fillAmount = (float)c.propertyChampion.healthPointSecond / (float)c.propertyChampion.healthPoint_Real;
        c.uI.transform.GetChild(5).GetComponent<Image>().fillAmount = (float)c.propertyChampion.manaPointSecond / (float)c.propertyChampion.manaPoint_Real;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 5);
    }
}
