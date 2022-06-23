using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainSceneManager : MonoBehaviour
{
    GameObject buttonStart;
    GameObject tracker;
    // Start is called before the first frame update
    void Start()
    {
        buttonStart = GameObject.Find("StartButton");
        tracker = GameObject.Find("tracker");
        Init();
    }

    private void Init()
    {
        buttonStart.SetActive(true);
        buttonStart.GetComponent<Button>().onClick.AddListener(onStartButtonClick);
        tracker.GetComponent<TrackerManager>().GetScript(onStartButtonClick);
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
