using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Remember : MonoBehaviour
{
    private List<float> _listfloat;
    public List<float> Listfloat { get { return _listfloat; } set { _listfloat = value; } }

    private void Start()
    {
        _listfloat = new List<float>();
    }
}
