using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI3DtouchZoom : MonoBehaviour
{
    GameObject panel;
    Vector2 scale;
    bool push;
    public RectTransform rect;
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
        panel = GameObject.Find("Canvas/BackGround");
    }

    void Update()
    {
        touchZoom();
    }

    void touchZoom()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            var touchpos = touch.position;
            if (touch.phase == TouchPhase.Began)
            {
                push = true;
                rect.localPosition = new Vector2(touchpos.x,touchpos.y);
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
                scale.x = Input.touches[0].pressure + 1;
                scale.x = Mathf.Clamp(scale.x, ScaleLimit.min, ScaleLimit.max);
                scale.y = scale.x;
            }
          
            panel.transform.localScale = new Vector2(scale.x, scale.y);
        }
        else
        {
            panel.transform.localScale = new Vector2(1, 1);
        }
    }
}
