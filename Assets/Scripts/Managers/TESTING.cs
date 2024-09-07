using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class TESTING : MonoBehaviour
{
    public List<int> test = new()
        {
            1,2
        };

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            test.RemoveAt(0);
        }
    }
}
