using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ApplePickResultManager : MonoBehaviour
{
    [SerializeField]
    GameObject resultText;
    [SerializeField]
    GameObject OKButton;
    [SerializeField]
    GameObject tracker;

    List<float> result;

    string resultTextCombiner;

    // Start is called before the first frame update
    void Start()
    {
        tracker = GameObject.Find("tracker");
        resultText = GameObject.Find("Results");
        result = new List<float>();
        OKButton = GameObject.Find("ButtonOK");
        resultTextCombiner = "";
        Init();
    }

    private void Init()
    {
        OKButton.GetComponent<Button>().onClick.AddListener(OnOKButtonClicked);
        tracker.GetComponent<TrackerManager>().GetScript(OnOKButtonClicked);
        result = GameObject.Find("Result").GetComponent<Remember>().Listfloat;
        for(int i=0; i<result.Count; i++)
        {
            resultTextCombiner = resultTextCombiner + i.ToString() + "번째 걸린 시간: " + result[i].ToString() + "초" + "\n";
        }
        resultText.GetComponent<Text>().text = resultTextCombiner;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnOKButtonClicked()
    {
        SceneManager.LoadScene(1);
    }
}
