using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KogMawBot : BotController
{
    private void Update()
    {
        if (c.state != Champion.State.Death)
        {
            Act();
        }
        UpdateImage();
        CheckItem();
    }

    public override void Act()
    {
        t -= Time.deltaTime;

        if (t > 0)
        {
            return;
        }
        else
        {
            t = Random.Range(.05f, .35f);
        }

        CheckLevel();

        Vector2 vPosTurret = ownTurret.transform.position;
        vPosTurret.y -= 1;

        //Thời gian < 15 -> 
        //Ra trụ đứng đợi lính
        if (Time.time < 15)
        {
            UseSkill(10);
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
            Fountain fountain = GameObject.Find("Fountain").transform.GetChild(1).GetComponent<Fountain>();
            if (Vector2.Distance(transform.position, fountain.transform.position) <= 2)
            {
                if (c.propertyChampion.healthPointSecond < c.propertyChampion.healthPoint_Real)
                {
                    c.rb2d.velocity = Vector2.zero;
                    c.state = Champion.State.Idle;
                    return;
                }
            }

            if (c.propertyChampion.healthPointSecond <= (c.propertyChampion.healthPoint_Real / 100 * 20) ||
                c.propertyChampion.manaPointSecond <= (c.propertyChampion.manaPoint_Real / 100 * 15))
            {
                UseSkill(2);
                if (Vector2.Distance(transform.position, fountain.transform.position) > 2)
                {
                    if (c.propertyChampion.healthPointSecond <= (c.propertyChampion.healthPoint_Real / 100 * 20) &&
                        c.spellD.timeCooldownSecond <= 0)
                    {
                        c.spellD.Use(c);
                    }

                    Vector2 v = ownTurret.transform.position;
                    v.x += 2;
                    if (Vector2.Distance(transform.position, v) > 2)
                    {
                        c.targetPos = v;
                        c.Move();
                        return;
                    }
                    else
                    {
                        c.ReCall();
                        return;
                    }
                }
                else
                {
                    c.rb2d.velocity = Vector2.zero;
                    c.state = Champion.State.Idle;
                    return;
                }
            }
            else
            {
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
                    UseSkill(2);
                    c.rb2d.velocity = Vector2.zero;
                    ActionCreep();
                    return;
                }
                else if (gChamp.Count == 1)
                {
                    if (transform.position.x > centerTurret.x)
                    {
                        UseSkill(8);
                        if (c.propertyChampion.healthPointSecond >= gChamp[0].GetComponent<Champion>().propertyChampion.healthPointSecond)
                        {
                            int r = Random.Range(0, 10);
                            if (r <= (c.attackSpeedSecond <= .3f ? 6 : 3))
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
                                return;
                            }

                        }
                        else
                        {
                            int r = Random.Range(0, 10);
                            if (r <= (c.attackSpeedSecond <= .3f ? 5 : 2))
                            {
                                Vector2 v = transform.position;
                                v.x += 1;
                                c.targetPos = v;
                                c.Move();
                                return;
                            }
                            else
                            {
                                c.targetAttack = gChamp[0];
                                c.MoveToAttack();
                                return;
                            }
                        }
                    }
                    else
                    {
                        UseSkill(4);
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
                else
                {
                    int r = Random.Range(0, 10);
                    if (r <= 3)
                    {
                        c.targetAttack = item;
                        c.LoadAttack();
                    }
                    else
                    {
                        c.targetAttack = null;
                    }
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
        v.y = Random.Range(v.y - 1, v.y + 1);

        if (Vector2.Distance(transform.position, v) > .5f)
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

    void CheckLevel()
    {
        if (c.leftPointSkill > 0)
        {
            switch (c.propertyChampion.level)
            {
                case 1:
                    c.leftPointSkill--;
                    c.levelSkillQ++;
                    break;
                case 2:
                    c.leftPointSkill--;
                    c.levelSkillE++;
                    break;
                case 3:
                    c.leftPointSkill--;
                    c.levelSkillW++;
                    break;
                case 4:
                    c.leftPointSkill--;
                    c.levelSkillQ++;
                    break;
                case 5:
                    c.leftPointSkill--;
                    c.levelSkillQ++;
                    break;
                case 6:
                    c.leftPointSkill--;
                    c.levelSkillR++;
                    break;
                case 7:
                    c.leftPointSkill--;
                    c.levelSkillQ++;
                    break;
                case 8:
                    c.leftPointSkill--;
                    c.levelSkillQ++;
                    break;
                case 9:
                    c.leftPointSkill--;
                    c.levelSkillE++;
                    break;
                case 10:
                    c.leftPointSkill--;
                    c.levelSkillE++;
                    break;
                case 11:
                    c.leftPointSkill--;
                    c.levelSkillR++;
                    break;
                case 12:
                    c.leftPointSkill--;
                    c.levelSkillE++;
                    break;
                case 13:
                    c.leftPointSkill--;
                    c.levelSkillE++;
                    break;
                case 14:
                    c.leftPointSkill--;
                    c.levelSkillW++;
                    break;
                case 15:
                    c.leftPointSkill--;
                    c.levelSkillW++;
                    break;
                case 16:
                    c.leftPointSkill--;
                    c.levelSkillR++;
                    break;
                case 17:
                    c.leftPointSkill--;
                    c.levelSkillW++;
                    break;
                case 18:
                    c.leftPointSkill--;
                    c.levelSkillW++;
                    break;
                default:
                    break;
            }
        }
    }

    void CheckItem()
    {
    }

    void UseSkill(int i)
    {
        //Q
        foreach (var item in GameObjectsInRadius(6))
        {
            if (item.GetComponent<Champion>() && item.GetComponent<Champion>().team != c.team)
            {
                Vector3 dir = item.transform.position - transform.position;
                RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, dir);
                foreach (var h in hit)
                {
                    if (hit.Length <= 2)
                    {
                        if (h.collider.gameObject != gameObject)
                        {
                            if (h.collider.GetComponent<Champion>())
                            {
                                if (c.levelSkillQ > 0 && c.propertyChampion.manaPointSecond >= 40)
                                {
                                    int r = Random.Range(0, 10);
                                    if (r <= i)
                                    {
                                        Vector2 v = item.transform.position;

                                        c.SkillQ(new Vector3(Random.Range(v.x - 1, v.x + 1), Random.Range(v.y - 1, v.y + 1), 0));
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        //E
        foreach (var item in GameObjectsInRadius(3))
        {
            if (item.GetComponent<Creep>() && item.GetComponent<Creep>().team != c.team)
            {
                int r = Random.Range(0, 10);
                if (r <= i)
                {
                    if (c.levelSkillE > 0 && c.propertyChampion.manaPointSecond >= 80)
                    {
                        c.SkillE(item.transform.position);
                    }
                }
            }
        }
        //R
        foreach (var item in GameObjectsInRadius(7))
        {
            if (c.levelSkillR > 0 && c.propertyChampion.manaPointSecond >= 40)
            {
                int r = Random.Range(0, 10);
                if (r <= i)
                {
                    c.SkillR(item.transform.position);
                }
            }
        }
    }
    public override void UpdateImage()
    {
        c.uI.transform.GetChild(3).GetComponent<Image>().fillAmount = (float)c.propertyChampion.healthPointSecond / (float)c.propertyChampion.healthPoint_Real;
        c.uI.transform.GetChild(5).GetComponent<Image>().fillAmount = (float)c.propertyChampion.manaPointSecond / (float)c.propertyChampion.manaPoint_Real;
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawWireSphere(transform.position, 5);


        //Bounds bounds = new Bounds();
        //List<GameObject> gCr = GameObjectsInRadius(5);
        //foreach (GameObject item in gCr)
        //{
        //    if (item.GetComponent<Creep>() && item.GetComponent<Creep>().team == c.team)
        //    {
        //        if ((bounds.center != Vector3.zero && Vector3.Distance(bounds.center, item.transform.position) <= 4)
        //            || bounds.center == Vector3.zero)
        //        {
        //            bounds.Encapsulate(item.GetComponent<BoxCollider2D>().bounds);
        //        }
        //    }
        //}

        //Vector2 v = Vector3.zero;
        //if (bounds.center != Vector3.zero)
        //{
        //    v = bounds.center;
        //}
        //else
        //{
        //    v = ownTurret.transform.position;
        //}

        //Gizmos.DrawWireCube(bounds.center, bounds.size);
    }
}
