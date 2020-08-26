using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MangerPickChamp : MonoBehaviour
{
    public static MangerPickChamp mangerPickChamp;

    public string nameChampionPick;

    public Button btnLockChamp;

    public Text textTime;

    public float timeLeft = 60;

    bool isPlayGame;

    public GameObject prefabChampToPick;
    public GameObject prefabChampPicked;

    public SlotChampToPick currentLock;

    public GameObject playerS1;

    private void Awake()
    {
        if (mangerPickChamp == null)
        {
            mangerPickChamp = this;
        }
        else
        {
            Destroy(this);
        }

        DontDestroyOnLoad(mangerPickChamp);
    }

    private void Start()
    {
        if (!isPlayGame)
        {
            btnLockChamp.interactable = false;
        }
    }

    private void Update()
    {
        if (!isPlayGame)
        {
            timeLeft -= Time.deltaTime;

            textTime.text = Mathf.RoundToInt(timeLeft).ToString();

            if (timeLeft <= 0)
            {
                SceneGame();
            }

            if (Input.GetKeyDown(KeyCode.N))
            {
                SceneGame();
            }
        }
    }

    void SceneGame()
    {
        isPlayGame = true;
        SceneManager.LoadScene("Play");
    }

    public void BtnPickChamp(GameObject g)
    {
        btnLockChamp.interactable = true;

        if (currentLock)
        {
            currentLock.transform.GetChild(0).GetComponent<Image>().color = new Color(0, 0, 0, 1);
        }

        g.transform.GetChild(0).GetComponent<Image>().color = Color.white;

        SlotChampToPick slotChampToPick = g.GetComponent<SlotChampToPick>();

        currentLock = slotChampToPick;

        playerS1.transform.GetChild(2).GetChild(0).GetComponent<Image>().color = Color.white;
        playerS1.transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite = currentLock.imageAvatar;
    }

    public void BtnLock()
    {
        if (currentLock != null)
        {
            timeLeft = 15;

            playerS1.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = Color.white;
            playerS1.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = currentLock.imageBackground;
            playerS1.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>().anchoredPosition = currentLock.vectorPos;
            playerS1.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta = currentLock.vectorAnchors;

            nameChampionPick = currentLock.nameChamp;

            //Load Sound
            if (Resources.Load(nameChampionPick + "/" + nameChampionPick + " Select"))
            {
                GameObject g = Instantiate(Resources.Load(nameChampionPick + "/" + nameChampionPick + " Select")) as GameObject;
            }
        }
    }
}
