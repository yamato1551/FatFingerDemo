using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIAction : MonoBehaviour
{
    public Sprite MainUI,SecondUI;
    Image ThisUI;
    bool changeflag = true;
    public bool FingerRangeflag=false;
    public AudioClip Sound;
    private AudioSource audiosouce;
    public enum trans{
        changeUI,
        MoveUI,
        OnSound
    }
    public trans _trans;
    void Start()
    {
        audiosouce = gameObject.GetComponent<AudioSource>();
        ThisUI = this.GetComponent<Image>();
        ThisUI.sprite = MainUI;
    }

    // Update is called once per frame
    void Update()
    {
        UIrange();
        switch (_trans)
        {
            case trans.changeUI:
                UIChange();
                break;
            case trans.MoveUI:
                UIMove();
                break;
            case trans.OnSound:
                PushOnSound();
                break;
        }

    }

    void UIChange()
    {
        
        if (FingerRangeflag == true)
        {
            Debug.Log("呼び出された");
            if (changeflag)
            {
                ThisUI.sprite = MainUI;
                FingerRangeflag = false;
                changeflag = false;
            }
            else
            {
                ThisUI.sprite = SecondUI;
                FingerRangeflag = false;
                changeflag = true;
            }
            FingerRangeflag = false;
        }
    }
    void UIMove()
    {
        
        if (FingerRangeflag == true)
        {
            if (changeflag)
            {
            
                changeflag = false;              
            }
            else
            {
                changeflag = true;
            }
            FingerRangeflag = false;
        }
        if (changeflag)
        {
            Vector3 initialPosition = transform.position;
            transform.position = new Vector3(Mathf.Sin(Time.time * 3) * 5 + initialPosition.x, initialPosition.y, initialPosition.z);
        }
    }

    void PushOnSound()
    {
       
        audiosouce.clip = Sound;
        if (FingerRangeflag == true)
        {
            audiosouce.Play();
            FingerRangeflag = false;
        }
    }
    void UIrange()
    {
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
    }
}
