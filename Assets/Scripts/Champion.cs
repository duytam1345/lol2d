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

    public int lifeSteel;
    public Dictionary<string, float> lifeSteelExtra = new Dictionary<string, float>();
    public int lifeSteel_Real;

    public int spellVamp;
    public Dictionary<string, float> spellVampExtra = new Dictionary<string, float>();
    public int spellVamp_Real;

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
        // Sát thương vật lý
        physicsDamage_Real = physicsDamage;

        foreach (var item in physicsDamageExtra.Values)
        {
            physicsDamage_Real += (int)item;
        }

        // Sức mạnh phép thuật
        magicDamage_Real = magicDamage;

        foreach (var item in magicDamageExtra.Values)
        {
            magicDamage_Real += (int)item;
        }

        //Máu
        healthPoint_Real = healthPoint;

        foreach (var item in healthPointExtra.Values)
        {
            healthPoint_Real += (int)item;
        }

        //Phục hồi máu
        healthRegen_Real = healthRegen;

        foreach (var item in healthRegenExtra.Values)
        {
            healthRegen_Real += item;
        }

        //Năng lượng
        manaPoint_Real = manaPoint;

        foreach (var item in manaPointExtra.Values)
        {
            manaPoint_Real += (int)item;
        }

        //Phục hồi năng lượng
        manaRegen_Real = manaRegen;

        foreach (var item in manaRegenExtra.Values)
        {
            manaRegen_Real += item;
        }

        //Hút máu
        lifeSteel_Real = lifeSteel;

        foreach (var item in lifeSteelExtra.Values)
        {
            lifeSteel_Real += (int)item;
        }

        //Hút máu phép
        spellVamp_Real = spellVamp;

        foreach (var item in spellVampExtra.Values)
        {
            spellVamp_Real += (int)item;
        }

        //Tốc độ di chuyển
        moveSpeed_Real = moveSpeed;

        foreach (var item in moveSpeedExtra.Values)
        {
            moveSpeed_Real += (int)item;
        }

        //Giáp
        arrmor_Real = arrmor;

        foreach (var item in arrmorExtra.Values)
        {
            arrmor_Real += (int)item;
        }

        //Kháng phép
        magicResistance_Real = magicResistance;

        foreach (var item in magicResistanceExtra.Values)
        {
            magicResistance_Real += (int)item;
        }

        //Tốc độ đánh
        attackSpeed_Real = attackSpeed;
        float percent = bonusAttackAtLv1;
        foreach (var item in attackSpeedExtra.Values)
        {
            percent += item;
        }
        attackSpeed_Real += (attackSpeed / 100 * percent);

        //Hồi chiêu
        cooldown_Real = cooldown;

        foreach (var item in cooldownExtra.Values)
        {
            cooldown_Real += (int)item;
        }

        //Tỉ lệ chí mạng
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
    public enum State
    {
        Idle,
        Walk,
        WalkToAttack,
        Attack,
        Spell
    }

    public State state;

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

    [Header("Biến về")]
    public bool recalling;
    public float timeRecall;
    public GameObject prefabEffect;

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

    public void TakeDamage(GameObject g, int dmg, Vector2 target)
    {
        float damage = CongThuc.LayDamage(dmg, propertyChampion.arrmor_Real);

        propertyChampion.healthPointSecond -= (int)damage;

        Vector2 rectPos = target;
        rectPos = new Vector2(
            (float)Random.Range(rectPos.x - .5f, rectPos.x + .5f),
            (float)Random.Range(rectPos.y - .5f, rectPos.y + .5f));

        UIManager.instace.MakeTextDamage(rectPos, ((int)damage).ToString());
    }

    public void TakeHealth(float amount)
    {
        float result = Mathf.Lerp(0, propertyChampion.healthPoint_Real, propertyChampion.healthPointSecond + amount);
        if (result - propertyChampion.healthPointSecond > 0)
        {
            propertyChampion.healthPointSecond += result - propertyChampion.healthPointSecond;
            UIManager.instace.MakeTextHeal(transform.position, result.ToString());
        }
    }

    public void SpawnAtFountain()
    {
        transform.position = (team == Team.Blue ? Mananger.instance.fountainBlue.transform.position : Mananger.instance.fountainRed.transform.position);
    }

    public void ReCall()
    {
        if (!recalling)
        {
            recalling = true;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            state = State.Idle;
            UIManager.instace.MakeReCallBar();

            GameObject e = Instantiate(prefabEffect, transform.position, Quaternion.identity);
            e.name = "Effect Recall";
        }
    }

    public void StopReCall()
    {
        if (recalling)
        {
            recalling = false;
            timeRecall = 0;
            if (GameObject.Find("Effect Recall"))
            {
                Destroy(GameObject.Find("Effect Recall"));
            }
            if (UIManager.instace.currentReCallUI)
            {
                Destroy(UIManager.instace.currentReCallUI.gameObject);
            }
        }
    }
}

