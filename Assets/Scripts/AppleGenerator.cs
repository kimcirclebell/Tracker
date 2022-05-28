using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppleGenerator : MonoBehaviour
{
    public int maxApple;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    public int appleCount;
    public int appleCollectedCount;

    public GameObject applePrefeb;
    public Text appleText;

    private static AppleGenerator _instance;
    public static AppleGenerator Instance { get { return _instance; } }
    // Start is called before the first frame update
    void Start()
    {
        appleText = GameObject.Find("AppleText").GetComponent<Text>();
        //appleText.text = "Apple: " + appleCollectedCount;
        Init();
    }

    private void Init()
    {
        maxApple = 5;
        minX = -220.0f;
        maxX = 300.0f;
        minY = -70.0f;
        maxY = 240.0f;
        appleCount = 0;
        appleCollectedCount = 0;

        
        _instance = new AppleGenerator();
    }

    // Update is called once per frame
    void Update()
    {
        if(appleCount == 0)
        {
            
            for (int i=0; i<Random.Range(3, 5); i++)
            {
                GameObject apple = Instantiate(applePrefeb);
                apple.transform.parent = GameObject.Find("Apples").transform;
                float x = Random.Range(minX, maxX);
                float y = Random.Range(minY, maxY);
                apple.GetComponent<Image>().rectTransform.anchoredPosition = new Vector2(x, y);
                //Debug.Log(x + "\n" + y);
            }
            //Debug.Log(appleCount);
        }

        appleCount = GameObject.Find("Apples").transform.childCount;
        appleText.text = "Score: " + appleCollectedCount.ToString();
    }

    public void AddApple()
    {
        appleCollectedCount++;
    }
}
