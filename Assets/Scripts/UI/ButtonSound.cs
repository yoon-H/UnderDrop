using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    public E_ButtonType buttonType;

    public void PlayButtonSound()
    {
        switch (buttonType)
        {
            case E_ButtonType.Default:
                GameManager.Instance.PlaySound("normalbuttonsound");
                break;
            case E_ButtonType.Switch:
                GameManager.Instance.PlaySound("switchbuttonsound");
                break;
        }
    }
}
