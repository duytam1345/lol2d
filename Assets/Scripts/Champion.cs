using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PropertyChampion
{
    public int exp;
    public int level;
    public float money;

    public float healthEachLv;
    public float healthRegenEachLv;
    public float manaEachLv;
    public float manRegenEachLv;
    public float attackDamageEachLv;
    public float attackSpeedEachLv;
    public float bonusAttackAtLv1;
    public float arrmorEachLv;
    public float magicResistanceEachLv;

    public float attackRange;

    public float attackSpeed;
    public Dictionary<string, float> attackSpeedExtra = new Dictionary<string, float>();
    public float attackSpeed_Real;

    public float healthPoint;
    public float healthPointSecond;
    public Dictionary<string, float> healthPointExtra = new Dictionary<string, float>();
    public float healthPoint_Real;

    public float healthRegen;
    public Dictionary<string, float> healthRegenExtra = new Dictionary<string, float>();
    public float healthRegen_Real;

    public float manaPoint;
    public float manaPointSecond;
    public Dictionary<string, float> manaPointExtra = new Dictionary<string, float>();
    public float manaPoint_Real;

    public float manaRegen;
    public Dictionary<string, float> manaRegenExtra = new Dictionary<string, float>();
    public float manaRegen_Real;

    public int moveSpeed;
    public Dictionary<string, float> moveSpeedExtra = new Dictionary<string, float>();
    public int moveSpeed_Real;

    public int physicsDamage;
    public Dictionary<string, float> physicsDamageExtra = new Dictionary<string, float>();
    public int physicsDamage_Real;

    public int magicDamage;
    public Dictionary<string, float> magicDamageExtra = new Dictionary<string, float>();
    public int magicDamage_Real;

    public int arrmor;
    public Dictionary<string, float> arrmorExtra = new Dictionary<string, float>();
    public int arrmor_Real;

    public int magicResistance;
    public Dictionary<string, float> magicResistanceExtra = new Dictionary<string, float>();
    public int magicResistance_Real;

    public int cooldown;
    public Dictionary<string, float> cooldownExtra = new Dictionary<string, float>();
    public int cooldown_Real;

    public int critRate;
    public Dictionary<string, float> critRateExtra = new Dictionary<string, float>();
    public int critRate_Real;

    public void UpdateValue()
    {
        //Physics Damage
        physicsDamage_Real = physicsDamage;

        foreach (var item in physicsDamageExtra.Values)
        {
            physicsDamage_Real += (int)item;
        }

        //Magic Damage
        magicDamage_Real = magicDamage;

        foreach (var item in magicDamageExtra.Values)
        {
            magicDamage_Real += (int)item;
        }

        //Health
        healthPoint_Real = healthPoint;

        foreach (var item in healthPointExtra.Values)
        {
            healthPoint_Real += (int)item;
        }

        //Health Regen
        healthRegen_Real = healthRegen;

        foreach (var item in healthRegenExtra.Values)
        {
            healthRegen_Real += item;
        }

        //Mana
        manaPoint_Real = manaPoint;

        foreach (var item in manaPointExtra.Values)
        {
            manaPoint_Real += (int)item;
        }

        //Mana Regen
        manaRegen_Real = manaRegen;

        foreach (var item in manaRegenExtra.Values)
        {
            manaRegen_Real += item;
        }

        //Attack Speed
        attackSpeed_Real = attackSpeed;

        foreach (var item in attackSpeedExtra.Values)
        {
            attackSpeed_Real += (int)item;
        }

        //Move Speed
        moveSpeed_Real = moveSpeed;

        foreach (var item in moveSpeedExtra.Values)
        {
            moveSpeed_Real += (int)item;
        }

        //Arrmor
        arrmor_Real = arrmor;

        foreach (var item in arrmorExtra.Values)
        {
            arrmor_Real += (int)item;
        }

        //Magic resistance
        magicResistance_Real = magicResistance;

        foreach (var item in magicResistanceExtra.Values)
        {
            magicResistance_Real += (int)item;
        }

        //Attack Speed
        attackSpeed_Real = attackSpeed;
        float percent = bonusAttackAtLv1;
        foreach (var item in attackSpeedExtra.Values)
        {
            percent += item;
        }
        attackSpeed_Real += (attackSpeed / 100 * percent);

        //Cooldown
        cooldown_Real = cooldown;

        foreach (var item in cooldownExtra.Values)
        {
            cooldown_Real += (int)item;
        }

        //Crit Rate
        critRate_Real = critRate;

        foreach (var item in critRateExtra.Values)
        {
            critRate_Real += (int)item;
        }
    }
}
#region MyRegion

