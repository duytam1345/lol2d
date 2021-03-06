﻿using System.Collections;
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
    public float manaRegenEachLv;
    public float attackDamageEachLv;
    public float attackSpeedEachLv;
    public float bonusAttackAtLv1;
    public float arrmorEachLv;
    public float magicResistanceEachLv;

    public float attackRange;
    public Dictionary<string, float> attackRangeExtra = new Dictionary<string, float>();
    public float attackRange_Real;

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

    public float moveSpeed;
    public Dictionary<string, float> moveSpeedExtra = new Dictionary<string, float>();
    public float moveSpeed_Real;

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
    public Dictionary<string, int> critRateExtra = new Dictionary<string, int>();
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
            moveSpeed_Real += item;
        }

        //Tầm đánh
        attackRange_Real = attackRange;

        foreach (var item in attackRangeExtra.Values)
        {
            attackRange_Real += item;
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
            critRate_Real = Mathf.Clamp(critRate_Real + item, 0, 100);
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
        Death,
        Spell
    }

    public State state;


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

        UIManager.instance.SelectedInventory(slot);


        //var id = int.Parse(gameObject.name);
        //if (tuiDo != null && tuiDo.item != null && tuiDo.item[id])
        //{
        //    tuiDo.ItemSelected = tuiDo.item[id];
        //    tuiDo.ItemSelected.imgTarget = slot;
        //}
    }

    public PropertyChampion propertyChampion;

    public Team team;

    public enum TypeSkill
    {
        Mana,
        Heal,
        None
    }

    public string nameChampion;
    [Header("Image")]
    public Sprite spriteAvatar;
    public Sprite spritePassive;
    public Sprite spriteQ;
    public Sprite spriteW;
    public Sprite spriteE;
    public Sprite spriteR;

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
    public int leftPointSkill;
    [Range(0, 5)]
    public int levelSkillQ;
    [Range(0, 5)]
    public int levelSkillW;
    [Range(0, 5)]
    public int levelSkillE;
    [Range(0, 3)]
    public int levelSkillR;

    [Header("Thông tin chiêu")]
    public string namePassive;
    public string infoPassive;

    public string nameSkillQ;
    public string infoSkillQ;
    public string costSkillQ;
    public TypeSkill typeSkillQ;

    public string nameSkillW;
    public string infoSkillW;
    public string costSkillW;
    public TypeSkill typeSkillW;

    public string nameSkillE;
    public string infoSkillE;
    public string costSkillE;
    public TypeSkill typeSkillE;

    public string nameSkillR;
    public string infoSkillR;
    public string costSkillR;
    public TypeSkill typeSkillR;

    [SerializeField]
    Text textLevel;

    [Header("Item")]
    public SlotInventory item1;
    public SlotInventory item2;
    public SlotInventory item3;
    public SlotInventory item4;
    public SlotInventory item5;
    public SlotInventory item6;

    [Header("Phép")]
    public Spell spellD;
    public Spell spellF;

    public Item ItemWard;
    public TuiDo tuiDo = new TuiDo();
    public virtual void SkillPassive() { }
    public virtual void SkillQ(Vector3 vector) { }
    public virtual void SkillW() { }
    public virtual void SkillE(Vector3 vector) { }
    public virtual void SkillR(Vector3 vector) { }

    public GameObject rend;

    [Header("Biến về")]
    public bool recalling;
    public float timeRecall;
    public GameObject prefabEffect;

    [Header("Khác")]
    public float timePerSecond;
    public float timePerHalfSecond;
    public bool canMove = true;
    public bool canAttack = true;
    public Animator anim;

    [Header("Chết")]
    [SerializeField]
    public bool isDeath;
    public float timeLeftToRespawn;

    [Header("Thống kê")]
    public int kill;
    public int death;
    public int assist;
    public int cr;

    public void PerSecond()
    {
        if (attackSpeedSecond > 0)
        {
            attackSpeedSecond -= Time.deltaTime;
        }

        if (spellD.timeCooldownSecond > 0)
        {
            spellD.timeCooldownSecond -= Time.deltaTime;
        }
        if (spellF.timeCooldownSecond > 0)
        {
            spellF.timeCooldownSecond -= Time.deltaTime;
        }

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
            if (state != State.Death)
            {
                //Regen Health
                propertyChampion.healthPointSecond =
                    Mathf.Clamp(propertyChampion.healthPointSecond + propertyChampion.healthRegen_Real / 10,
                    0, propertyChampion.healthPoint_Real);

                //Regen Mana
                propertyChampion.manaPointSecond =
                    Mathf.Clamp(propertyChampion.manaPointSecond + propertyChampion.manaRegen_Real / 10,
                    0, propertyChampion.manaPoint_Real);
            }
            timePerHalfSecond = .5f;
        }
    }

    public void TakeExp(int xp)
    {
        if (propertyChampion.level < 18)
        {
            propertyChampion.exp += xp;

            if (propertyChampion.exp >= (CongThuc.ExpNextLevel(propertyChampion.level + 1) / 2))
            {
                LevelUp();
            }
        }
    }

    public void LevelUp()
    {
        if (propertyChampion.level < 18)
        {
            //Sound
            GameObject s = Instantiate(Resources.Load("Audio/Level Up")) as GameObject;

            propertyChampion.level++;
            leftPointSkill++;
            if (isBot)
            {
                uI.transform.GetChild(6).GetChild(0).GetComponent<Text>().text = propertyChampion.level.ToString();
            }
            else
            {
                textLevel.text = propertyChampion.level.ToString();
                UIManager.instance.LoadPanelUpgradeSkill(this);
            }

            SetProperty();
        }
    }

    void SetProperty()
    {
        //health
        propertyChampion.healthPointExtra.Add("Level Up " + propertyChampion.level, propertyChampion.healthEachLv);
        propertyChampion.healthPointSecond += propertyChampion.healthEachLv;

        ////health regen
        propertyChampion.healthRegenExtra.Add("Level Up " + propertyChampion.level, propertyChampion.healthRegenEachLv);

        ////mana
        propertyChampion.manaPointExtra.Add("Level Up " + propertyChampion.level, propertyChampion.manaEachLv);
        propertyChampion.manaPointSecond += propertyChampion.manaEachLv;

        ////mana regen
        propertyChampion.manaRegenExtra.Add("Level Up " + propertyChampion.level, propertyChampion.manaRegenEachLv);

        ////physics damage
        propertyChampion.physicsDamageExtra.Add("Level Up " + propertyChampion.level, propertyChampion.attackDamageEachLv);

        ////attack speed
        propertyChampion.attackSpeedExtra.Add("Level Up " + propertyChampion.level, propertyChampion.attackSpeedEachLv);

        ////arrmor
        propertyChampion.arrmorExtra.Add("Level Up " + propertyChampion.level, propertyChampion.arrmorEachLv);

        ////magic resistance
        propertyChampion.magicResistanceExtra.Add("Level Up " + propertyChampion.level, propertyChampion.magicResistanceEachLv);

        propertyChampion.UpdateValue();
    }

    public virtual void SetSkill() { }

    //Tấn công:

    //Đối tượng tấn công truyền sát thương gốc qua TakeDamage() và tên của đối tượng tấn công. 
    //Tính chí mạng
    //Đồng thời tạo hiệu ứng(nếu có).

    //Đối tượng bị tấn công sẽ tính toán ra kết quả cuối cùng(giáp, kháng phép,...).
    //Tính hút máu trả về
    //Tạo ui sát thương gây ra và thông báo kết liễu với tên của đối tượng tấn công.


    [Header("Đánh thường")]
    public GameObject targetAttack;
    public float attackSpeedSecond;
    public GameObject prefabHit;
    public bool attacking;
    public Vector2 targetPos;
    public Rigidbody2D rb2d;

    public bool isRangeChamp;

    public UIFollowTarget uI;

    public bool isBot;

    public virtual void Start()
    {
        BtnItem.KhiTuiDoClick += KhiChon1Item;

        SkillPassive();

        rb2d = GetComponent<Rigidbody2D>();
        anim = transform.GetChild(0).GetComponent<Animator>();

        propertyChampion.UpdateValue();

        Spell sD = spellD.GetComponent<Spell>();
        sD.timeCooldown = spellD.GetComponent<Spell>().timeCooldown;
        gameObject.AddComponent(sD.GetType());
        GetComponents<Spell>()[0].timeCooldown = spellD.GetComponent<Spell>().timeCooldown;
        GetComponents<Spell>()[0].effect = spellD.GetComponent<Spell>().effect;
        GetComponents<Spell>()[0].iconEffect = spellD.GetComponent<Spell>().iconEffect;
        spellD = GetComponents<Spell>()[0];

        Spell sF = spellF.GetComponent<Spell>();
        sF.timeCooldown = spellF.GetComponent<Spell>().timeCooldown;
        gameObject.AddComponent(sF.GetType());
        GetComponents<Spell>()[1].timeCooldown = spellF.GetComponent<Spell>().timeCooldown;
        GetComponents<Spell>()[1].effect = spellF.GetComponent<Spell>().effect;
        GetComponents<Spell>()[1].iconEffect = spellF.GetComponent<Spell>().iconEffect;
        spellF = GetComponents<Spell>()[1];

        //Set With Champ
        if (!isBot)
        {
            textLevel = GameObject.Find("Bar Top Player").transform.Find("Level").GetChild(0).GetComponent<Text>();
        }
        GameObject.Find("Bar Top Player").transform.Find("Text").GetComponent<Text>().text = nameChampion;
        //Avatar
        GameObject.Find("Avatar").GetComponent<Image>().sprite = spriteAvatar;
        //Nội tại
        GameObject.Find("Skill Passive").GetComponent<Image>().sprite = spritePassive;
        GameObject.Find("Skill Passive (1)").GetComponent<Image>().sprite = spritePassive;
        //Q
        GameObject.Find("Skill Q").GetComponent<Image>().sprite = spriteQ;
        GameObject.Find("Skill 1 (1)").GetComponent<Image>().sprite = spriteQ;
        //W
        GameObject.Find("Skill W").GetComponent<Image>().sprite = spriteW;
        GameObject.Find("Skill 2 (1)").GetComponent<Image>().sprite = spriteW;
        //E
        GameObject.Find("Skill E").GetComponent<Image>().sprite = spriteE;
        GameObject.Find("Skill 3 (1)").GetComponent<Image>().sprite = spriteE;
        //R
        GameObject.Find("Skill R").GetComponent<Image>().sprite = spriteR;
        GameObject.Find("Skill 4 (1)").GetComponent<Image>().sprite = spriteR;
        //Iteam
        item1 = UIManager.instance.imageItem1.GetComponent<SlotInventory>();
        item2 = UIManager.instance.imageItem2.GetComponent<SlotInventory>();
        item3 = UIManager.instance.imageItem3.GetComponent<SlotInventory>();
        item4 = UIManager.instance.imageItem4.GetComponent<SlotInventory>();
        item5 = UIManager.instance.imageItem5.GetComponent<SlotInventory>();
        item6 = UIManager.instance.imageItem6.GetComponent<SlotInventory>();

    }

    public virtual void LoadAttack()
    {
        if (targetAttack)
        {
            if (attackSpeedSecond <= 0)
            {
                Attack();
                attackSpeedSecond = 1 / propertyChampion.attackSpeed_Real;
            }
        }
        else
        {
            attacking = false;
            targetPos = Vector2.zero;
            rb2d.velocity = Vector2.zero;
            state = Champion.State.Idle;
        }
    }

    public virtual void Attack() { }

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

            UIManager.instance.imageItem1.transform.GetChild(1).GetComponent<Image>().sprite = item.sprite;
            UIManager.instance.imageItem1.transform.GetChild(0).GetComponent<Image>().color = cBackground;
            UIManager.instance.imageItem1.transform.GetChild(1).GetComponent<Image>().color = cImage;

            UIManager.instance.imageItem1_UIChamp.GetComponent<SlotInventory>().item = item;
            UIManager.instance.imageItem1_UIChamp.transform.GetChild(0).GetComponent<Image>().color = cImage;
            UIManager.instance.imageItem1_UIChamp.transform.GetChild(0).GetComponent<Image>().sprite = item.sprite;
            return 1;
        }
        else if (!item2.item)
        {
            item2.item = item;

            Color cBackground = new Color(0, 0, 0, 1);
            Color cImage = new Color(1, 1, 1, 1);

            UIManager.instance.imageItem2.transform.GetChild(1).GetComponent<Image>().sprite = item.sprite;
            UIManager.instance.imageItem2.transform.GetChild(0).GetComponent<Image>().color = cBackground;
            UIManager.instance.imageItem2.transform.GetChild(1).GetComponent<Image>().color = cImage;

            UIManager.instance.imageItem2_UIChamp.GetComponent<SlotInventory>().item = item;
            UIManager.instance.imageItem2_UIChamp.transform.GetChild(0).GetComponent<Image>().color = cImage;
            UIManager.instance.imageItem2_UIChamp.transform.GetChild(0).GetComponent<Image>().sprite = item.sprite;
            return 2;
        }
        else if (!item3.item)
        {
            item3.item = item;

            Color cBackground = new Color(0, 0, 0, 1);
            Color cImage = new Color(1, 1, 1, 1);

            UIManager.instance.imageItem3.transform.GetChild(1).GetComponent<Image>().sprite = item.sprite;
            UIManager.instance.imageItem3.transform.GetChild(0).GetComponent<Image>().color = cBackground;
            UIManager.instance.imageItem3.transform.GetChild(1).GetComponent<Image>().color = cImage;

            UIManager.instance.imageItem3_UIChamp.GetComponent<SlotInventory>().item = item;
            UIManager.instance.imageItem3_UIChamp.transform.GetChild(0).GetComponent<Image>().color = cImage;
            UIManager.instance.imageItem3_UIChamp.transform.GetChild(0).GetComponent<Image>().sprite = item.sprite;
            return 3;
        }
        else if (!item4.item)
        {
            item4.item = item;

            Color cBackground = new Color(0, 0, 0, 1);
            Color cImage = new Color(1, 1, 1, 1);

            UIManager.instance.imageItem4.transform.GetChild(1).GetComponent<Image>().sprite = item.sprite;
            UIManager.instance.imageItem4.transform.GetChild(0).GetComponent<Image>().color = cBackground;
            UIManager.instance.imageItem4.transform.GetChild(1).GetComponent<Image>().color = cImage;

            UIManager.instance.imageItem4_UIChamp.GetComponent<SlotInventory>().item = item;
            UIManager.instance.imageItem4_UIChamp.transform.GetChild(0).GetComponent<Image>().color = cImage;
            UIManager.instance.imageItem4_UIChamp.transform.GetChild(0).GetComponent<Image>().sprite = item.sprite;
            return 4;
        }
        else if (!item5.item)
        {
            item5.item = item;

            Color cBackground = new Color(0, 0, 0, 1);
            Color cImage = new Color(1, 1, 1, 1);

            UIManager.instance.imageItem5.transform.GetChild(1).GetComponent<Image>().sprite = item.sprite;
            UIManager.instance.imageItem5.transform.GetChild(0).GetComponent<Image>().color = cBackground;
            UIManager.instance.imageItem5.transform.GetChild(1).GetComponent<Image>().color = cImage;

            UIManager.instance.imageItem5_UIChamp.GetComponent<SlotInventory>().item = item;
            UIManager.instance.imageItem5_UIChamp.transform.GetChild(0).GetComponent<Image>().color = cImage;
            UIManager.instance.imageItem5_UIChamp.transform.GetChild(0).GetComponent<Image>().sprite = item.sprite;
            return 5;
        }
        else if (!item6.item)
        {
            item6.item = item;

            Color cBackground = new Color(0, 0, 0, 1);
            Color cImage = new Color(1, 1, 1, 1);
            UIManager.instance.imageItem6.transform.GetChild(1).GetComponent<Image>().sprite = item.sprite;
            UIManager.instance.imageItem6.transform.GetChild(0).GetComponent<Image>().color = cBackground;
            UIManager.instance.imageItem6.transform.GetChild(1).GetComponent<Image>().color = cImage;

            UIManager.instance.imageItem6_UIChamp.GetComponent<SlotInventory>().item = item;
            UIManager.instance.imageItem6_UIChamp.transform.GetChild(0).GetComponent<Image>().color = cImage;
            UIManager.instance.imageItem6_UIChamp.transform.GetChild(0).GetComponent<Image>().sprite = item.sprite;
            return 6;
        }
        else
        {
            return 0;
        }
    }

    public void TakeDamage(GameObject g, int dmg, Vector2 target)
    {
        float dmged = CongThuc.LayDamage(dmg, propertyChampion.arrmor_Real);

        propertyChampion.healthPointSecond -= dmged;

        Vector2 rectPos = target;
        rectPos = new Vector2(
            (float)Random.Range(rectPos.x - .5f, rectPos.x + .5f),
            (float)Random.Range(rectPos.y - .5f, rectPos.y + .5f));

        UIManager.instance.MakeTextDamage(rectPos, ((int)dmged).ToString());

        if (propertyChampion.healthPointSecond <= 0)
        {
            if(team == Team.Blue)
            {
                Mananger.instance.totalRed++;
            }
            else
            {
                Mananger.instance.totalRed++;
            }

            if (g.GetComponent<Champion>())
            {
                if (g.GetComponent<Champion>().isBot)
                {
                    //Sound
                    GameObject s = Instantiate(Resources.Load("Audio/You have been slain")) as GameObject;
                }
                else
                {
                    //Sound
                    GameObject s = Instantiate(Resources.Load("Audio/Enemy has been slain")) as GameObject;
                }

                g.GetComponent<Champion>().kill++;
            }

            UIManager.instance.CreateTextDestroyed(g, gameObject);
            Death();
        }
    }

    public void TakeHealth(float amount)
    {
        float result = Mathf.Clamp(propertyChampion.healthPointSecond + amount, 0, propertyChampion.healthPoint_Real);
        if (result - propertyChampion.healthPointSecond > 0)
        {
            UIManager.instance.MakeTextHeal(transform.position, (result - propertyChampion.healthPointSecond).ToString());
            propertyChampion.healthPointSecond += (result - propertyChampion.healthPointSecond);
        }
    }

    public void TakeMana(float amount)
    {
        float result = Mathf.Clamp(propertyChampion.manaPointSecond + amount, 0, propertyChampion.manaPoint_Real);
        if (result - propertyChampion.manaPointSecond > 0)
        {
            propertyChampion.manaPointSecond += (result - propertyChampion.manaPointSecond);
        }
    }

    public void SpawnAtFountain()
    {
        if (isDeath)
        {
            isDeath = false;
            propertyChampion.healthPointSecond = propertyChampion.healthPoint_Real;
            propertyChampion.manaPointSecond = propertyChampion.manaPoint_Real;
        }

        rend.SetActive(true);
        state = State.Idle;
        transform.position = (team == Team.Blue ? Mananger.instance.fountainBlue.transform.position : Mananger.instance.fountainRed.transform.position);
        UIManager.instance.imageAvatar.color = new Color(1f, 1f, 1f, 1);
        UIManager.instance.barTopHealth.SetActive(true);
        if (!isBot)
        {
            Camera.main.transform.position = new Vector3(transform.position.x, 0, -10);
        }
    }

    public void ReCall()
    {
        if (!recalling)
        {
            recalling = true;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            state = State.Idle;
            GameObject e = Instantiate(prefabEffect, transform.position, Quaternion.identity);
            e.name = "Effect Recall";

            if (!isBot)
            {
                UIManager.instance.MakeReCallBar();

            }
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
            if (UIManager.instance.currentReCallUI)
            {
                Destroy(UIManager.instance.currentReCallUI.gameObject);
            }
        }
    }

    public virtual void Death() { }

    public void MoveToAttack()
    {
        if (targetAttack)
        {
            if (targetAttack.GetComponent<Champion>() && targetAttack.GetComponent<Champion>().state == State.Death)
            {
                targetAttack = null;
                state = State.Idle;
                attacking = false;
            }

            targetPos = Vector2.zero;
            attacking = true;
            if (targetAttack && Vector2.Distance(transform.position, targetAttack.transform.position) > propertyChampion.attackRange_Real)
            {
                Vector2 dir = (Vector2)targetAttack.transform.position - (Vector2)transform.position;

                rb2d.velocity = (dir.normalized) * propertyChampion.moveSpeed_Real;

                state = State.WalkToAttack;

                anim.SetFloat("VelocityX", dir.x);
                anim.SetFloat("VelocityY", dir.y);
            }
            else
            {
                rb2d.velocity = Vector2.zero;
                LoadAttack();
                state = State.Attack;
            }
        }
        else
        {
            state = State.Idle;
            attacking = false;
        }
    }

    public virtual void Move()
    {
        if (targetPos != Vector2.zero)
        {
            if (Vector2.Distance(transform.position, targetPos) >= .2f)
            {
                Vector2 dir = targetPos - (Vector2)transform.position;

                rb2d.velocity = (dir.normalized) * propertyChampion.moveSpeed_Real;
                state = State.Walk;
            }
            else
            {
                state = State.Idle;
                targetPos = Vector2.zero;
                rb2d.velocity = Vector2.zero;
            }
        }
    }

    // Làm 

    //Chỉnh sửa miniMap
    // Giao diện chat
    //Điều chỉnh tỉ lệ đánh lính
    // Hiệu ứng âm thanh
    //-đếm ngược giây chọn tướng
    //-wellcome smnr minion
    //-đánh thường, skill KogMaw
    //-tiêu diệt
    // Bot né chiêu
    //Bot tự mua trang bị

}

