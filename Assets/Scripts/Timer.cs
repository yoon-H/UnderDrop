using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Timer : MonoBehaviour
{
    public GameObject ObstacleSpawnerRef;
    private ObstacleSpawner ObstacleSpawner;
    public GameObject MonsterSpawnerRef;
    private MonsterSpawner MonsterSpawner;
    public Text ScoreText;

    private float ObstacleSpawnCounter = 0f;
    private float MonsterSpawnCounter = 0f;
    public float ScoreCounter = 0f;

    public float ObstacleSpawnTime = 1f;
    public float MonsterSpawnTime = 4f;

    //private bool IsPaused = false;

    public GameObject GameOverPanel;
    private SwitchPopUp GameOverPopUp;
    private ScoreBoard ScoreBoard;

    public int Score = 0;

    

    // Start is called before the first frame update
    void Start()
    {
        ObstacleSpawner = ObstacleSpawnerRef.GetComponent<ObstacleSpawner>();
        MonsterSpawner = MonsterSpawnerRef.GetComponent<MonsterSpawner>();

        GameOverPopUp = GameOverPanel.GetComponent<SwitchPopUp>();
        ScoreBoard = GameOverPanel.GetComponentInChildren<ScoreBoard>();

        SetIsPaused(false);
    }

    // Update is called once per frame
    void Update()
    {
        ObstacleSpawnCounter += Time.deltaTime;
        MonsterSpawnCounter += Time.deltaTime;
        ScoreCounter += Time.deltaTime;

        if(ScoreText) 
        {
            Score = (int)(ScoreCounter / 0.2);
            ScoreText.text = Score + "m";
        }


        if (!ObstacleSpawner) return;

        if (ObstacleSpawnCounter >= ObstacleSpawnTime)
        {
            ObstacleSpawnCounter -= ObstacleSpawnTime;
            ObstacleSpawner.SpawnObstacle();
        }

        if (!MonsterSpawner) return;

        if (MonsterSpawnCounter >= MonsterSpawnTime)
        {
            MonsterSpawnCounter -= MonsterSpawnTime;
            MonsterSpawner.SpawnMonster();
        }

    }

    public void SetIsPaused(bool flag)
    {
        if(flag)
        {
            //IsPaused = true;
            Time.timeScale = 0;
        }
        else
        {
            //IsPaused = false;
            Time.timeScale = 1;
        }
    }

    public void GameOver()
    {
        
        //SetScoreText
        int bestscore = GameManager.Instance.BestScore;
        if(Score > bestscore)
        {
            GameManager.Instance.BestScore = Score;
        }
        ScoreBoard.SetText(Score);

        //Show PopUp
        GameOverPopUp.SwitchFlag(true);

        //Stop Game
        SetIsPaused(true);

    }
}
