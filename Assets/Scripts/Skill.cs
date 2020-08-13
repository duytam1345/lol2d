using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SkillActive
{
    public enum Active
    {
        TrieuHoi,
        HoiMau,
        Stun,
        Slow
    }

    public GameObject g;
    public Active active;
    public float amount;
    public float timeEffect;
    public float timeEffectSecond;
}

public class Skill : MonoBehaviour
{
    public enum TypeActiveSkill
    {
        Switch,
        Active
    }

    public enum TypeSkill
    {
        passive,
        proactive
    }

    public enum TypeCost
    {
        Mana,
        Health,
        None
    }
    public TypeActiveSkill typeActiveSkill;
    public TypeSkill typeSkill;
    public TypeCost typeCost;

    public string nameSkill;
    public int costSkill;
    public float timeCooldown;
    public float timeCooldownSecond;

    [SerializeField]
    Image imageCooldown;

    public string info;


    public List<SkillActive> skillActives;

    private void Update()
    {
        if (timeCooldownSecond > 0)
        {
            timeCooldownSecond -= Time.deltaTime;
        }

        imageCooldown.fillAmount = (timeCooldownSecond / timeCooldown);
    }

    public void Active(GameObject g)
    {
        if (timeCooldownSecond <= 0)
        {
            timeCooldownSecond = timeCooldown;
            Charater c = FindObjectOfType<Charater>();

            foreach (var item in skillActives)
            {
                if (item.timeEffectSecond > 0)
                {
                    item.timeEffectSecond -= Time.deltaTime;
                    continue;
                }
                switch (item.active)
                {
                    case SkillActive.Active.TrieuHoi:
                        GameObject gS = Instantiate(item.g, c.transform.position, Quaternion.identity);
                        Vector2 dir = (Camera.main.ScreenToWorldPoint(InputManager.m_mousePosition)) - c.transform.position;
                        gS.GetComponent<BoxDamageToEnemy>().dir = dir.normalized;
                        break;
                    case SkillActive.Active.HoiMau:
                        c.TakeHealth((int)item.amount);
                        item.timeEffectSecond = item.timeEffect;
                        break;
                    case SkillActive.Active.Stun:
                        break;
                    case SkillActive.Active.Slow:
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
