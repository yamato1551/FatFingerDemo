using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//サブカメラの動き制御
public class PartialEnlargement : MonoBehaviour
{
    GameObject SubCam;//サブカメラ格納
    Camera _SubCam;
    Touch touch;
    Vector3 subcampos;//サブカメラのポジション
    Vector2 touchpos//タッチポジション
        , subcamrect;//サブカメラの発生位置
        

    float PreVal;//圧力値
    public enum Method
    {
        UpperPart,
        Lens,
    }
    public Method _method;
    // Start is called before the first frame update
    void Start()
    {
        SubCam = GameObject.Find("SubCamera");
        _SubCam = SubCam.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        subcampos = SubCam.transform.position;
     
        switch (_method)
        {
            case Method.UpperPart://拡大画面が上部の場合
                touchState();
                subcampos.x = touchpos.x;
                subcampos.y = touchpos.y+60;
                subcampos.z = -984+(PreVal*100);
                SubCam.transform.position = new Vector3(subcampos.x, subcampos.y, subcampos.z);
                Debug.Log("cam:" + subcampos);
                Debug.Log("touch:" + touch.position);
                break;
            case Method.Lens://拡大画面が画面内の場合
                touchState();
                subcampos.x = touchpos.x;
                subcampos.y = touchpos.y - 20;
                subcampos.z = -984 + (PreVal * 100);
                SubCam.transform.position = new Vector3(subcampos.x, subcampos.y, subcampos.z);
                subcamrect.x = (touchpos.x / 1080)-0.1f;
                subcamrect.y = (touchpos.y / 1920)+0.1f;
                _SubCam.rect = new Rect(subcamrect.x,subcamrect.y, 0.2f, 0.1f);
                Debug.Log("touch:" + touch.position);
                break;
        }
    }
    void touchState()//タッチしたさいの位置と圧力取得
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            touchpos = touch.position;
        }
        if (Input.touches[0].pressure > 0)
        {
            PreVal = Input.touches[0].pressure;
        }

    }
}
