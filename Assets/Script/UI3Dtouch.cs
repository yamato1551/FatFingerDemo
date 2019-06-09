using UnityEngine;
public class UI3Dtouch : MonoBehaviour
{
    GameObject Button,_Button;
    Vector2 pos,Size;
    bool Push;
    int ButtonNum, TouchPressure;
    [SerializeField]
    GameObject[] instansButton=new GameObject[4];
    void Start()
    {
        Push = false;
        Button = Resources.Load<GameObject>("Prefab/3DtouchButton");
        _Button = this.gameObject;
        pos = this.gameObject.transform.position;
        Size = _Button.GetComponent<RectTransform>().sizeDelta;
    }
    public void ButtonPressed()
    {
        Push = true;
        Debug.Log("押された");
    }
    public void ButtonRelease()
    {
        Push = false;
    }
    void Update()
    {
        Debug.Log(Push);
        foreach(GameObject i in instansButton)
        {
            Debug.Log(i);
        }

        if (Push)//3Dtouchで圧力によるUI生成
        {
            switch (TouchPressure) {
                case 0:

                    if (Input.touches.Length > 1)
                    {
                        pos.y += Size.y + 10;
                        Placement();
                        TouchPressure += 1;
                    }
                    break;

                case 1:

                    if (Input.touches.Length > 2)
                    {
                        ButtonNum += 1;
                        pos.x -= Size.x - 10;
                        Placement();
                        TouchPressure += 1;
                    }
                    if (Input.touches.Length < 1)
                    {
                        ButtonNum -= 1;
                        TouchPressure -= 1;
                        Destroy(instansButton[0]);
                    }
                    break;

                case 2:
                    if (Input.touches.Length > 3)
                    {
                        ButtonNum += 1;
                        pos.y -= Size.y - 10;
                        Placement();
                        TouchPressure += 1;
                    }
                    if (Input.touches.Length < 2)
                    {
                        ButtonNum -= 1;
                        TouchPressure -= 1;
                        Destroy(instansButton[1]);
                    }
                    break;

                case 3:
                    if (Input.touches.Length > 4)
                    {
                        ButtonNum += 1;
                        pos.x += Size.x + 10;
                        Placement();
                        TouchPressure += 1;
                    }

                    if(Input.touches.Length < 3)
                    {
                        ButtonNum -= 1;
                        TouchPressure -= 1;
                        Destroy(instansButton[2]);
                    }
                    break;

                case 4:
                    if (Input.touches.Length < 4)
                    {
                        ButtonNum -= 1;
                        TouchPressure -= 1;
                        Destroy(instansButton[3]);
                    }
                    break;
            }
        }

    }
    void Placement()
    {
        Button = Instantiate(Button, pos, Quaternion.identity) as GameObject;
        instansButton[ButtonNum] = Button;
        Button.transform.parent = this.gameObject.transform;
        pos = this.gameObject.transform.position;
    }
}