[System.Serializable]
public class TuiDo
{
    public Item ItemSelected;
    public Item[] item = new Item[6];
    public void Add(Item item)
    {

    }
    public void Remove(int index)
    {

    }
}
#endregion
public class Champion : MonoBehaviour
{
    private void Start()
    {
        BtnItem.KhiTuiDoClick += KhiChon1Item;

        SkillPassive();

        propertyChampion.UpdateValue();
    }
    public void KhiChon1Item(SlotInventory slot)
    {
        Color cDeselect = new Color(0, 0, 0, 0);
        Color cCelect = new Color(0, 0, 0, 1);
        item1.transform.GetChild(0).GetComponent<Image>().color = cDeselect;
        item2.transform.GetChild(0).GetComponent<Image>().color = cDeselect;
        item3.transform.GetChild(0).GetComponent<Image>().color = cDeselect;
        item4.transform.GetChild(0).GetComponent<Image>().color = cDeselect;
        item5.transform.GetChild(0).GetComponent<Image>().color = cDeselect;
        item6.transform.GetChild(0).GetComponent<Image>().color = cDeselect;
        slot.transform.GetChild(0).GetComponent<Image>().color = cCelect;

        UIManager.instace.SelectedInventory(slot);


        //var id = int.Parse(gameObject.name);
        //if (tuiDo != null && tuiDo.item != null && tuiDo.item[id])
        //{
        //    tuiDo.ItemSelected = tuiDo.item[id];
        //    tuiDo.ItemSelected.imgTarget = slot;
        //}
    }

    public PropertyChampion propertyChampion;

    public Team team;

    [Header("Thời Gian Hồi Chiêu")]

    public float timeCoolDownSkillQ;
    public float timeCoolDownSkillQSecond;
    public float timeCoolDownSkillW;
    public float timeCoolDownSkillWSecond;
    public float timeCoolDownSkillE;
    public float timeCoolDownSkillESecond;
    public float timeCoolDownSkillR;
    public float timeCoolDownSkillRSecond;

    [Header("Cấp Độ Chiêu")]
    [Range(0, 5)]
    public int levelSkillQ;
    [Range(0, 5)]
    public int levelSkillW;
    [Range(0, 5)]
    public int levelSkillE;
    [Range(0, 3)]
    public int levelSkillR;

    [SerializeField]
    Text textLevel;

    [Header("Item")]
    public SlotInventory item1;
    public SlotInventory item2;
    public SlotInventory item3;
    public SlotInventory item4;
    public SlotInventory item5;
    public SlotInventory item6;

    public Item ItemWard;
    public TuiDo tuiDo = new TuiDo();
    public virtual void SkillPassive() { }
    public virtual void SkillQ() { }
    public virtual void SkillW() { }
    public virtual void SkillE() { }
    public virtual void SkillR() { }
    public virtual void SkillD() { }
    public virtual void SkillF() { }

    [Header("Khác")]
    public float timePerSecond;
    public float timePerHalfSecond;

    public void PerSecond()
    {
        //Per second
        timePerSecond -= Time.deltaTime;
        if (timePerSecond <= 0)
        {
            //Money
            propertyChampion.money += 1.3f;

            timePerSecond = 1;
        }

        //Per Half Second
        timePerHalfSecond -= Time.deltaTime;
        if (timePerHalfSecond <= 0)
        {
            //Regen Health
            propertyChampion.healthPointSecond += propertyChampion.healthRegen_Real / 10;

            //Regen Mana
            propertyChampion.manaPointSecond += propertyChampion.manaRegen_Real / 10;

            timePerHalfSecond = .5f;
        }
    }

