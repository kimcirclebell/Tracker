using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSelectSceneManager : MonoBehaviour
{
    GameObject AllGameButton;
    GameObject HomeButton;
    GameObject GameSelectButton;
    GameObject EasyButton;
    GameObject HardButton;

    [SerializeField]
    GameObject tracker;

    GameObject Difficulty;
    GameObject dif;
    GameObject result;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    void Awake()
    {
        if (GameObject.Find("Result"))
            GameObject.Find("Result").GetComponent<Remember>().PreviousScene = 0;
    }

    private void Init()
    {
        AllGameButton = GameObject.Find("AllGameButton");
        HomeButton = GameObject.Find("HomeButton");
        GameSelectButton = GameObject.Find("GameSelectButton");
        EasyButton = GameObject.Find("EasyButton");
        HardButton = GameObject.Find("HardButton");
        tracker = GameObject.Find("tracker");

        AllGameButton.GetComponent<Button>().onClick.AddListener(onAllButtonClick);
        HomeButton.GetComponent<Button>().onClick.AddListener(onHomeButtonClick);
        GameSelectButton.GetComponent<Button>().onClick.AddListener(onGameSelectButtonClick);
        EasyButton.GetComponent<Button>().onClick.AddListener(onEasyButtonClick);
        HardButton.GetComponent<Button>().onClick.AddListener(onHardButtonClick);

        if (!GameObject.Find("Difficulty"))
        {
            Difficulty = new GameObject();
            Difficulty.transform.name = "Difficulty";
            dif = new GameObject();
            dif.transform.SetParent(Difficulty.transform);
            result = new GameObject();
            result.transform.name = "Result";
            result.transform.SetParent(Difficulty.transform);
            result.AddComponent<Remember>();

            DontDestroyOnLoad(Difficulty);
        }
        else
        {
            dif = GameObject.Find("Difficulty").transform.GetChild(0).gameObject;
            result = GameObject.Find("Difficulty").transform.GetChild(1).gameObject;
        }

        EasyButton.SetActive(false);
        HardButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        string trackercollision = "";

        if (tracker.GetComponent<TrackerManager>().Collision2D != "")
        {
            trackercollision = tracker.GetComponent<TrackerManager>().Collision2D;
        }
        switch (trackercollision)
        {
            case "AllGameButton":
                tracker.GetComponent<TrackerManager>().GetScript(onAllButtonClick);
                break;
            case "HomeButton":
                tracker.GetComponent<TrackerManager>().GetScript(onHomeButtonClick);
                break;
            case "GameSelectButton":
                tracker.GetComponent<TrackerManager>().GetScript(onGameSelectButtonClick);
                break;
            case "EasyButton":
                tracker.GetComponent<TrackerManager>().GetScript(onEasyButtonClick);
                break;
            case "HardButton":
                tracker.GetComponent<TrackerManager>().GetScript(onHardButtonClick);
                break;
            default:
                break;
        }
    }

    void onHomeButtonClick()
    {
        SceneManager.LoadScene(0);
        Debug.Log("HomeButtonClicked");
    }

    void onAllButtonClick()
    {
        Debug.Log("AllButtonClicked");
    }

    void onGameSelectButtonClick()
    {
        Debug.Log("GameSelectButtonClicked");
        GameSelectButton.SetActive(false);
        EasyButton.SetActive(true);
        HardButton.SetActive(true);
    }

    void onEasyButtonClick()
    {
        dif.transform.name = "Easy";
        SceneManager.LoadScene(2);
        Debug.Log("EasyButtonClicked");
    }

    void onHardButtonClick()
    {
        dif.transform.name = "Hard";
        SceneManager.LoadScene(2);
        Debug.Log("HardButtonClicked");
    }
}
