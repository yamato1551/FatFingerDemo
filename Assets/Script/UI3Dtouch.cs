using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI3Dtouch : MonoBehaviour
{
    GameObject Button,_Button;
    Vector2 pos,Size;
    void Start()
    {
        Button = Resources.Load<GameObject>("Prefab/3DtouchButton");
        _Button = this.gameObject;
        pos = this.gameObject.transform.position;
        Size = _Button.GetComponent<RectTransform>().sizeDelta;
        Debug.Log(Size);
    }
    public void OnButtonPressed()
    {
        Debug.Log("押された");

        Debug.Log(Input.touches[0].pressure);
        if (Input.touches.Length>0)
        {
            pos.y += Size.y + 10;
            Placement();
           
        }
    }
    void Placement()
    {
        Button = Instantiate(Button, pos, Quaternion.identity) as GameObject;
        Button.transform.parent = this.gameObject.transform;
    }
}
