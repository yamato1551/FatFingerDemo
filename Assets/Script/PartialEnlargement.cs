using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    float PreVal;//圧力値
    public GameObject[] EnableObj;//見えなくするオブジェクト
    public enum Method
    {
        UpperPart,
        Lens,
    }
    public Method _method;
    // Start is called before the first frame update
    void Start()
    {
        TouchposUI = GameObject.Find("Image/Canvas/TouchPoint");
        SubCam = GameObject.Find("SubCamera");
        _SubCam = SubCam.GetComponent<Camera>();
        touchpos.x = 0; touchpos.y = 0;
        subcampos.x = 0; subcampos.y = 0;
        subcamrect.x = 0; subcamrect.y = 0;
        for (int i = 0; i < EnableObj.Length; i++)//開始時に特定のオブジェクトを見えなくする
        {
            EnableObj[i].SetActive(false);
        }
    }
    void Update()
    {
        subcampos = SubCam.transform.position;
        touchState();
        Debug.Log(touchpos);
        //TouchLocation();
        switch (_method)//カメラの配置やuiの配置は手動
        {
            case Method.UpperPart://拡大画面が上部の場合,サブカメラの位置を変更しているだけ
                subcampos.x = touchpos.x;
                subcampos.y = touchpos.y + 60;
                subcampos.z = -1000 + (PreVal * 100);
                SubCam.transform.position = new Vector3(subcampos.x, subcampos.y, subcampos.z);
                Debug.Log("cam:" + subcampos);
                Debug.Log("touch:" + touch.position);
                break;
            case Method.Lens://拡大画面が画面内の場合

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
            }
        }
    }
}
