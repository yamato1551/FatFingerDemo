using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIColumn : MonoBehaviour
{
    GameObject Button;
    GameObject canvas;
    RectTransform rt;
    Vector2 pos;
    Vector2 margin;
    // Start is called before the first frame update
    void Start()
    {
        margin.x = 25;
        margin.y = -25;
        rt = GetComponent<RectTransform>();
        pos = rt.anchoredPosition;
        pos.x = -Screen.width * 0.5f + rt.rect.width * 0.5f + margin.x;
        pos.y = Screen.height * 0.5f + rt.rect.height * 0.5f + margin.y;
        Button = Resources.Load<GameObject>("Prefab/Button");
        canvas = GameObject.Find("Canvas");
        for (int i = 0; i < 10; i++)
        {
            margin.x +=25;
            Placement();
        }
    }
    void Placement()
    {
        Button = Instantiate(Button, pos, Quaternion.identity) as GameObject;
        Button.transform.parent = canvas.transform;
    }
}
