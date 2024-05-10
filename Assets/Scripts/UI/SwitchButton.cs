using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

public class SwitchButton : MonoBehaviour
{
    protected bool Flag = true;

    public Sprite OnImage;
    public Sprite OffImage;

    protected Image Image;
    

    // Start is called before the first frame update
    void Start()
    {
        Image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void SwitchFlag()
    {
        if (!Image) return;
        if (Flag)
        { 
            Flag = false;
            Image.sprite = OffImage;
        }
        else 
        { 
            Flag = true;
            Image.sprite = OnImage;
        }
    }
}
