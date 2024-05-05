using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{

    public float Maxvalue = 30;
    public float Minvalue = 0;
    public float Value;
    public float Width;
    public GameObject Bar;

    public Image Fill;

    [SerializeField]
    private float MaxWidth;

    public int InActiveOpacity = 40;
    public int ActiveOpacity = 255;


    // Start is called before the first frame update
    void Start()
    {
        InitProgressBar();
    }

    // Update is called once per frame
    void Update()
    {
        SetWidth();
    }

    public void SetWidth()
    {
        float percentage = (float) Value / (float) Maxvalue;
        float width = MaxWidth * percentage;
        Width = width;
        if (!Fill) return;
        Fill.transform.localScale = new Vector3(width, Fill.transform.localScale.y, Fill.transform.localScale.z);
    }


    public void SetActiveProgress(bool isActive)
    {
        if(isActive) 
        {
            SetTransparency(ActiveOpacity);
        }
        else
        {
            SetTransparency(InActiveOpacity);
        }
    }

    public void SetTransparency(Int32 value)
    {
        Image[] images = GetComponentsInChildren<Image>();

        foreach (var image in images)
        {
            Color color = image.color;
            if (color == null) return;
            color.a = (float) value / 255f;
            image.GetComponent<Image>().color = color;
        }

    }

    public void InitProgressBar()
    {
        Value = Maxvalue;
        if (!Fill) return;
        MaxWidth = Fill.transform.localScale.x;
        SetTransparency(ActiveOpacity);
    }
}
