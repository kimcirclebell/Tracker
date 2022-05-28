using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using System;
using UnityEngine.UI;

public class HandTracker : MonoBehaviour
{
    public int portNumber = 22222;
    static UdpClient udpClient;
    public Image tracker;

        
    // Start is called before the first frame update
    void Start()
    {
        udpClient = new UdpClient(portNumber);
        tracker = GameObject.Find("tracker").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        IPEndPoint remoteEP = null;
        byte[] data = udpClient.Receive(ref remoteEP);
        string message = Encoding.ASCII.GetString(data);
        //print(message);

        double x = Math.Truncate(float.Parse(message) / 1000);
        double y = Math.Truncate(float.Parse(message) % 1000);

        tracker.rectTransform.anchoredPosition = new Vector2(-((float)x) * 1.5f + 900.0f, -((float)y)+500.0f);

        //Debug.Log("x = " + x + "\n y = " + y);
    }
}
