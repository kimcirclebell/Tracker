using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverSceneManager : MonoBehaviour
{
    GameObject tracker;
    GameObject ContinueButton;
    GameObject ExitButton;

    string trackercollision;

    //Todo(이전의 씬 받아와서 컨티뉴 만들기)

    void Start()
    {
        Init();
    }

    void Init()
    {
        tracker = GameObject.Find("tracker");
        ContinueButton = GameObject.Find("ButtonContinue");
        ExitButton = GameObject.Find("ButtonExit");

        ContinueButton.GetComponent<Button>().onClick.AddListener(OnContinueButtonClicked);
        ExitButton.GetComponent<Button>().onClick.AddListener(OnExitButtonClicked);
    }

    private void Update()
    {
        if (tracker.GetComponent<TrackerManager>().Collision2D != "")
        {
            trackercollision = tracker.GetComponent<TrackerManager>().Collision2D;
        }
        else
        {
            trackercollision = "";
        }


        switch (trackercollision)
        {
            case "ButtonContinue":
                tracker.GetComponent<TrackerManager>().GetScript(OnContinueButtonClicked);
                break;
            case "ButtonExit":
                tracker.GetComponent<TrackerManager>().GetScript(OnExitButtonClicked);
                break;
            default:
                break;
        }
    }

    void OnContinueButtonClicked()
    {
        SceneManager.LoadScene(GameObject.Find("Result").GetComponent<Remember>().PreviousScene);
    }

    void OnExitButtonClicked()
    {
        SceneManager.LoadScene(1);
    }
}
