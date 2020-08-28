using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Team
{
    Red,
    Blue,
}
public class Mananger : MonoBehaviour
{
    public static Mananger mananger;

    [SerializeField]
    GameObject effectRightClick;
    //[ConditionalField("WanderAround")] public float WanderDistance = 5;

    [SerializeField]
    int numMinion;
    [SerializeField]
    Transform spawEnemyPosition;
    public static Mananger instance;

    [Header("Time Spawn")]
    [SerializeField]
    float timeToNextWaveCreep;
    [SerializeField]
    float timeToNextWaveCreepSecond;

    [Header("Prefab Spawn")]
    [SerializeField]
    GameObject preafabCreep;
    [SerializeField]
    GameObject prefabTurret;

    [Header("Position Spawn")]
    [SerializeField]
    GameObject posSpawnCreepBlue;
    [SerializeField]
    GameObject posSpawnCreepRed;
    [SerializeField]
    GameObject[] pointTurretBlue;
    [SerializeField]
    GameObject[] pointTurretRed;

    [Header("UI Spawn")]
    [SerializeField]
    GameObject uiCreepRed;
    [SerializeField]
    GameObject uiCreepBlue;
    [SerializeField]
    GameObject uiTurretRed;
    [SerializeField]
    GameObject uiTurretBlue;


    [Header("Cursor")]
    [SerializeField]
    Texture2D m1;
    [SerializeField]
    Texture2D m2;

    public GameObject fountainRed;
    public GameObject fountainBlue;

    public List<Champion> championInGame;

    public int totalBlue = 0;
    public int totalRed = 0;

    public float timeMinute;
    public float timeSecond;

    public bool inChatMode;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }


        string n = "";

        if (MangerPickChamp.mangerPickChamp)
        {
            n = MangerPickChamp.mangerPickChamp.nameChampionPick;
        }

        if (string.IsNullOrEmpty(n))
        {
            n = "Kog'Maw";
            print("Null Select Champ!.");
        }
        if (Resources.Load(n))
        {
            Charater c = FindObjectOfType<Charater>();
            GameObject g = Resources.Load(n) as GameObject;
            c.gameObject.AddComponent(g.GetComponent<Champion>().GetType());

            string nameComponent = "";

            if (n == "Kog'Maw")
            {
                nameComponent = "KogMawChampion";
            }
            else if (n == "Dr. Mundo")
            {
                nameComponent = "MundoChampion";
            }
            else if (n == "Heimerdinger")
            {
                nameComponent = "HeimerdingerChampion";
            }

            UnityEditorInternal.ComponentUtility.CopyComponent(g.GetComponent(nameComponent));
            UnityEditorInternal.ComponentUtility.PasteComponentValues(c.GetComponent<Champion>());

            championInGame.Add(c.GetComponent<Champion>());
        }


        championInGame.Add(GameObject.Find("Bot").GetComponent<Champion>());

        SpawnTurret();
    }
    private void Start()
    {
        //Cursor.SetCursor(m1, Vector2.zero,CursorMode.ForceSoftware);

        Minion.OnFindEnemy += OnMinionFindEnemy;
        SpawMinion();
    }

    private void Update()
    {
        CalTime();
    }

    void SpawnTurret()
    {
        for (int i = 0; i < pointTurretBlue.Length; i++)
        {
            ////Create Blue
            ///
            //Turret
            GameObject tB = Instantiate(prefabTurret, pointTurretBlue[i].transform.position, Quaternion.identity);
            tB.name = "Turret Blue " + (i + 1);

            //UI
            GameObject uB = Instantiate(uiTurretBlue, GameObject.Find("UI Turret").transform);

            tB.GetComponent<Turret>().team = Team.Blue;
            tB.GetComponent<Turret>().ui = uB.GetComponent<UIFollowTarget>();
            uB.GetComponent<UIFollowTarget>().gTarget = tB;

            ////Create Red
            ///
            //Turret
            GameObject tR = Instantiate(prefabTurret, pointTurretRed[i].transform.position, Quaternion.identity);
            tR.name = "Turret Red " + (i + 1);

            //UI
            GameObject uR = Instantiate(uiTurretRed, GameObject.Find("UI Turret").transform);

            tR.GetComponent<Turret>().team = Team.Red;
            tR.GetComponent<Turret>().ui = uR.GetComponent<UIFollowTarget>();
            uR.GetComponent<UIFollowTarget>().gTarget = tR;
        }
    }

    void CalTime()
    {
        timeToNextWaveCreepSecond -= Time.deltaTime;
        if (timeToNextWaveCreepSecond <= 0)
        {
            SpawnCreep();
            timeToNextWaveCreepSecond = timeToNextWaveCreep;
        }
    }

    void SpawnCreep()
    {
        StartCoroutine(SpawnCreepCo());
    }

    IEnumerator SpawnCreepCo()
    {
        for (int i = 0; i < 3; i++)
        {
            //Creep Blue
            //Creep
            GameObject cB = Instantiate(preafabCreep, posSpawnCreepBlue.transform.position, Quaternion.identity);
            //UI
            GameObject uB = Instantiate(uiCreepBlue, GameObject.Find("UI Creep").transform);

            cB.GetComponent<Creep>().ui = uB.GetComponent<UIFollowTarget>();
            cB.GetComponent<Creep>().targetBase = posSpawnCreepRed;
            cB.GetComponent<Creep>().team = Team.Blue;
            cB.GetComponent<Creep>().direction = Creep.Dir.Right;
            uB.GetComponent<UIFollowTarget>().gTarget = cB;

            //Creep Red
            //Creep
            GameObject cR = Instantiate(preafabCreep, posSpawnCreepRed.transform.position, Quaternion.identity);
            //UI
            GameObject uR = Instantiate(uiCreepRed, GameObject.Find("UI Creep").transform);

            cR.GetComponent<Creep>().ui = uR.GetComponent<UIFollowTarget>();
            cR.GetComponent<Creep>().targetBase = posSpawnCreepBlue;
            cR.GetComponent<Creep>().team = Team.Red;
            cR.GetComponent<Creep>().direction = Creep.Dir.Left;
            uR.GetComponent<UIFollowTarget>().gTarget = cR;

            yield return new WaitForSeconds(2);
        }
    }

    private void SpawMinion()
    {
        StartCoroutine(IESpawMinion());
    }
    private IEnumerator IESpawMinion()
    {
        var enemy = Resources.Load("Minion_type1 1") as GameObject;
        for (int i = 0; i < numMinion; i++)
        {
            yield return new WaitForSeconds(1f);
            var e = GameObject.Instantiate(enemy);
            e.transform.position = spawEnemyPosition.position;
        }
    }
    private void OnMinionFindEnemy(Minion minion)
    {
        minion.SetEnemy(GameObject.Find("Minion_type1_test").transform);
    }

    public void MakeAnimClickMove(Vector2 pos)
    {
        GameObject g = Instantiate(effectRightClick, pos, Quaternion.identity);
    }

    public void SetCursor(int i)
    {
        //if (i == 0)
        //{
        //    Cursor.SetCursor(m1, Vector2.zero, CursorMode.ForceSoftware);
        //}
        //else if (i == 1)
        //{
        //    Cursor.SetCursor(m2, Vector2.zero, CursorMode.ForceSoftware);
        //}
    }
}
