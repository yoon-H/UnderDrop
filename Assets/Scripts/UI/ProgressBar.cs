using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{

    public int Maxvalue = 30;
    public int Minvalue = 0;
    public int Value;
    public GameObject Bar;

    public Image Fill;
    private float MaxWidth;

    public int InActiveOpacity = 40;
    public int ActiveOpacity = 255;


    // Start is called before the first frame update
    void Start()
    {
        Value = Maxvalue;
        MaxWidth = Fill.transform.localScale.x;
        SetTransparency(InActiveOpacity);
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
        Component[] images = GetComponentsInChildren<Image>();

        foreach (var image in images)
        {
            Color color = image.GetComponent<Image>().color;
            color.a = (float) value / 255f;
            image.GetComponent<Image>().color = color;
        }

    }
}
