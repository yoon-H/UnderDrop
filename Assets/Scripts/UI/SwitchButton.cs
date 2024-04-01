using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchButton : MonoBehaviour
{
    private bool Flag = true;

    public Sprite OnImage;
    public Sprite OffImage;

    private Image Image;

    // Start is called before the first frame update
    void Start()
    {
        Image = gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchFlag()
    {
        if (Flag)
        { 
            Flag = false;
            Image.sprite = OffImage;
            SwitchSetting();
            print("False");
        }
        else 
        { 
            Flag = true;
            Image.sprite = OnImage;
            SwitchSetting();
            print("True");
        }
    }

    protected virtual void SwitchSetting()
    {

    }
}
