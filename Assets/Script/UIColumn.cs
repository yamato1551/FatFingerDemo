using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIColumn : MonoBehaviour
{
    public GameObject Button;
    // Start is called before the first frame update
    void Start()
    {
        GameObject prefab = (GameObject)Instantiate(Button);
        Button.gameObject.transform.SetParent(this. transform);
    }
}
