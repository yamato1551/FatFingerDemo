using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TapErrorDebug : MonoBehaviour
{
    Image thisImage;
    public enum TouchResult
    {
        errorResult,
        clearResult
    }
    public TouchResult touchResult;
    void Start()
    {
        thisImage = this.gameObject.GetComponent<Image>();
    }
    void Update()
    {
        var buttonpos = this.gameObject.transform.position;
        var buttonsize = this.gameObject.GetComponent<RectTransform>().sizeDelta;
        thisImage.color = new Color(1, 1, 1, 0);

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            var touchpos = touch.position;

            if (buttonpos.x - (buttonsize.x / 2) < touchpos.x
           && buttonpos.x + (buttonsize.x / 2) > touchpos.x
           && buttonpos.y - (buttonsize.y/2) < touchpos.y
           && buttonpos.y + (buttonsize.y/2) > touchpos.y)
            {
                thisImage.color = new Color(1, 1, 1, 100f / 255f);
                if (touch.phase == TouchPhase.Ended)
                {
                    switch (touchResult)
                    {
                        case TouchResult.errorResult:
                            Debug.Log("Error");
                            break;
                        case TouchResult.clearResult:
                            Debug.Log("Clear");
                            break;
                    }
                }

            }
            else
            {
                if (touch.phase == TouchPhase.Ended)
                {
                    Debug.Log("OutOfError");
                }
            }
        }
    }
}
