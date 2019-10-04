using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonTap : MonoBehaviour
{
    public bool FingerRangeflag;
    Image ThisUI;
    void Start()
    {
        this.ThisUI = this.GetComponent<Image>();
    }
    void Update()
    {
        //UIの発行--------------------------------------
        float alpha = Mathf.PingPong(Time.time, 1);
        ThisUI.color = new Color(1f, 1f, 1f, alpha);
        //----------------------------------------------

        //ボタンの判定取得-------------------------------
        var buttonpos = this.gameObject.transform.position;
        var buttonsize = this.gameObject.GetComponent<RectTransform>().sizeDelta;
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            var touchpos = touch.position;

            if (buttonpos.x - (buttonsize.x / 2) < touchpos.x
           && buttonpos.x + (buttonsize.x / 2) > touchpos.x
           && buttonpos.y - (buttonsize.y / 2) < touchpos.y
           && buttonpos.y + (buttonsize.y / 2) > touchpos.y)
            {
                if (touch.phase == TouchPhase.Ended)
                {
                    FingerRangeflag = true;
                }
            }
        }
        //----------------------------------------------
    }
}
