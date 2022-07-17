using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SeasonSceneManager : MonoBehaviour
{
    [SerializeField]
    GameObject tracker;

    GameObject roundText;

    GameObject HomeButton;
    GameObject SpringButton;
    GameObject SummerButton;
    GameObject FallButton;
    GameObject WinterButton;

    GameObject SeasonImage1;
    GameObject SeasonImage2;
    GameObject SeasonImage3;
    GameObject SeasonImage4;

    string trackercollision;
    string currSeason;
    List<string> Seasons;

    int round;

    bool stageCleared;
    // Start is called before the first frame update
    void Start()
    {
        Seasons = new List<string>();
        Init();
    }

    private void Init()
    {
        HomeButton = GameObject.Find("HomeButton");
        SpringButton = GameObject.Find("SpringButton");
        SummerButton = GameObject.Find("SummerButton");
        FallButton = GameObject.Find("FallButton");
        WinterButton = GameObject.Find("WinterButton");
        tracker = GameObject.Find("tracker");
        roundText = GameObject.Find("RoundText");

        SeasonImage1 = GameObject.Find("SeasonImage1");
        SeasonImage2 = GameObject.Find("SeasonImage2");
        SeasonImage3 = GameObject.Find("SeasonImage3");
        SeasonImage4 = GameObject.Find("SeasonImage4");

        HomeButton.GetComponent<Button>().onClick.AddListener(onHomeButtonClick);
        SpringButton.GetComponent<Button>().onClick.AddListener(onSpringButtonClick);
        SummerButton.GetComponent<Button>().onClick.AddListener(onSummerButtonClick);
        FallButton.GetComponent<Button>().onClick.AddListener(onFallButtonClick);
        WinterButton.GetComponent<Button>().onClick.AddListener(onWinterButtonClick);
        stageCleared = true;

        round = 0;

        Seasons.Add("Spring");
        Seasons.Add("Summer");
        Seasons.Add("Fall");
        Seasons.Add("Winter");
    }

    // Update is called once per frame
    void Update()
    {
        if (tracker.GetComponent<TrackerManager>().Collision2D != "")
        {
            Debug.Log(tracker.GetComponent<TrackerManager>().Collision2D);
            trackercollision = tracker.GetComponent<TrackerManager>().Collision2D;
        }

        switch (trackercollision)
        {
            case "HomeButton":
                tracker.GetComponent<TrackerManager>().GetScript(onHomeButtonClick);
                break;
            case "SpringButton":
                tracker.GetComponent<TrackerManager>().GetScript(onSpringButtonClick);
                break;
            case "SummerButton":
                tracker.GetComponent<TrackerManager>().GetScript(onSummerButtonClick);
                break;
            case "FallButton":
                tracker.GetComponent<TrackerManager>().GetScript(onFallButtonClick);
                break;
            case "WinterButton":
                tracker.GetComponent<TrackerManager>().GetScript(onWinterButtonClick);
                break;
            default:
                break;
        }

        if (stageCleared)
        {
            stageCleared = false;
            round++;
            if (round > 10)
            {
                SceneManager.LoadScene(1);
            }
            roundText.GetComponent<Text>().text = "stage: " + (round > 10 ? 10: round).ToString() + "/10";
            currSeason = Seasons[Random.Range(0, 4)];
            SetImages(currSeason);
        }
    }

    public void onHomeButtonClick()
    {
        Debug.Log("onHomeButtonClicked");
        SceneManager.LoadScene(1);
    }

    public void onSpringButtonClick()
    {
        Debug.Log("onSpringButtonClicked");
        if (currSeason == "Spring")
        {
            stageCleared = true;
        }
    }

    public void onSummerButtonClick()
    {
        Debug.Log("onSummerButtonClicked");
        if (currSeason == "Summer")
        {
            stageCleared = true;
        }
    }

    public void onFallButtonClick()
    {
        Debug.Log("onFallButtonClicked");
        if (currSeason == "Fall")
        {
            stageCleared = true;
        }
    }

    public void onWinterButtonClick()
    {
        Debug.Log("onWinterButtonClicked");
        if (currSeason == "Winter")
        {
            stageCleared = true;
        }
    }

    void SetImages(string currentSeason)
    {
        List<Sprite> imageNums = new List<Sprite>();
        imageNums = SetImageSprite(currSeason);
        SeasonImage1.GetComponent<Image>().sprite = imageNums[0];
        SeasonImage2.GetComponent<Image>().sprite = imageNums[1];
        SeasonImage3.GetComponent<Image>().sprite = imageNums[2];
        SeasonImage4.GetComponent<Image>().sprite = imageNums[3];
    }

    List<Sprite> SetImageSprite(string currentSeason)
    {
        List<Sprite> images = new List<Sprite>();
        for (int i = 0; i < 4; i++)
        {
            Debug.Log("Sprites/SeasonImage/" + currentSeason + (i + 1).ToString());
            images.Add(Resources.Load<Sprite>("Sprites/SeasonImage/" + currentSeason + (i + 1).ToString()));
        }
        return images;
    }
}
