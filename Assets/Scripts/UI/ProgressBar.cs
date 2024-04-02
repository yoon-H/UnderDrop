using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{

    public Int32 Maxvalue = 30;
    public Int32 Minvalue = 0;
    public Int32 Value;

    public Image Fill;
    private float MaxWidth;


    // Start is called before the first frame update
    void Start()
    {
        Value = Maxvalue;
        MaxWidth = Fill.transform.localScale.x;
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
}
