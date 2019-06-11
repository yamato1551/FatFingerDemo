using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI3DtouchZoom : MonoBehaviour
{
    private GridLayoutGroup content;
    private Transform wrapper;
    private RectTransform contentRect;
    private float scale = 1;
    public float min, max;
    [System.Serializable]
    struct RangeClass
    {
        public float min, max;
    }
    [SerializeField]
    private RangeClass RangeScale;
    [SerializeField]
    private RangeClass RangeLimitedScale;
     float ConvergenceTime;
    bool isPitch;
    void Start()
    {
        contentRect = content.GetComponent<RectTransform>();
    }
    void Update()
    {
        ConvergenceTime = Input.touches[0].pressure;
    }
    void EditorControl()
    {
        if (isPitch)
        {
            if (Input.touchCount > 0)
            {
                if()
            }
        }
    }
}
