using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instace;

    Charater charater;

    [SerializeField]
    GameObject textDamage;

    [SerializeField]
    GameObject textMoney;

    [SerializeField]
    Image healthBarOnPlayer;

    [SerializeField]
    Image healthBar;

    [SerializeField]
    Text textHealth;

    [Header("Selected Object")]
    [SerializeField]
    GameObject gSelected;

    [SerializeField]
    Image imageSelected;

    [Header("Icon")]
    [SerializeField]
    Sprite spriteMinionCasterBlue;
    [SerializeField]
    Sprite spriteMinionCasterRed;
    [SerializeField]
    Sprite spriteTurretBlue;
    [SerializeField]
    Sprite spriteTurretRed;

    [Header("Skill")]
    [SerializeField]
    GameObject gInfoSkill;
    [SerializeField]
    Text tNameSkill;
    [SerializeField]
    Text tCostSkill;
    [SerializeField]
    Text tInfoSkill;
    [SerializeField]
    Text tCooldown;

    private void Awake()
    {
        if (!instace)
        {
            instace = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        charater = FindObjectOfType<Charater>();
        UpdatePlayer(charater);
    }

    public GameObject panelShop;

    public void ClickShop()
    {
        panelShop.SetActive(!panelShop.activeInHierarchy);
    }

    public void MakeTextDamage(Vector2 position, string str)
    {
        Vector2 rPos = Camera.main.WorldToScreenPoint(position);
        GameObject t = Instantiate(textDamage, rPos, Quaternion.identity, GameObject.Find("Canvas").transform);
        t.GetComponent<Text>().text = str;
        t.GetComponent<TextOnUI>().pos = rPos;

        StartCoroutine(FadeOut(t.GetComponent<Text>(), 1));
    }

    public void MakeTextMoney(Vector2 position, string str)
    {
        Vector2 rPos = Camera.main.WorldToScreenPoint(position);
        GameObject t = Instantiate(textMoney, rPos, Quaternion.identity, GameObject.Find("Canvas").transform);
        t.GetComponent<Text>().text = str;
        t.GetComponent<TextOnUI>().pos = rPos;

        StartCoroutine(FadeOut(t.GetComponent<Text>(), 3));
    }

    public void UpdatePlayer(Charater charater)
    {
        healthBarOnPlayer.fillAmount = (float)charater.currentHealth / (float)charater.property.healthPoint;
        healthBar.fillAmount = (float)charater.currentHealth / (float)charater.property.healthPoint;
        textHealth.text = charater.currentHealth + "/" + charater.property.healthPoint;
    }

    public IEnumerator FadeOut(Text t, float duration)
    {
        int index = 0;

        Color c = t.color;

        while (index < 1000 && c.a > 0)
        {
            c.a -= 1 / (duration / Time.deltaTime);
            if (t)
            {
                t.color = c;
            }
            index++;
            yield return null;
        }
    }

    public void SetInfoPanel(GameObject g)
    {
        if (g != null)
        {
            gSelected.SetActive(true);
            if (g.GetComponent<Creep>())
            {
                imageSelected.sprite = (g.GetComponent<Creep>().team == Team.Blue ?
                    spriteMinionCasterBlue : spriteMinionCasterRed);
            }
            else if (g.GetComponent<Creep>())
            {
                imageSelected.sprite = (g.GetComponent<Turret>().team == Team.Blue ?
                    spriteTurretBlue : spriteTurretRed);
            }
        }
        else
        {
            gSelected.SetActive(false);
        }
    }

    public void MouseEnterInfo(GameObject g)
    {
        gInfoSkill.SetActive(true);

        Skill skill = g.GetComponent<Skill>();
        tNameSkill.text = (skill.typeSkill == Skill.TypeSkill.passive ? "(p) " : "") + skill.nameSkill;
        tCostSkill.text = skill.costSkill + " ";
        switch (skill.typeCost)
        {
            case Skill.TypeCost.Mana:
                tCostSkill.text += "Năng lượng";
                break;
            case Skill.TypeCost.Health:
                tCostSkill.text += "Máu";
                break;
            case Skill.TypeCost.None:
                tCostSkill.text += "Không tiêu hao";
                break;
            default:
                break;
        }
        tCooldown.text = (skill.timeCooldown == 0 ? "" : skill.timeCooldown + " giây");
        tInfoSkill.text = skill.info;
    }

    public void MouseExitInfo()
    {
        gInfoSkill.SetActive(false);
    }
}
