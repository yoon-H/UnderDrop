using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Timer : MonoBehaviour
{
    public GameObject ObstacleSpawnerRef;
    public ObstacleSpawner ObstacleSpawner;
    public GameObject MonsterSpawnerRef;
    private MonsterSpawner MonsterSpawner;
    public Text ScoreText;

    private float ObstacleSpawnCounter = 0f;
    private float MonsterSpawnCounter = 0f;
    public float ScoreCounter = 0f;

    public float MaxObstacleSpawnTime = 3f;
    private float CurObstacleSpawnTime;
    public float MinObstacleSpawnTime = 2f;
    public float ObstacleSpawnTimeReducingAmount = 0.2f;

    public float MaxMonsterSpawnTime = 4f;
    private float CurMonsterSpawnTime;
    public float MinMonsterSpawnTime = 3f;
    public float MonsterSpawnTimeReducingAmount = 0.2f;

    public int FixedSpeedPeriod = 100;
    private int CurSpeedPeriod;


    private int CurSpawnPeriod;
    public int FixedSpawnPeriod = 300;

    //Raid Event
    public float RaidSpawnTime = 20f;
    private float RaidSpawnCounter = 0f;
    public float RaidRemainTime = 15f;
    private float RaidRemainCounter = 0f;

    private bool IsRaidExisted = false;

    public RaidEvent RaidEvent;


    //private bool IsPaused = false;

    public GameObject GameOverPanel;
    private SwitchPopUp GameOverPopUp;
    private ScoreBoard ScoreBoard;

    public int Score = 0;

    //TeamRegion

    public NormalTeam NormalTeam;
    public WeaselTeam WeaselTeam;



    // Start is called before the first frame update
    void Start()
    {
        ObstacleSpawner = ObstacleSpawnerRef.GetComponent<ObstacleSpawner>();
        MonsterSpawner = MonsterSpawnerRef.GetComponent<MonsterSpawner>();

        GameOverPopUp = GameOverPanel.GetComponent<SwitchPopUp>();
        ScoreBoard = GameOverPanel.GetComponentInChildren<ScoreBoard>();

        SetIsPaused(false);

        CurMonsterSpawnTime = MaxMonsterSpawnTime;
        CurObstacleSpawnTime = MaxObstacleSpawnTime;

        CurSpeedPeriod = FixedSpeedPeriod;
        CurSpawnPeriod = FixedSpawnPeriod;

        //RaidEvent
        RaidEvent.SetTimer(this);
    }

    // Update is called once per frame
    void Update()
    {
        ObstacleSpawnCounter += Time.deltaTime;
        MonsterSpawnCounter += Time.deltaTime;
        ScoreCounter += Time.deltaTime;

        //Raid Counter
        if(IsRaidExisted)
        {
            RaidRemainCounter += Time.deltaTime;
            RaidEvent.Bar.Value = RaidRemainTime - RaidRemainCounter;
            if(RaidRemainCounter >= RaidRemainTime)
            {
                IsRaidExisted = false;
                RaidRemainCounter = 0;
                RaidEvent.RaidBar.SetActive(false);
            }
        }
        else
        {
            RaidSpawnCounter += Time.deltaTime;
            if (RaidSpawnCounter >= RaidSpawnTime)
            {
                StartCoroutine(RaidEvent.IE_Warning());
                
                RaidSpawnCounter = 0;
            }
        }
        


        if(ScoreText) 
        {
            Score = (int)(ScoreCounter / 0.2f);
            ScoreText.text = Score + "m";
        }

        if(Score >= CurSpeedPeriod)
        {
            CurSpeedPeriod += FixedSpeedPeriod;

            ObstacleSpawner.ReduceTimeForArrival();
            MonsterSpawner.ReduceTimeForArrival();
        }

        if(Score >= CurSpawnPeriod)
        {
            CurSpawnPeriod += FixedSpawnPeriod;
            if(CurObstacleSpawnTime > MinObstacleSpawnTime)
            {
                CurObstacleSpawnTime -= ObstacleSpawnTimeReducingAmount;
            }
            
            if(CurMonsterSpawnTime > MinMonsterSpawnTime)
            {
                CurMonsterSpawnTime -= MonsterSpawnTimeReducingAmount;
            }
            
        }

        if(RaidRemainCounter >= RaidRemainTime)
        {
            IsRaidExisted = false;
        }


        if (!ObstacleSpawner) return;

        if (ObstacleSpawnCounter >= CurObstacleSpawnTime)
        {
            ObstacleSpawnCounter -= CurObstacleSpawnTime;
            ObstacleSpawner.SpawnObstacle(IsRaidExisted);
        }

        if (!MonsterSpawner) return;

        if (MonsterSpawnCounter >= CurMonsterSpawnTime)
        {
            MonsterSpawnCounter -= CurMonsterSpawnTime;
            MonsterSpawner.SpawnMonster(IsRaidExisted);
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

    public void SetIsRaidExisted(bool flag)
    {
        IsRaidExisted = flag;
    }
}
