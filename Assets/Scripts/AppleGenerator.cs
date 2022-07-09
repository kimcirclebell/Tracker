using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AppleGenerator : MonoBehaviour
{
    float minX;
    float maxX;
    float minY;
    float maxY;
    float leftoverTime;
    int applesNumber;
    int maxApple;
    public int appleCollectedCount;
    public int stage;


    [SerializeField]
    private List<float> limitTime;
    [SerializeField]
    private List<int> appleCount;
    [SerializeField]
    private List<int> rottenAppleCount;
    [SerializeField]
    private List<float> usedTime;

    [SerializeField]
    private bool isAppleGenerated;

    public GameObject applePrefeb;
    public GameObject rottenApplePrefeb;
    private GameObject apples;

    private Text leftTime;
    private Text Score;

    private static AppleGenerator _instance;
    public static AppleGenerator Instance { get { return _instance; } }
    // Start is called before the first frame update
    void Start()
    {
        limitTime = new List<float>();
        appleCount = new List<int>();
        rottenAppleCount = new List<int>();
        usedTime = new List<float>();
        Init();
    }

    private void Init()
    {
        minX = -500.0f;
        maxX = 500.0f;
        minY = -50.0f;
        maxY = 300.0f;
        leftoverTime = 100.0f;
        appleCollectedCount = 0;
        stage = 0;
        applesNumber = 0;
        maxApple = 10;
        isAppleGenerated = false;
        apples = GameObject.Find("Apples");

        leftTime = GameObject.Find("LeftTime").GetComponent<Text>();
        Score = GameObject.Find("Score").GetComponent<Text>();

        if (GameObject.Find("Easy"))
        {
            Debug.Log("Level is Easy");
            limitTime.Add(60.0f);
            limitTime.Add(60.0f);
            limitTime.Add(50.0f);
            limitTime.Add(45.0f);
            limitTime.Add(40.0f);

            appleCount.Add(5);
            appleCount.Add(7);
            appleCount.Add(10);
            appleCount.Add(13);
            appleCount.Add(15);

            rottenAppleCount.Add(0);
            rottenAppleCount.Add(1);
            rottenAppleCount.Add(2);
            rottenAppleCount.Add(2);
            rottenAppleCount.Add(2);
        }
        else if (GameObject.Find("Hard"))
        {
            Debug.Log("Level is Easy");
            limitTime.Add(60.0f);
            limitTime.Add(50.0f);
            limitTime.Add(45.0f);
            limitTime.Add(40.0f);
            limitTime.Add(35.0f);

            appleCount.Add(10);
            appleCount.Add(15);
            appleCount.Add(20);
            appleCount.Add(25);
            appleCount.Add(30);

            rottenAppleCount.Add(0);
            rottenAppleCount.Add(2);
            rottenAppleCount.Add(2);
            rottenAppleCount.Add(2);
            rottenAppleCount.Add(2);
        }


        
        _instance = new AppleGenerator();
    }

    void Awake()
    {
        if (GameObject.Find("Result"))
            GameObject.Find("Result").GetComponent<Remember>().PreviousScene = 2;
    }

    // Update is called once per frame
    void Update()
    {
        Score.text = appleCollectedCount.ToString() + "/" + appleCount[stage];
        leftTime.text = "제한시간\n" + leftoverTime.ToString();
        leftoverTime -= Time.deltaTime;

        if(leftoverTime < 0)
        {
            SceneManager.LoadScene(4);
        }

        if (!isAppleGenerated)
        {
            //라운드가 넘어갔으면
            leftoverTime = limitTime[stage];
            isAppleGenerated = true;

            for(int i=0; i< rottenAppleCount[stage]; i++)
            {
                RottenAppleGenerate(applesNumber);
            }

        }

        if(applesNumber< maxApple)
        {
            AppleGenerate(applesNumber);
        }

        if(appleCollectedCount >= appleCount[stage])
        {
            usedTime.Add(limitTime[stage] - leftoverTime);
            stage++;
            if (stage > 4)
            {
                GameObject.Find("Result").GetComponent<Remember>().Listfloat = usedTime;
                SceneManager.LoadScene(3);
            }
            appleCollectedCount = 0;
            isAppleGenerated = false;

            for(int i=0; i<apples.transform.childCount; i++)
            {
                Destroy(apples.transform.GetChild(i).gameObject);
            }
            applesNumber = 0;
        }
    }

    public void AddApple()
    {
        appleCollectedCount++;
        applesNumber--;
    }

    public void AddRottenApple()
    {
        leftoverTime -= 5.0f;
        applesNumber--;
    }

    private void AppleGenerate(int appleNum)
    {
        GameObject apple = Instantiate(applePrefeb);
        apple.transform.parent = apples.transform;
        bool Connected = true;


        //겹치지 않을때 까지 반복
        while (Connected)
        {
            //랜덤 좌표 생성 
            float x = Random.Range(minX, maxX);
            float y = Random.Range(minY, maxY);
            apple.GetComponent<Image>().rectTransform.localPosition = new Vector2(x, y);
            Debug.Log("X = " + x + ",  Y = " + y);
            Connected = false;

            //트래커를 찾아서 트래커와 너무 가까우면 재배치
            Vector3 tracker = GameObject.Find("tracker").gameObject.transform.position;

            float dist = Mathf.Sqrt(Mathf.Pow(apple.transform.position.x - tracker.x, 2) + Mathf.Pow(apple.transform.position.y - tracker.y, 2));
            Debug.Log(appleNum + "번째 사과와 트래커와의 거리" + dist.ToString());
            if (dist < 100.0f)
            {
                Debug.Log(appleNum + "번째 사과가 트래커와 너무 가까워서 재설정");
                Connected = true;
            }

            //다른 사과의 위치를 봐서 너무 가까우면 재배치
            for (int j = 0; j < appleNum; j++)
            {

                dist = Mathf.Sqrt(Mathf.Pow(apple.transform.position.x - apples.transform.GetChild(j).transform.position.x, 2) +
                    Mathf.Pow(apple.transform.position.y - apples.transform.GetChild(j).transform.position.y, 2));
                Debug.Log(appleNum + "번째 사과와 " + j + "번째 사과의 거리" + dist.ToString());
                if (dist < 100.0f)
                {
                    Debug.Log(appleNum + "번째 사과가 " + j + "번째 사과랑 너무 가까워서 재설정");
                    Connected = true;
                }
            }

        }
        applesNumber++;
    }

    private void RottenAppleGenerate(int appleNum)
    {
        GameObject rottenApple = Instantiate(rottenApplePrefeb);
        rottenApple.transform.parent = apples.transform;
        bool Connected = true;


        //겹치지 않을때 까지 반복
        while (Connected)
        {
            //랜덤 좌표 생성 
            float x = Random.Range(minX, maxX);
            float y = Random.Range(minY, maxY);
            rottenApple.GetComponent<Image>().rectTransform.localPosition = new Vector2(x, y);
            Debug.Log("X = " + x + ",  Y = " + y);
            Connected = false;

            //트래커를 찾아서 트래커와 너무 가까우면 재배치
            Vector3 tracker = GameObject.Find("tracker").gameObject.transform.position;

            float dist = Mathf.Sqrt(Mathf.Pow(rottenApple.transform.position.x - tracker.x, 2) + Mathf.Pow(rottenApple.transform.position.y - tracker.y, 2));
            Debug.Log(appleNum + "번째 사과와 트래커와의 거리" + dist.ToString());
            if (dist < 100.0f)
            {
                Debug.Log(appleNum + "번째 사과가 트래커와 너무 가까워서 재설정");
                Connected = true;
            }

            //다른 사과의 위치를 봐서 너무 가까우면 재배치
            for (int j = 0; j < appleNum; j++)
            {

                dist = Mathf.Sqrt(Mathf.Pow(rottenApple.transform.position.x - apples.transform.GetChild(j).transform.position.x, 2) +
                    Mathf.Pow(rottenApple.transform.position.y - apples.transform.GetChild(j).transform.position.y, 2));
                Debug.Log(appleNum + "번째 사과와 " + j + "번째 사과의 거리" + dist.ToString());
                if (dist < 100.0f)
                {
                    Debug.Log(appleNum + "번째 사과가 " + j + "번째 사과랑 너무 가까워서 재설정");
                    Connected = true;
                }
            }

        }
        applesNumber++;
    }
}
