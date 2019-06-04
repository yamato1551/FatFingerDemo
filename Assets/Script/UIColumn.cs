using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIColumn : MonoBehaviour
{
    GameObject Button;
    Button buttonscale;
    GameObject canvas;
    RectTransform rt;
    Vector2 pos, scale;
    public Vector2 buttonNum;
    public float _scale;
    // Start is called before the first frame update
    void Start()
    {
        scale.x = _scale;
        scale.y = _scale;
        Button = Resources.Load<GameObject>("Prefab/Button");
        buttonscale = Resources.Load<Button>("Prefab/Button");
        canvas = GameObject.Find("Canvas");
        rt = GetComponent<RectTransform>();
        RectTransform buttonRect = buttonscale.GetComponent<RectTransform>();
        buttonRect.sizeDelta = new Vector2(scale.x,scale.y);
        pos = rt.anchoredPosition;
        pos.x = -Screen.width * 0.5f + rt.rect.width * 0.5f + scale.x/2;
        pos.y = Screen.height * 0.5f + rt.rect.height * 0.5f - scale.y/2;
        Debug.Log(Screen.width);
        for (int I = 0; I < buttonNum.y; I++)
        {
            for (int i = 0; i < buttonNum.x; i++)
            {
               
                Placement();
                pos.x += (Screen.width - scale.x) / (buttonNum.x - 1);
            }
            pos.x = scale.x/2;
            pos.y -= (Screen.height - scale.y) / (buttonNum.y - 1);
        }
    }
    void Placement()
    {
        Button = Instantiate(Button, pos, Quaternion.identity) as GameObject;
        Button.transform.parent = canvas.transform;
    }
}
