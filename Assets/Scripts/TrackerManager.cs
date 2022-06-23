using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrackerManager : MonoBehaviour
{
    [SerializeField]
    private string _collision2D;
    public string Collision2D { get { return _collision2D; } set { _collision2D = value; } }

    [SerializeField]
    float triggeredTime;
    Action triggerAction;

    private void Start()
    {
        _collision2D = "";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.transform.localScale = new Vector3(1.1f, 1.1f, 1.0f);
        _collision2D = collision.name;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        _collision2D = "";
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(triggeredTime >= 1.5f)
        {
            triggeredTime = 0.0f;
            triggerAction.Invoke();
        }
    }

    private void Update()
    {
        if(_collision2D != "")
        {
            triggeredTime += Time.deltaTime;
        }
        else
        {
            triggeredTime = 0.0f;
        }
    }
    public void GetScript(Action script)
    {
        triggerAction = script;
    }
}
