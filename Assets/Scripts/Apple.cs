using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    AppleGenerator appleGenerator;
    // Start is called before the first frame update
    void Awake()
    {
        appleGenerator = GameObject.Find("BG").GetComponent<AppleGenerator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        appleGenerator.AddApple();
        Destroy(this.gameObject);
    }
}
