using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI3DtouchZoom : MonoBehaviour
{
    GameObject panel;
    Vector2 scale;
    bool push;
    RectTransform rect,canvas;
    Vector2 touchpos,_touchpos;
    public int BlankArea;
    [System.Serializable]
    struct RangeClass
    {
        public int min, max;
    }
    [SerializeField]
    private RangeClass ScaleLimit;

    void Start()
    {
        rect = GameObject.Find("Canvas/BackGround").GetComponent<RectTransform>();
        canvas = GameObject.Find("Canvas").GetComponent<RectTransform>();
        panel = GameObject.Find("Canvas/BackGround");
    }

    void Update()
    {
       
        touchZoom();
        touchMove();
        //UIOutSide();

    }

    void touchZoom()
    {
   
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            touchpos = touch.position;
            if (touch.phase == TouchPhase.Began)
            {
                rect.position = new Vector2(touchpos.x, touchpos.y);

                push = true;
            }
            if (touch.phase == TouchPhase.Ended)
            {
                push = false;
                rect.localPosition = new Vector2(0,0);
            }
        }
        if (push)
        {
            if (Input.touches[0].pressure > 0)
            {
                //Debug.Log(Input.touches[0].pressure);
                scale.x = (Input.touches[0].pressure + 1)*1.5f;
                scale.x = Mathf.Clamp(scale.x, ScaleLimit.min, ScaleLimit.max);
                scale.y = scale.x;
            }
          
            panel.transform.localScale = new Vector2(scale.x, scale.y);
        }
        else
        {
            panel.transform.localScale = new Vector2(1, 1);
            rect.localPosition = new Vector2(0, 0);
        }
      
    }
    void touchMove()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            //touchpos = touch.position;
            //Debug.Log(touchpos);
            if (touch.phase == TouchPhase.Began)
            {
                _touchpos = touchpos;
            }
            if (push)
            {
                var touchlocalpos = canvas.localPosition;

                if ((Screen.width * 0.5f)+BlankArea < touchpos.x)//右
                {
                    _touchpos.x -= 1;
                }
                if ((Screen.width * 0.5f)-BlankArea > touchpos.x)//左
                {
                    _touchpos.x += 1;
                }
                if ((Screen.height * 0.5f)+BlankArea < touchpos.y)//上
                {
                    _touchpos.y -= 1;
                }
                if ((Screen.height * 0.5f)-BlankArea > touchpos.y)//下
                {
                    _touchpos.y += 1;
                }
               
            }
            rect.position = new Vector2(_touchpos.x, _touchpos.y);
        }
    }
    void UIOutSide()
    {
        var pos = rect.localPosition;
        var _pos = panel.transform.position;

        //var _pos = pos;//最初の値格納
        //右端
        if (pos.x + _pos.x < Screen.width / 2)
        {
            //pos.x = pos.x+_pos.x;
        }
        //左端
        if (pos.x-_pos.x < Screen.width - Screen.width)
        {
            //pos.x = 0;
        }
        // Debug.Log(pos.x - _pos.x);
        Debug.Log(pos);
        rect.localPosition = new Vector2(pos.x, pos.y);

    }
}
