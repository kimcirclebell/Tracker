using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainSceneManager : MonoBehaviour
{
    GameObject buttonStart;
    GameObject tracker;


    GameObject Difficulty;
    GameObject dif;
    GameObject result;
    // Start is called before the first frame update
    void Start()
    {
        buttonStart = GameObject.Find("StartButton");
        tracker = GameObject.Find("tracker");
        Init();
    }

    void Awake()
    {
        if(GameObject.Find("Result"))
            GameObject.Find("Result").GetComponent<Remember>().PreviousScene = 0;
    }

    private void Init()
    {
        buttonStart.SetActive(true);
        buttonStart.GetComponent<Button>().onClick.AddListener(onStartButtonClick);
        tracker.GetComponent<TrackerManager>().GetScript(onStartButtonClick);

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
    }

    public void onStartButtonClick()
    {
        SceneManager.LoadScene(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
