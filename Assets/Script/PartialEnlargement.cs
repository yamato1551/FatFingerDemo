using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//サブカメラの動き制御
public class PartialEnlargement : MonoBehaviour
{
    GameObject SubCam;//サブカメラ格納
    Touch touch = Input.GetTouch(0);
    Vector3 subcampos;//サブカメラのポジション
    Vector2 touchpos;//タッチポジション
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
        subcampos = SubCam.transform.position;
        touchpos = touch.position;
    }

    // Update is called once per frame
    void Update()
    {
        PreVal = Input.touches[0].pressure;
        switch (_method)
        {
            case Method.UpperPart://拡大画面が上部の場合
                subcampos.x = touchpos.x;
                subcampos.y = touchpos.y;
                subcampos.z = PreVal;
                subcampos = new Vector3(subcampos.x, subcampos.y, subcampos.x);
                break;
            case Method.Lens://拡大画面が画面内の場合
                break;
        }
    }
}