    public void TakeExp(int xp)
    {
        propertyChampion.exp += xp;

        if (propertyChampion.exp >= (CongThuc.ExpNextLevel(propertyChampion.level + 1) / 2))
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        propertyChampion.level++;
        textLevel.text = propertyChampion.level.ToString();
    }

    //public int AddItem(Item item)
    //{
    //    if (!tuiDo.item[0])
    //    {
    //        tuiDo.item[0] = item;

    //        Color cBackground = new Color(0, 0, 0, 1);
    //        Color cImage = new Color(1, 1, 1, 1);
    //        UIManager.instace.imageItem1.transform.GetChild(1).GetComponent<Image>().sprite = item.sprite;
    //        UIManager.instace.imageItem1.transform.GetChild(0).GetComponent<Image>().color = cBackground;
    //        UIManager.instace.imageItem1.transform.GetChild(1).GetComponent<Image>().color = cImage;
    //        return 1;
    //    }
    //    else if (!tuiDo.item[1])
    //    {
    //        tuiDo.item[1] = item;

    //        Color cBackground = new Color(0, 0, 0, 1);
    //        Color cImage = new Color(1, 1, 1, 1);
    //        UIManager.instace.imageItem2.transform.GetChild(1).GetComponent<Image>().sprite = item.sprite;
    //        UIManager.instace.imageItem2.transform.GetChild(0).GetComponent<Image>().color = cBackground;
    //        UIManager.instace.imageItem2.transform.GetChild(1).GetComponent<Image>().color = cImage;
    //        return 2;
    //    }
    //    else if (!tuiDo.item[2])
    //    {
    //        tuiDo.item[2] = item;

    //        Color cBackground = new Color(0, 0, 0, 1);
    //        Color cImage = new Color(1, 1, 1, 1);
    //        UIManager.instace.imageItem3.transform.GetChild(1).GetComponent<Image>().sprite = item.sprite;
    //        UIManager.instace.imageItem3.transform.GetChild(0).GetComponent<Image>().color = cBackground;
    //        UIManager.instace.imageItem3.transform.GetChild(1).GetComponent<Image>().color = cImage;
    //        return 3;
    //    }
    //    else if (!tuiDo.item[3])
    //    {
    //        tuiDo.item[3] = item;

    //        Color cBackground = new Color(0, 0, 0, 1);
    //        Color cImage = new Color(1, 1, 1, 1);
    //        UIManager.instace.imageItem4.transform.GetChild(1).GetComponent<Image>().sprite = item.sprite;
    //        UIManager.instace.imageItem4.transform.GetChild(0).GetComponent<Image>().color = cBackground;
    //        UIManager.instace.imageItem4.transform.GetChild(1).GetComponent<Image>().color = cImage;
    //        return 4;
    //    }
    //    else if (!tuiDo.item[4])
    //    {
    //        tuiDo.item[4] = item;

    //        Color cBackground = new Color(0, 0, 0, 1);
    //        Color cImage = new Color(1, 1, 1, 1);
    //        UIManager.instace.imageItem5.transform.GetChild(1).GetComponent<Image>().sprite = item.sprite;
    //        UIManager.instace.imageItem5.transform.GetChild(0).GetComponent<Image>().color = cBackground;
    //        UIManager.instace.imageItem5.transform.GetChild(1).GetComponent<Image>().color = cImage;
    //        return 5;
    //    }
    //    else if (!tuiDo.item[5])
    //    {
    //        tuiDo.item[5] = item;

