using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Remember : MonoBehaviour
{
    private List<float> _listfloat;
    public List<float> Listfloat { get { return _listfloat; } set { _listfloat = value; } }

    private int _previousScene;
    public int PreviousScene { get { return _previousScene; } set { _previousScene = value; } }

    private void Start()
    {
        _listfloat = new List<float>();
        _previousScene = 0;
    }
}
