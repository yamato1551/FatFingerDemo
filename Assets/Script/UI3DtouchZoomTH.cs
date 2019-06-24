using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UI3DtouchZoomTH : MonoBehaviour
{
    void Start()
    {
    }
    void Update()
    {
        ontouch();
    }
    void ontouch()
    {
        var buttonpos = this.gameObject.transform.position;
        var buttonsize = this.gameObject.GetComponent<RectTransform>().sizeDelta;

        if (Input.touchCount > 0)
        {

            Touch touch = Input.GetTouch(0);
            var touchpos = touch.position;
            //ボタン範囲内の場合
            if (buttonpos.x - (buttonsize.x / 2) < touchpos.x
            && buttonpos.x + (buttonsize.x / 2) > touchpos.x
            && buttonpos.y - (buttonsize.y / 2) < touchpos.y
            && buttonpos.y + (buttonsize.y / 2) > touchpos.y)
            {
                if (Input.touches[0].pressure > 5)
                {
                    SceneManager.LoadScene("DammyScene");
                }
            }
            
        }
    }
}