    //        Color cBackground = new Color(0, 0, 0, 1);
    //        Color cImage = new Color(1, 1, 1, 1);
    //        UIManager.instace.imageItem6.transform.GetChild(1).GetComponent<Image>().sprite = item.sprite;
    //        UIManager.instace.imageItem6.transform.GetChild(0).GetComponent<Image>().color = cBackground;
    //        UIManager.instace.imageItem6.transform.GetChild(1).GetComponent<Image>().color = cImage;
    //        return 6;
    //    }
    //    else
    //    {
    //        return 0;
    //    }
    //}

    public bool CanAdd()
    {
        if (!item1.item)
        {
            return true;
        }
        else if (!item2.item)
        {
            return true;
        }
        else if (!item3.item)
        {
            return true;
        }
        else if (!item4.item)
        {
            return true;
        }
        else if (!item5.item)
        {
            return true;
        }
        else if (!item6.item)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public int AddItem(Item item)
    {
        if (!item1.item)
        {
            item1.item = item;

            Color cBackground = new Color(0, 0, 0, 1);
            Color cImage = new Color(1, 1, 1, 1);
            UIManager.instace.imageItem1.transform.GetChild(1).GetComponent<Image>().sprite = item.sprite;
            UIManager.instace.imageItem1.transform.GetChild(0).GetComponent<Image>().color = cBackground;
            UIManager.instace.imageItem1.transform.GetChild(1).GetComponent<Image>().color = cImage;
            return 1;
        }
        else if (!item2.item)
        {
            item2.item = item;

            Color cBackground = new Color(0, 0, 0, 1);
            Color cImage = new Color(1, 1, 1, 1);
            UIManager.instace.imageItem2.transform.GetChild(1).GetComponent<Image>().sprite = item.sprite;
            UIManager.instace.imageItem2.transform.GetChild(0).GetComponent<Image>().color = cBackground;
            UIManager.instace.imageItem2.transform.GetChild(1).GetComponent<Image>().color = cImage;
            return 2;
        }
        else if (!item3.item)
        {
            item3.item = item;

            Color cBackground = new Color(0, 0, 0, 1);
            Color cImage = new Color(1, 1, 1, 1);
            UIManager.instace.imageItem3.transform.GetChild(1).GetComponent<Image>().sprite = item.sprite;
            UIManager.instace.imageItem3.transform.GetChild(0).GetComponent<Image>().color = cBackground;
            UIManager.instace.imageItem3.transform.GetChild(1).GetComponent<Image>().color = cImage;
            return 3;
        }
        else if (!item4.item)
        {
            item4.item = item;

            Color cBackground = new Color(0, 0, 0, 1);
            Color cImage = new Color(1, 1, 1, 1);
            UIManager.instace.imageItem4.transform.GetChild(1).GetComponent<Image>().sprite = item.sprite;
            UIManager.instace.imageItem4.transform.GetChild(0).GetComponent<Image>().color = cBackground;
            UIManager.instace.imageItem4.transform.GetChild(1).GetComponent<Image>().color = cImage;
            return 4;
        }
        else if (!item5.item)
        {
            item5.item = item;

            Color cBackground = new Color(0, 0, 0, 1);
            Color cImage = new Color(1, 1, 1, 1);
            UIManager.instace.imageItem5.transform.GetChild(1).GetComponent<Image>().sprite = item.sprite;
            UIManager.instace.imageItem5.transform.GetChild(0).GetComponent<Image>().color = cBackground;
            UIManager.instace.imageItem5.transform.GetChild(1).GetComponent<Image>().color = cImage;
            return 5;
        }
        else if (!item6.item)
        {
            item6.item = item;

            Color cBackground = new Color(0, 0, 0, 1);
            Color cImage = new Color(1, 1, 1, 1);
            UIManager.instace.imageItem6.transform.GetChild(1).GetComponent<Image>().sprite = item.sprite;
            UIManager.instace.imageItem6.transform.GetChild(0).GetComponent<Image>().color = cBackground;
            UIManager.instace.imageItem6.transform.GetChild(1).GetComponent<Image>().color = cImage;
            return 6;
        }
        else
        {
            return 0;
        }
    }
}

