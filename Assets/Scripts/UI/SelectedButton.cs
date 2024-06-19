using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedButton : MonoBehaviour
{
    // Start is called before the first frame update
    public Button[] Buttons;
    private int ButtonIndex = 0;
    private void OnEnable()
    {
        if (Buttons[ButtonIndex] != null)
            Buttons[ButtonIndex].Select();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetIndex(int index)
    {
        ButtonIndex = index;
    }
}
