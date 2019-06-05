using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    int[] A;
    int an;
    // Start is called before the first frame update
    void Start()
    {
        A = new int[11] { 1, 2, 4, 2, 5, 1, 3, 7, 4, 1, 3 };
        for(int i = 1; i < 11; i++)
        {
            an += A[i];
        }
        Debug.Log(an);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
