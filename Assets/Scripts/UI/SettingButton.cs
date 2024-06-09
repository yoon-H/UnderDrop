using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

public class SettingButton : SwitchButton
{
    public E_SettingType Type;

    // Start is called before the first frame update
    void Start()
    {
        Image = GetComponent<Image>();

        InitSettingImage();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void SwitchFlag()
    {
        if (!Image) return;
        if (Flag)
        {
            Flag = false;
            Image.sprite = OffImage;
            SwitchSetting(false);
        }
        else
        {
            Flag = true;
            Image.sprite = OnImage;
            SwitchSetting(true);
        }
    }

    private void SwitchSetting(bool flag)
    {
        switch (Type)
        {
            case E_SettingType.Music:
                GameManager.Instance.SwitchMusic(flag);
                break;
            case E_SettingType.Sound:
                GameManager.Instance.SwitchSound(flag);
                break;
            case E_SettingType.Vibration:
                GameManager.Instance.OnVib = flag;
                break;
        }
    }

    private void InitSettingImage()
    {
        bool flag = true;
        switch (Type)
        {
            case E_SettingType.Music:
                flag = GameManager.Instance.OnMusic;
                break;
            case E_SettingType.Sound:
                flag = GameManager.Instance.OnSound;
                break;
            case E_SettingType.Vibration:
                flag = GameManager.Instance.OnVib;
                break;
        }

        Flag = flag;

        if (flag)
        {
            Image.sprite = OnImage;
        }
        else
        {
            Image.sprite = OffImage;
        }
    }
}
