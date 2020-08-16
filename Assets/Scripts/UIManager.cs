using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instace;

    Charater charater;

    [SerializeField]
    GameObject gTextDamage;

    [SerializeField]
    GameObject gTextHeal;

    [SerializeField]
    GameObject gTextMoney;

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

    [SerializeField]
    Image imageSkillQ;
    [SerializeField]
    Image imageSkillW;
    [SerializeField]
    Image imageSkillE;
    [SerializeField]
    Image imageSkillR;
    [SerializeField]
    Image imageSkillD;
    [SerializeField]
    Image imageSkillF;

    [SerializeField]
    Text textPhysicsDamage;
    [SerializeField]
    Text textMagicDamage;
    [SerializeField]
    Text textArrmor;
    [SerializeField]
    Text textMagicResistance;
    [SerializeField]
    Text textAttackSpeed;
    [SerializeField]
    Text textCooldown;
    [SerializeField]
    Text textCritRate;
    [SerializeField]
    Text textMoveSpeed;

    [SerializeField]
    Text textMoney;

    [SerializeField]
    Text textSelectedPhysicsDamage;
    [SerializeField]
    Text textSelectedMagicDamage;
    [SerializeField]
    Text textSelectedArrmor;
    [SerializeField]
    Text textSelectedMagicResistance;
    [SerializeField]
    Text textSelectedAttackSpeed;
    [SerializeField]
    Text textSelectedCooldown;
    [SerializeField]
    Text textSelectedCritRate;
    [SerializeField]
    Text textSelectedMoveSpeed;


    [SerializeField]
    Item itemSelected;
    [SerializeField]
    SlotInventory inventorySelected;
    [SerializeField]
    Image imageSelectItem;
    [SerializeField]
    Text textSelectedNameItem;
    [SerializeField]
    Text textSelectedCostItem;
    [SerializeField]
    Text textSelectedInfoItem;

    [SerializeField]
    Button buyBtn;

    [SerializeField]
    Button sellBtn;
    [SerializeField]
    Button undoBtn;

    [SerializeField]
    GameObject containBuildInto;
    [SerializeField]
    GameObject containAllItem;
    [SerializeField]
    GameObject containRequires;

    public GameObject imageItem1;
    public GameObject imageItem2;
    public GameObject imageItem3;
    public GameObject imageItem4;
    public GameObject imageItem5;
    public GameObject imageItem6;

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
    }

    public GameObject panelShop;

    public void ClickShop()
    {
        panelShop.SetActive(!panelShop.activeInHierarchy);
    }

    public void MakeTextDamage(Vector2 position, string str)
    {
        Vector2 rPos = Camera.main.WorldToScreenPoint(position);
        GameObject t = Instantiate(gTextDamage, rPos, Quaternion.identity, GameObject.Find("Canvas").transform);
        t.GetComponent<Text>().text = str;
        t.GetComponent<TextOnUI>().pos = rPos;

        StartCoroutine(FadeOut(t.GetComponent<Text>(), 1));
    }

    public void MakeTextHeal(Vector2 position, string str)
    {
        Vector2 rPos = Camera.main.WorldToScreenPoint(position);
        GameObject t = Instantiate(gTextHeal, rPos, Quaternion.identity, GameObject.Find("Canvas").transform);
        t.GetComponent<Text>().text = str;
        t.GetComponent<TextOnUI>().pos = rPos;

        StartCoroutine(FadeOut(t.GetComponent<Text>(), 1));
    }

    public void MakeTextMoney(Vector2 position, string str)
    {
        Vector2 rPos = Camera.main.WorldToScreenPoint(position);
        GameObject t = Instantiate(gTextMoney, rPos, Quaternion.identity, GameObject.Find("Canvas").transform);
        t.GetComponent<Text>().text = str;
        t.GetComponent<TextOnUI>().pos = rPos;

        StartCoroutine(FadeOut(t.GetComponent<Text>(), 3));
    }

    public void UpdateImage(Champion champion)
    {
        healthBarOnPlayer.fillAmount = (float)champion.propertyChampion.healthPointSecond / (float)champion.propertyChampion.healthPoint_Real;
        healthBar.fillAmount = (float)champion.propertyChampion.healthPointSecond / (float)champion.propertyChampion.healthPoint_Real;
        textHealth.text = (int)champion.propertyChampion.healthPointSecond + "/" + (int)champion.propertyChampion.healthPoint_Real;

        imageSkillQ.fillAmount = champion.timeCoolDownSkillQSecond / champion.timeCoolDownSkillQ;
        imageSkillW.fillAmount = champion.timeCoolDownSkillWSecond / champion.timeCoolDownSkillW;
        imageSkillE.fillAmount = champion.timeCoolDownSkillESecond / champion.timeCoolDownSkillE;
        imageSkillR.fillAmount = champion.timeCoolDownSkillRSecond / champion.timeCoolDownSkillR;

        textPhysicsDamage.text = champion.propertyChampion.physicsDamage_Real.ToString();

        textMagicDamage.text = champion.propertyChampion.magicDamage_Real.ToString();

        textArrmor.text = champion.propertyChampion.arrmor_Real.ToString();

        textMagicResistance.text = champion.propertyChampion.magicResistance_Real.ToString();

        textAttackSpeed.text = Math.Round(champion.propertyChampion.attackSpeed_Real, 3).ToString();

        textCooldown.text = champion.propertyChampion.cooldown_Real.ToString() + "%";

        textCritRate.text = champion.propertyChampion.critRate_Real.ToString() + "%";

        textMoveSpeed.text = champion.propertyChampion.moveSpeed_Real.ToString();

        textMoney.text = ((int)champion.propertyChampion.money).ToString();
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
                Creep c = g.GetComponent<Creep>();
                imageSelected.sprite = (c.team == Team.Blue ?
                    spriteMinionCasterBlue : spriteMinionCasterRed);

                textSelectedPhysicsDamage.text = c.property.damage.ToString();
                textSelectedMagicDamage.text = "0";
                textSelectedArrmor.text = c.property.arrmor.ToString();
                textSelectedMagicResistance.text = c.property.magicResistance.ToString(); ;
                textSelectedAttackSpeed.text = c.property.attackSpeed.ToString();
                textSelectedCooldown.text = "0%";
                textSelectedCritRate.text = "0%";
                textSelectedMoveSpeed.text = c.property.moveSpeed.ToString();
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
        //gInfoSkill.SetActive(true);

        //Skill skill = g.GetComponent<Skill>();
        //tNameSkill.text = (skill.typeSkill == Skill.TypeSkill.passive ? "(p) " : "") + skill.nameSkill;
        //tCostSkill.text = skill.costSkill + " ";
        //switch (skill.typeCost)
        //{
        //    case Skill.TypeCost.Mana:
        //        tCostSkill.text += "Năng lượng";
        //        break;
        //    case Skill.TypeCost.Health:
        //        tCostSkill.text += "Máu";
        //        break;
        //    case Skill.TypeCost.None:
        //        tCostSkill.text += "Không tiêu hao";
        //        break;
        //    default:
        //        break;
        //}
        //tCooldown.text = (skill.timeCooldown == 0 ? "" : skill.timeCooldown + " giây");
        //tInfoSkill.text = skill.info;
    }

    public void MouseExitInfo()
    {
        gInfoSkill.SetActive(false);
    }

    void SetPanelBuildInto(Item item)
    {
        foreach (Transform t in containBuildInto.transform)
        {
            Destroy(t.gameObject);
        }

        foreach (var parent in item.parentItem)
        {
            GameObject g = Instantiate(parent.gameObject, containBuildInto.transform);
            g.GetComponent<Item>().where = Item.Where.BuildInto;
        }
    }

    void SetPanelRequires(Item item)
    {
        //Destroy
        foreach (Transform t in containRequires.transform)
        {
            Destroy(t.gameObject);
        }

        //Set
        GameObject p = Instantiate(Resources.Load(item.typeRequires.ToString()) as GameObject, containRequires.transform);
        p.name = item.typeRequires.ToString();

        switch (item.typeRequires)
        {
            case Item.TypeRequires.T1:
                GameObject gT1 = Instantiate(item.gameObject,p.transform);
                gT1.GetComponent<Item>().where = Item.Where.Requires;
                break;
            case Item.TypeRequires.T11:
                GameObject gT1A = Instantiate(item.gameObject, p.transform.GetChild(0));
                gT1A.GetComponent<Item>().where = Item.Where.Requires;

                GameObject gT1B = Instantiate(item.childItem[0].gameObject, p.transform.GetChild(1));
                gT1B.GetComponent<Item>().where = Item.Where.Requires;
                break;
            case Item.TypeRequires.T12:
                break;
            case Item.TypeRequires.T13:
                break;
            case Item.TypeRequires.T12_11:
                break;
            case Item.TypeRequires.T12_12:
                break;
            case Item.TypeRequires.T12_13:
                break;
            case Item.TypeRequires.T12_1:
                break;
            case Item.TypeRequires.T11_12:
                break;
            case Item.TypeRequires.T1_11_1:
                break;
            case Item.TypeRequires.T13_12:
                break;
            case Item.TypeRequires.T1_1_11:
                break;
            case Item.TypeRequires.T12_1_1:
                break;
            case Item.TypeRequires.T12_1_12:
                break;
            case Item.TypeRequires.T13_11_1:
                break;
            case Item.TypeRequires.T11_11:
                GameObject gT11_11A = Instantiate(item.gameObject, p.transform.GetChild(0));
                gT11_11A.GetComponent<Item>().where = Item.Where.Requires;

                GameObject gT11_11B = Instantiate(item.childItem[0].gameObject, p.transform.GetChild(1));
                gT11_11B.GetComponent<Item>().where = Item.Where.Requires;

                GameObject gT11_11C = Instantiate(item.childItem[1].gameObject, p.transform.GetChild(2));
                gT11_11C.GetComponent<Item>().where = Item.Where.Requires;

                GameObject gT11_11D = Instantiate(item.childItem[0].childItem[0].gameObject, p.transform.GetChild(3));
                gT11_11D.GetComponent<Item>().where = Item.Where.Requires;

                GameObject gT11_11E = Instantiate(item.childItem[1].childItem[0].gameObject, p.transform.GetChild(4));
                gT11_11E.GetComponent<Item>().where = Item.Where.Requires;
                break;
            default:
                break;
        }
    }

    void SetPanelInfo(Item item)
    {
        imageSelectItem.sprite = item.sprite;
        textSelectedNameItem.text = item.nameItem;
        textSelectedCostItem.text = item.GetAllCost().ToString();
        textSelectedInfoItem.text = item.infoItem;
    }

    public void SelectedItem(Item item)
    {
        if (itemSelected)
        {
            itemSelected.transform.GetChild(0).GetComponent<Image>().color = new Color(.35f, .35f, .35f, 1);
        }

        //item.GetName(0);

        switch (item.where)
        {
            case Item.Where.Inventory:
                break;
            case Item.Where.ListItem:
                itemSelected = item;
                itemSelected.transform.GetChild(0).GetComponent<Image>().color = Color.white;

                SetPanelInfo(item);
                SetPanelBuildInto(item);
                SetPanelRequires(item);

                buyBtn.interactable = (charater.champion.propertyChampion.money >= itemSelected.GetAllCost()) ? true : false;
                break;
            case Item.Where.BuildInto:
                foreach (Transform t in containAllItem.transform)
                {
                    if (t.GetComponent<Item>().nameItem == item.nameItem)
                    {
                        itemSelected = t.GetComponent<Item>();
                        t.GetChild(0).GetComponent<Image>().color = Color.white;

                        SetPanelInfo(t.GetComponent<Item>());
                        SetPanelBuildInto(t.GetComponent<Item>());
                        SetPanelRequires(t.GetComponent<Item>());

                        buyBtn.interactable = (charater.champion.propertyChampion.money >= t.GetComponent<Item>().GetAllCost()) ? true : false;
                    }
                }
                break;
            case Item.Where.Requires:
                itemSelected = item;
                itemSelected.transform.GetChild(0).GetComponent<Image>().color = Color.white;

                SetPanelInfo(item);
                SetPanelBuildInto(item);

                buyBtn.interactable = (charater.champion.propertyChampion.money >= itemSelected.GetAllCost()) ? true : false;
                break;
            default:
                break;
        }
    }

    public void SelectedInventory(SlotInventory slot)
    {
        sellBtn.interactable = (slot.item ? true : false);
        if (slot.item)
        {
            inventorySelected = slot;
            inventorySelected.transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
        else
        {
            inventorySelected = null;
        }
    }

    public void BuyBtn()
    {
        if (itemSelected && charater.champion.propertyChampion.money >= itemSelected.GetAllCost())
        {
            if (!charater.champion.CanAdd())
            {
                return;
            }
            Item newItem = itemSelected.Buy();
            int i = charater.champion.AddItem(newItem);
            if (i != 0)
            {
                newItem.indexInventory = i;
                charater.champion.propertyChampion.money -= itemSelected.GetAllCost();
                if (itemSelected.type == Item.Type.Normal)
                {
                    itemSelected.Use(i);
                }

                charater.champion.propertyChampion.UpdateValue();
            }
        }
    }

    public void SellBtn()
    {
        if (inventorySelected)
        {
            inventorySelected.item.Sell();

            Color cBackground = new Color(0, 0, 0, 0);
            Color cImage = new Color(1, 1, 1, 0);
            inventorySelected.transform.GetChild(0).GetComponent<Image>().color = cBackground;
            inventorySelected.transform.GetChild(1).GetComponent<Image>().color = cImage;

            inventorySelected.item = null;

            inventorySelected = null;
        }

        //if (charater.champion.tuiDo.ItemSelected)
        //{
        //    charater.champion.tuiDo.ItemSelected.Sell();

        //    Color cBackground = new Color(0, 0, 0, 0);
        //    Color cImage = new Color(1, 1, 1, 0);
        //    charater.champion.tuiDo.ItemSelected.imgTarget.transform.GetChild(0).GetComponent<Image>().color = cBackground;
        //    charater.champion.tuiDo.ItemSelected.imgTarget.transform.GetChild(1).GetComponent<Image>().color = cImage;

        //    //print(Convert.ToInt32(charater.champion.tuiDo.ItemSelected.imgTarget.name));

        //    charater.champion.tuiDo.item[Convert.ToInt32(charater.champion.tuiDo.ItemSelected.imgTarget.name)] = null;
        //}
    }
}
