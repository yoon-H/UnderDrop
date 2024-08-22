using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedButton : MonoBehaviour
{
    // Start is called before the first frame update
    private int ButtonIndex = 0;

    public Image[] ButtonImages;
    public Sprite[] OnImages;
    public Sprite[] OffImages;
    


    public CollectionPanel Panel;

    private void OnEnable()
    {
        if (ButtonImages[ButtonIndex] != null)
        {
            ButtonImages[ButtonIndex].sprite = OnImages[ButtonIndex];
        }
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
        ButtonImages[ButtonIndex].sprite = OffImages[ButtonIndex];

        ButtonIndex = index;
        ButtonImages[ButtonIndex].sprite = OnImages[ButtonIndex];

        Panel.SetPanelIndex(ButtonIndex);
    }
}
