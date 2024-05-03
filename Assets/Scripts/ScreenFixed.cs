using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFixed : MonoBehaviour
{
    public int Width = 1440;
    public int Height = 2560;

    void Awake()
    {
        SetResolution();
    }

    public void SetResolution()
    {
        int deviceWidth = Screen.width;
        int deviceHeight = Screen.height;

        float gameAspectRatio = (float)Width / Height;
        float screenAspectRatio = (float)deviceWidth / deviceHeight;

        if (gameAspectRatio < screenAspectRatio) // device ratio is bigger
        {
            float newWidth = gameAspectRatio / screenAspectRatio;
            Camera.main.rect = new Rect((1f - newWidth) / 2f, 0f, newWidth, 1f);
        }
        else if(gameAspectRatio > screenAspectRatio)// game ratio is bigger
        {
            float newHeight = screenAspectRatio / gameAspectRatio;
            Camera.main.rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight);
        }
    }
}
