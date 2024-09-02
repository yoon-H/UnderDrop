using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape)) 
        {
            if (GameManager.Instance.CurrentScene == E_Scene.MainScene)
            {
                Application.Quit();
            }
        }
        
    }

    public void MoveToInGameScene()
    {
        SceneManager.LoadScene("InGameScene");
        GameManager.Instance.StopMusic();
        GameManager.Instance.PlayMusic("ingamebgm");
        GameManager.Instance.CurrentScene = E_Scene.InGameScene;
    }

    public void MoveToMainScene()
    {
        SceneManager.LoadScene("MainScene");
        GameManager.Instance.StopMusic();
        GameManager.Instance.PlayMusic("mainbgm");
        GameManager.Instance.CurrentScene = E_Scene.MainScene;
    }
}
