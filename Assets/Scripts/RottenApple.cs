using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RottenApple : MonoBehaviour
{
    AppleGenerator appleGenerator;
    // Start is called before the first frame update
    void Awake()
    {
        appleGenerator = GameObject.Find("AppleGenerator").GetComponent<AppleGenerator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        appleGenerator.AddRottenApple();
        Destroy(this.gameObject);
    }
}
