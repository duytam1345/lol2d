using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

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
    Image manaBarOnPlayer;

    [SerializeField]
    Image healthBar;
    [SerializeField]
    Image manaBar;

    [SerializeField]
    Text textHealth;
    [SerializeField]
    Text textMana;

    [Header("Selected Object")]
    [SerializeField]
    GameObject gSelected;

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
    Sprite minionCasterBlue;
    [SerializeField]
    Sprite minionCasterRed;

    [Header("UI")]

    public Image imageAvatar;
    public GameObject barTopHealth;

    public GameObject prefabDestroy;

    public GameObject panelKDA;

    public GameObject prefabHealthBarChampion;

    public Text textTimeGame;

    [Header("Hiển thị hiệu ứng hiện tại")]
    [SerializeField]
    GameObject prefabSlot;
    [SerializeField]
    GameObject containListEffect;
    public Dictionary<string, SlotListEffect> listEffect = new Dictionary<string, SlotListEffect>();

    [Header("Hiển thị trạng thái hiện tại")]
    [SerializeField]
    GameObject containListState;

    [Header("Hiển thị bảng nâng cấp chiêu")]
    public GameObject panelUpgradeSkill;
    public Button btnQ;
    public Button btnW;
    public Button btnE;
    public Button btnR;
    public Transform listNodeQ;
    public Transform listNodeW;
    public Transform listNodeE;
    public Transform listNodeR;

    [Header("Hiển thị hồi chiêu")]

    public Image imageSkillQBg;
    public Image imageSkillQ;
    public Image imageSkillWBg;
    public Image imageSkillW;
    public Image imageSkillEBg;
    public Image imageSkillE;
    public Image imageSkillRBg;
    public Image imageSkillR;

    [SerializeField]
    Image imageSpellD;
    [SerializeField]
    Image imageSpellF;


    [Header("Hiển thị thông tin tướng")]

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

    [Header("Hiển thị tiền và trang bị")]

    [SerializeField]
    Text textMoney;
    [SerializeField]
    Text textMoneyInShopPanel;

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

    //item tren shop
    public GameObject imageItem1;
    public GameObject imageItem2;
    public GameObject imageItem3;
    public GameObject imageItem4;
    public GameObject imageItem5;
    public GameObject imageItem6;

    //item tren bang thong tin player
    public GameObject imageItem1_UIChamp;
    public GameObject imageItem2_UIChamp;
    public GameObject imageItem3_UIChamp;
    public GameObject imageItem4_UIChamp;
    public GameObject imageItem5_UIChamp;
    public GameObject imageItem6_UIChamp;

    public List<SlotInPanelKDA> slotInPanelKDAs = new List<SlotInPanelKDA>();

    public Transform containPanelKDABlue;
    public Transform containPanelKDARed;
    public GameObject prefabSlotKDA;

    [Header("Biến về")]
    [SerializeField]
    GameObject prefabReCallUI;
    public GameObject currentReCallUI;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        charater = FindObjectOfType<Charater>();

        for (int i = 0; i < Mananger.instance.championInGame.Count; i++)
        {
            GameObject g = Instantiate(prefabSlotKDA,
                Mananger.instance.championInGame[i].team == Team.Blue ? containPanelKDABlue : containPanelKDARed);
            g.GetComponent<SlotInPanelKDA>().c = Mananger.instance.championInGame[i];

            slotInPanelKDAs.Add(g.GetComponent<SlotInPanelKDA>());

            if (Mananger.instance.championInGame[i].isBot)
            {
                GameObject ui = Instantiate(prefabHealthBarChampion, GameObject.Find("Canvas").transform);
                ui.GetComponent<UIFollowTarget>().gTarget = Mananger.instance.championInGame[i].gameObject;
                Mananger.instance.championInGame[i].uI = ui.GetComponent<UIFollowTarget>();
            }
        }
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

        manaBarOnPlayer.fillAmount = (float)champion.propertyChampion.manaPointSecond / (float)champion.propertyChampion.manaPoint_Real;
        manaBar.fillAmount = (float)champion.propertyChampion.manaPointSecond / (float)champion.propertyChampion.manaPoint_Real;
        textMana.text = (int)champion.propertyChampion.manaPointSecond + "/" + (int)champion.propertyChampion.manaPoint_Real;

        imageSkillQ.fillAmount = champion.timeCoolDownSkillQSecond / champion.timeCoolDownSkillQ;
        imageSkillW.fillAmount = champion.timeCoolDownSkillWSecond / champion.timeCoolDownSkillW;
        imageSkillE.fillAmount = champion.timeCoolDownSkillESecond / champion.timeCoolDownSkillE;
        imageSkillR.fillAmount = champion.timeCoolDownSkillRSecond / champion.timeCoolDownSkillR;

        imageSpellD.fillAmount = champion.spellD.timeCooldownSecond / champion.spellD.timeCooldown;
        imageSpellF.fillAmount = champion.spellF.timeCooldownSecond / champion.spellF.timeCooldown;

        textPhysicsDamage.text = champion.propertyChampion.physicsDamage_Real.ToString();
        textMagicDamage.text = champion.propertyChampion.magicDamage_Real.ToString();
        textArrmor.text = champion.propertyChampion.arrmor_Real.ToString();
        textMagicResistance.text = champion.propertyChampion.magicResistance_Real.ToString();
        textAttackSpeed.text = Math.Round(champion.propertyChampion.attackSpeed_Real, 3).ToString();
        textCooldown.text = champion.propertyChampion.cooldown_Real.ToString() + "%";
        textCritRate.text = champion.propertyChampion.critRate_Real.ToString() + "%";
        textMoveSpeed.text = champion.propertyChampion.moveSpeed_Real.ToString();

        textMoney.text = ((int)champion.propertyChampion.money).ToString();
        textMoneyInShopPanel.text = ((int)champion.propertyChampion.money).ToString();
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
            gSelected.GetComponent<SelectedObject>().g = g;
        }
        else
        {
            gSelected.SetActive(false);
        }
    }

    public void MouseEnterInfo(GameObject game)
    {
        gInfoSkill.SetActive(true);
        Champion c = charater.champion;

        if (game.name == "Skill Passive")
        {
            tNameSkill.text = c.namePassive;
            tCostSkill.text = "";
            tCooldown.text = "";
            tInfoSkill.text = c.infoPassive;
        }
        else if (game.name == "Skill Q")
        {
            tNameSkill.text = c.nameSkillQ;
            tCostSkill.text = c.costSkillQ;
            tCooldown.text = c.timeCoolDownSkillQ + " giây";
            tInfoSkill.text = c.infoSkillQ;
        }
        else if (game.name == "Skill W")
        {
            tNameSkill.text = c.nameSkillW;
            tCostSkill.text = c.costSkillW;
            tCooldown.text = c.timeCoolDownSkillW + " giây";
            tInfoSkill.text = c.infoSkillW;
        }
        else if (game.name == "Skill E")
        {
            tNameSkill.text = c.nameSkillE;
            tCostSkill.text = c.costSkillE;
            tCooldown.text = c.timeCoolDownSkillE + " giây";
            tInfoSkill.text = c.infoSkillE;
        }
        else if (game.name == "Skill R")
        {
            tNameSkill.text = c.nameSkillR;
            tCostSkill.text = c.costSkillR;
            tCooldown.text = c.timeCoolDownSkillR + " giây";
            tInfoSkill.text = c.infoSkillR;
        }
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
                GameObject gT1 = Instantiate(item.gameObject, p.transform);
                gT1.GetComponent<Item>().where = Item.Where.Requires;
                break;
            case Item.TypeRequires.T11:
                GameObject gT1A = Instantiate(item.gameObject, p.transform.GetChild(0));
                gT1A.GetComponent<Item>().where = Item.Where.Requires;

                GameObject gT1B = Instantiate(item.childItem[0].gameObject, p.transform.GetChild(1));
                gT1B.GetComponent<Item>().where = Item.Where.Requires;
                break;
            case Item.TypeRequires.T12:
                GameObject gT12A = Instantiate(item.gameObject, p.transform.GetChild(0));
                gT12A.GetComponent<Item>().where = Item.Where.Requires;

                GameObject gT12B = Instantiate(item.childItem[0].gameObject, p.transform.GetChild(1));
                gT12B.GetComponent<Item>().where = Item.Where.Requires;

                GameObject gT12C = Instantiate(item.childItem[1].gameObject, p.transform.GetChild(2));
                gT12C.GetComponent<Item>().where = Item.Where.Requires;
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

            //Set hinh trang bi
            #region
            Color c = new Color(1, 1, 1, 0);
            if (!charater.champion.item1.item)
            {
                imageItem1_UIChamp.transform.GetChild(0).GetComponent<Image>().color = c;
                imageItem1_UIChamp.transform.GetChild(0).GetComponent<Image>().sprite = null;
            }
            if (!charater.champion.item2.item)
            {
                imageItem2_UIChamp.transform.GetChild(0).GetComponent<Image>().color = c;
                imageItem2_UIChamp.transform.GetChild(0).GetComponent<Image>().sprite = null;
            }
            if (!charater.champion.item3.item)
            {
                imageItem3_UIChamp.transform.GetChild(0).GetComponent<Image>().color = c;
                imageItem3_UIChamp.transform.GetChild(0).GetComponent<Image>().sprite = null;
            }
            if (!charater.champion.item4.item)
            {
                imageItem4_UIChamp.transform.GetChild(0).GetComponent<Image>().color = c;
                imageItem4_UIChamp.transform.GetChild(0).GetComponent<Image>().sprite = null;
            }
            if (!charater.champion.item5.item)
            {
                imageItem5_UIChamp.transform.GetChild(0).GetComponent<Image>().color = c;
                imageItem5_UIChamp.transform.GetChild(0).GetComponent<Image>().sprite = null;
            }
            if (!charater.champion.item6.item)
            {
                imageItem6_UIChamp.transform.GetChild(0).GetComponent<Image>().color = c;
                imageItem6_UIChamp.transform.GetChild(0).GetComponent<Image>().sprite = null;
            }
            #endregion
        }
    }

    public void MakeReCallBar()
    {
        if (currentReCallUI)
        {
            Destroy(currentReCallUI);
        }

        GameObject g = Instantiate(prefabReCallUI, GameObject.Find("Canvas").transform);
        currentReCallUI = g;
    }

    public void CreateSlotEffect(string key, IconEffect iconEffect)
    {
        GameObject g = Instantiate(prefabSlot, containListEffect.transform);
        g.GetComponent<SlotListEffect>().iconEffect = iconEffect;

        listEffect.Add(key, g.GetComponent<SlotListEffect>());
    }

    public void LoadPanelUpgradeSkill(Champion c)
    {
        btnQ.interactable = false;
        btnW.interactable = false;
        btnE.interactable = false;
        btnR.interactable = false;

        if (c.propertyChampion.level < 6)
        {
            btnQ.interactable = true;
            btnW.interactable = true;
            btnE.interactable = true;
        }
        else if (c.propertyChampion.level >= 6 && c.propertyChampion.level < 11)
        {
            if (c.levelSkillQ < 5)
            {
                btnQ.interactable = true;
            }
            if (c.levelSkillW < 5)
            {
                btnW.interactable = true;
            }
            if (c.levelSkillE < 5)
            {
                btnE.interactable = true;
            }
            if (c.levelSkillR < 1)
            {
                btnR.interactable = true;
            }
        }
        else if (c.propertyChampion.level >= 11 && c.propertyChampion.level < 16)
        {
            if (c.levelSkillQ < 5)
            {
                btnQ.interactable = true;
            }
            if (c.levelSkillW < 5)
            {
                btnW.interactable = true;
            }
            if (c.levelSkillE < 5)
            {
                btnE.interactable = true;
            }
            if (c.levelSkillR < 2)
            {
                btnR.interactable = true;
            }
        }
        else if (c.propertyChampion.level >= 16 && c.propertyChampion.level < 19)
        {
            if (c.levelSkillQ < 5)
            {
                btnQ.interactable = true;
            }
            if (c.levelSkillW < 5)
            {
                btnW.interactable = true;
            }
            if (c.levelSkillE < 5)
            {
                btnE.interactable = true;
            }
            if (c.levelSkillR < 3)
            {
                btnR.interactable = true;
            }
        }

        StartCoroutine(ShowPanelUpgradeSkill(true));
    }

    IEnumerator ShowPanelUpgradeSkill(bool isShowUp)
    {
        if (isShowUp)
        {
            Vector2 v = panelUpgradeSkill.GetComponent<RectTransform>().sizeDelta;
            while (v.y < 200)
            {
                v.y += 5;
                panelUpgradeSkill.GetComponent<RectTransform>().sizeDelta = v;
                yield return null;
            }
        }
        else
        {
            Vector2 v = panelUpgradeSkill.GetComponent<RectTransform>().sizeDelta;
            while (v.y > 100)
            {
                v.y -= 5;
                panelUpgradeSkill.GetComponent<RectTransform>().sizeDelta = v;
                yield return null;
            }
        }
    }

    public void BtnUpgradeSkill(string s)
    {
        if (charater.champion.leftPointSkill > 0)
        {
            if (s == "Q")
            {
                charater.champion.levelSkillQ++;
            }
            else if (s == "W")
            {
                charater.champion.levelSkillW++;
            }
            else if (s == "E")
            {
                charater.champion.levelSkillE++;
            }
            else if (s == "R")
            {
                charater.champion.levelSkillR++;
            }
            charater.champion.leftPointSkill--;
            UpdateNodeSkill();
            charater.champion.SetSkill();
            if (charater.champion.leftPointSkill > 0)
            {
                LoadPanelUpgradeSkill(charater.champion);
            }
            else
            {
                StartCoroutine(ShowPanelUpgradeSkill(false));
            }
        }
    }

    public void UpdateNodeSkill()
    {
        for (int i = 0; i < charater.champion.levelSkillQ; i++)
        {
            listNodeQ.GetChild(i).GetChild(0).gameObject.SetActive(true);
        }
        for (int i = 0; i < charater.champion.levelSkillW; i++)
        {
            listNodeW.GetChild(i).GetChild(0).gameObject.SetActive(true);
        }
        for (int i = 0; i < charater.champion.levelSkillE; i++)
        {
            listNodeE.GetChild(i).GetChild(0).gameObject.SetActive(true);
        }
        for (int i = 0; i < charater.champion.levelSkillR; i++)
        {
            listNodeR.GetChild(i).GetChild(0).gameObject.SetActive(true);
        }
    }

    public void CreateTextDestroyed(GameObject a, GameObject b)
    {
        GameObject g = Instantiate(prefabDestroy, GameObject.Find("Canvas").transform);

        if (a.GetComponent<Champion>())
        {
            g.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = a.GetComponent<Champion>().spriteAvatar;
            if (!a.GetComponent<Champion>().isBot)
            {
                if (b.GetComponent<Turret>())
                {
                    g.transform.GetChild(0).GetComponent<Text>().text = "Trụ địch đã bị phá hủy!";
                    //g.transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite = b.GetComponent<Turret>().spriteAvatar;
                }
                else if (b.GetComponent<Champion>())
                {
                    g.transform.GetChild(0).GetComponent<Text>().text = "Kẻ địch đã bị bạn giết!";
                    g.transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite = b.GetComponent<Champion>().spriteAvatar;
                }
            }
            else
            {
                if (b.GetComponent<Turret>())
                {
                    g.transform.GetChild(0).GetComponent<Text>().text = "Trụ đồng minh đã bị phá hủy!";
                    //g.transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite = b.GetComponent<Turret>().spriteAvatar;
                }
                else if (b.GetComponent<Champion>())
                {
                    g.transform.GetChild(0).GetComponent<Text>().text = "Bạn đã bị hạ gục!";
                    g.transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite = b.GetComponent<Champion>().spriteAvatar;
                }
            }
        }
        else if (a.GetComponent<Turret>())
        {
            //g.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = a.GetComponent<Turret>().spriteAvatar;
            if (b.GetComponent<Champion>())
            {
                g.transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite = b.GetComponent<Champion>().spriteAvatar;
                if (b.GetComponent<Champion>().isBot)
                {
                    g.transform.GetChild(0).GetComponent<Text>().text = "Kẻ địch đã bị trụ bắn hạ!";
                }
                else
                {
                    g.transform.GetChild(0).GetComponent<Text>().text = "Bạn đã bị trụ hạ gục!";
                }
            }
        }
    }

    public void SetTabPanel(bool b)
    {
        if (b)
        {
            panelKDA.SetActive(true);

            foreach (var item in slotInPanelKDAs)
            {
                item.Set();
            }
        }
        else
        {
            panelKDA.SetActive(false);
        }
    }
}
