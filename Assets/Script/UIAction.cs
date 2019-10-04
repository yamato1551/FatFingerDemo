using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIAction : MonoBehaviour
{
    public Sprite MainSprite, SecondSprite;
    public GameObject PoPUI;
    GameObject canvas;
    Image MainUI;
    bool changeflag = false,TapFlag;

    public AudioClip Sound;
    private AudioSource audiosouce;
    float acceleration = 1;
    ButtonTap buttontap;
    public enum trans{
        changeUI,
        MoveUI,
        OnSound,
        UIPop,
        ChangeandMove
    }
    public trans _trans;
    void Start()
    {
        buttontap = GetComponentInChildren<ButtonTap>();
        canvas = GameObject.Find("Canvas");
        audiosouce = gameObject.GetComponent<AudioSource>();
        MainUI = gameObject.GetComponent<Image>();
        MainUI.sprite = MainSprite;
       

    }

    void Update()
    {
        TapFlag = buttontap.FingerRangeflag;//別スクリプトからフラグ取得
       
        //動作変更
        switch (_trans)
        {
            case trans.changeUI:
                UIChange();
                break;
            case trans.MoveUI:
                UIMove();
                break;
            case trans.OnSound:
                SoundOn();
                break;
            case trans.UIPop:
                PopUI();
                break;
            case trans.ChangeandMove:
                MoveandChange();
                break;
        }

    }

    void UIChange()
    {
        
        if (TapFlag == true)
        {
            Debug.Log("呼び出された");
            if (changeflag)
            {
                MainUI.sprite = MainSprite;
                TapFlag = false;
                changeflag = false;
            }
            else
            {
                MainUI.sprite = SecondSprite;
                TapFlag = false;
                changeflag = true;
            }
            TapFlag = false;
        }
    }
    void UIMove()
    {
        
        if (TapFlag == true)
        {
            if (changeflag)
            {
            
                changeflag = false;              
            }
            else
            {
                changeflag = true;
            }
            TapFlag = false;
        }
        if (changeflag)
        {
            Vector3 initialPosition = transform.position;
            transform.position = new Vector3(Mathf.Sin(Time.time * 3) * 5 + initialPosition.x, initialPosition.y, initialPosition.z);
        }
    }

    void SoundOn()
    {
       
        audiosouce.clip = Sound;
        if (TapFlag == true)
        {
            audiosouce.Play();
            TapFlag = false;
        }
    }
    void PopUI()
    {
        var UIpos = this.gameObject.transform.position;

        if (TapFlag == true)
        {
            var PrefabObj=Instantiate(PoPUI, new Vector2(UIpos.x, UIpos.y+20), Quaternion.identity);
            PrefabObj.transform.parent = canvas.transform;
            TapFlag = false;
        }
    }
    void MoveandChange()
    {
        if (TapFlag)
        {
           
            MainUI.sprite = SecondSprite;
            acceleration = acceleration * 1.05f;
            this.gameObject.transform.Translate(0, acceleration, 0);
        }
    }
  
}
