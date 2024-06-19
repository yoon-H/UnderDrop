using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMusic : MonoBehaviour
{
    public void Pause()
    {
        GameManager.Instance.PauseMusic();
    }

    public void OnPause()
    {
        GameManager.Instance.ResumeMusic();
    }
}
