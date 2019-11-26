﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
//サブカメラの動き制御
public class PartialEnlargement : MonoBehaviour
{
    GameObject SubCam;//サブカメラ格納
    GameObject TouchposUI;//タッチした場所に出るUI
    Camera _SubCam;
    Touch touch;
    Vector3 subcampos;//サブカメラのポジション
    Vector2 touchpos//タッチポジション
        , subcamrect;//サブカメラの発生位置
    bool onflag = false;//圧力検知を行わせるフラグ
    bool Lensflag = true;
    float PreVal;//圧力値
    MagnifierCheck macheck;
    public GameObject[] EnableObj;//見えなくするオブジェクト
    public Text minmaxText;
    public int notInitializationTimes;
    public RectTransform MainField;
    public enum Method
    {
        Neutral,
        Lens,
    }
    public enum SubCameraLensPosition
    {
        Upper,
        Left,
        UpperLeft
    }
    public Method _method;
    public SubCameraLensPosition _SubCamLensPos;
    // Start is called before the first frame update
    void Start()
    {
        Npos(540,960);
        //macheck = GameObject.Find("Canvas/LensOnOff").GetComponent<MagnifierCheck>();
        minmaxText = minmaxText.GetComponent<Text>();
        TouchposUI = GameObject.Find("Canvas/TouchPoint");
        MainField = GameObject.Find("Canvas/MainField").GetComponent<RectTransform>();
        SubCam = GameObject.Find("SubCamera");
        _SubCam = SubCam.GetComponent<Camera>();
        for (int i = 0; i < EnableObj.Length; i++)//開始時に特定のオブジェクトを見えなくする
        {
            EnableObj[i].SetActive(false);
        }
        SubjectNumber(notInitializationTimes);
    }
    void Update()
    {
        if (Lensflag)
        {
            subcampos = SubCam.transform.position;
            touchState();

            TouchLocation();
        }
        else
        {
            for (int i = 0; i < EnableObj.Length; i++)//開始時に特定のオブジェクトを見えなくする
            {
                EnableObj[i].SetActive(false);
            }
        }

        SwitchNorL();
        MinMaxTextChange();
    }
    void SwitchNorL()
    {
        switch (_method)//カメラの配置やuiの配置は手動
        {
            case Method.Neutral://拡大画面が上部の場合,サブカメラの位置を変更しているだけ
                subcampos.x = touchpos.x;
                subcampos.y = touchpos.y;
                subcampos.z = -1000 + (PreVal * 500);
                SubCam.transform.position = new Vector3(subcampos.x, subcampos.y, subcampos.z);
                SwitchPos();
                break;
            case Method.Lens://拡大画面が画面内の場合
                //Lensflag = macheck.Checkflag;

                //サブカメラの位置------------------------
                subcampos.x = touchpos.x;
                subcampos.y = touchpos.y;
                subcampos.z = -1000 + (PreVal * 100);
                //レンズの位置----------------------------
                subcamrect.x = (touchpos.x / 1080) - 0.1f;
                subcamrect.y = (touchpos.y / 1920) + 0.1f;
                //----------------------------------------
                if (touchpos.y >= 1500)//レンズがはみ出してしまう場合の位置変更
                {
                    //subcampos.y = touchpos.y - 60;
                    subcamrect.x = (touchpos.x / 1080) - 0.1f;
                    subcamrect.y = (touchpos.y / 1920) - 0.3f;
                }
                SubCam.transform.position = new Vector3(subcampos.x, subcampos.y, subcampos.z);
                _SubCam.rect = new Rect(subcamrect.x, subcamrect.y, 0.3f, 0.15f);
                _SubCam.fieldOfView = 25;
                break;
        }

    }
    void SwitchPos()
    {
        
        switch (_SubCamLensPos)
        {
            case SubCameraLensPosition.Upper:
                _SubCam.rect = new Rect(0, 0.9f, 1, 0.1f);
                _SubCam.fieldOfView = 12;
                MainField.localPosition = new Vector3(0, -200, 0);
                break;
            case SubCameraLensPosition.Left:
                _SubCam.rect = new Rect(0, 0, 0.2f, 1);
                _SubCam.fieldOfView = 100;
                MainField.localPosition = new Vector3(0, 0, 0);
                break;
            case SubCameraLensPosition.UpperLeft:
                _SubCam.rect = new Rect(0, 0.9f, 0.2f, 0.1f);
                _SubCam.fieldOfView = 25;
                MainField.localPosition = new Vector3(0, 0, 0);
                break;
        }
        
    }
    void TouchLocation()
    {

        TouchposUI.transform.position = touchpos;

    }
    void touchState()//タッチした際の位置と圧力取得
    {

        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            touchpos = touch.position;
            if (touch.phase == TouchPhase.Began)
            {
                onflag = true;
                for (int i = 0; i < EnableObj.Length; i++)//開始時に特定のオブジェクトを見えなくする
                {
                    EnableObj[i].SetActive(true);
                }
            }
            if (touch.phase == TouchPhase.Ended)
            {
                onflag = false;
                for (int i = 0; i < EnableObj.Length; i++)//開始時に特定のオブジェクトを見えなくする
                {
                    EnableObj[i].SetActive(false);

                }
            }

        }
        if (onflag == true)
        {
            if (Input.touches[0].pressure > 0)
            {
                PreVal = Input.touches[0].pressure;
                PreVal = Mathf.Clamp(PreVal, 0, 1.5f);
                //ModeratePressure();
            }
        }
    }
    void MinMaxTextChange()
    {
        if (Input.touchCount > 0)
        {
            // タッチ情報の取得
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Ended)
            {
                //圧力値、表示圧力を初期化
                minmaxText.text = "Min=0,Max=100 : 0";
                PreVal = 0;
            }

            if (touch.phase == TouchPhase.Moved)//画面に触れているときに圧力値を表示する
            {
                minmaxText.text = "Min=0,Max=100 : " + Mathf.FloorToInt(((PreVal * 100) / 150) * 100);
            }
        }

    }
    void SubjectNumber(int num)
    {
        StreamWriter sw = new StreamWriter(Application.dataPath + "/TextData.txt", true);
        sw.WriteLine("被験者番号:" + num);// ファイルに書き出したあと改行
        sw.Flush();// StreamWriterのバッファに書き出し残しがないか確認
        sw.Close();// ファイルを閉じる
    }
    void Npos(float Nposx,float NposY)
    {
        touchpos.x = Nposx;
        touchpos.y = NposY;
    }
    void ModeratePressure()
    {
        if (30 >= ((PreVal * 100) / 150) * 100)
        {
            PreVal = 0.45f;//30
        }
        else if (30 <= ((PreVal * 100) / 150) * 100 && 40 >= ((PreVal * 100) / 150) * 100)
        {
            PreVal = 0.525f;//35
        }
        else if (40 <= ((PreVal * 100) / 150) * 100 && 50 >= ((PreVal * 100) / 150) * 100)
        {
            PreVal = 0.675f;//45
        }
        else if (50 <= ((PreVal * 100) / 150) * 100 && 60 >= ((PreVal * 100) / 150) * 100)
        {
            PreVal = 0.825f;//55
        }
        else if (60 <= ((PreVal * 100) / 150) * 100 && 80 >= ((PreVal * 100) / 150) * 100)
        {
            PreVal = 1.05f;//70
        }
        else if (80 <= ((PreVal * 100) / 150) * 100 && 95 >= ((PreVal * 100) / 150) * 100)
        {
            PreVal = 1.35f;//90
        }
        else if (95 <= ((PreVal * 100) / 150) * 100)
        {
            PreVal = 1.5f;//100
        }
    }
}
