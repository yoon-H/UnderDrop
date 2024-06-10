using JetBrains.Annotations;
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
    public Text[] ScoreTexts;

    //BackGround
    public GameObject BackGroundRef;
    public BackGroundMovement BackGround;

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
    public float RaidSpawnTime = 30f;
    private float RaidSpawnCounter = 0f;
    public float RaidRemainTime = 15f;
    private float RaidRemainCounter = 0f;

    private int RaidMissedCount = 0;

    private bool IsRaidExisted = false;

    public RaidEvent RaidEvent;


    //private bool IsPaused = false;

    public GameObject GameOverPanel;
    private SwitchPopUp GameOverPopUp;
    private ScoreBoard ScoreBoard;

    public float WaitingTime = 5f;

    public int Score = 0;

    //TeamRegion

    public NormalTeam NormalTeam;
    public WeaselTeam WeaselTeam;

    public E_Team[] RaidTeams = new E_Team[2] { E_Team.SID, E_Team.SID};
    public E_Team curRaidTeam;

    SpawnCharacter SpawnCharacter;

    // Start is called before the first frame update
    void Start()
    {
        SpawnCharacter = GetComponent<SpawnCharacter>();

        ObstacleSpawner = ObstacleSpawnerRef.GetComponent<ObstacleSpawner>();
        MonsterSpawner = MonsterSpawnerRef.GetComponent<MonsterSpawner>();
        BackGround = BackGroundRef.GetComponentInChildren<BackGroundMovement>();

        GameOverPopUp = GameOverPanel.GetComponent<SwitchPopUp>();
        ScoreBoard = GameOverPanel.GetComponentInChildren<ScoreBoard>();

        SetIsPaused(false);

        CurMonsterSpawnTime = MaxMonsterSpawnTime;
        CurObstacleSpawnTime = MaxObstacleSpawnTime;

        CurSpeedPeriod = FixedSpeedPeriod;
        CurSpawnPeriod = FixedSpawnPeriod;

        //RaidEvent
        RaidEvent.SetTimer(this);

        //Set Raid Teams in this game
        SetRaidTeams();
    }

    // Update is called once per frame
    void Update()
    {
        ObstacleSpawnCounter += Time.deltaTime;
        ScoreCounter += Time.deltaTime;

        //Raid Counter
        if (IsRaidExisted)
        {
            RaidRemainCounter += Time.deltaTime;
            MonsterSpawnCounter += Time.deltaTime;
            RaidEvent.Bar.Value = RaidRemainTime - RaidRemainCounter;

            if (RaidRemainCounter >= RaidRemainTime)
            {
                IsRaidExisted = false;
                RaidRemainCounter = 0;
                RaidEvent.RaidBar.SetActive(false);
                RaidEvent.RaidMarkRef.SetActive(false);
            }
        }
        else
        {
            RaidSpawnCounter += Time.deltaTime;
            if (RaidSpawnCounter >= RaidSpawnTime)
            {
                if (RaidMissedCount >= 2)
                {
                    System.Random rand = new System.Random();
                    int res = rand.Next(2);

                    //Set current RaidTeam
                    //curRaidTeam = RaidTeams[res];
                    curRaidTeam = E_Team.Weasel;


                    StartCoroutine(RaidEvent.IE_Warning(curRaidTeam));
                    RaidMissedCount = 0;
                }
                else
                {
                    System.Random rand = new System.Random();
                    int res = rand.Next(3);

                    if (res == 2)
                    {
                        RaidMissedCount++;
                    }
                    else
                    {
                        //Set current RaidTeam
                        //curRaidTeam = RaidTeams[res];
                        curRaidTeam = E_Team.Weasel;

                        StartCoroutine(RaidEvent.IE_Warning(curRaidTeam));
                    }
                }


                RaidSpawnCounter = 0;
            }
        }



        if (ScoreTexts != null)
        {
            Score = (int)(ScoreCounter / 0.2f);

            foreach (var text in ScoreTexts)
            {
                text.text = Score + "m";
            }
        }

        if (Score >= CurSpeedPeriod)
        {
            CurSpeedPeriod += FixedSpeedPeriod;

            ObstacleSpawner.ReduceTimeForArrival();
            MonsterSpawner.ReduceTimeForArrival();
            BackGround.ReduceTimeForArrival();
        }

        if (Score >= CurSpawnPeriod)
        {
            CurSpawnPeriod += FixedSpawnPeriod;
            if (CurObstacleSpawnTime > MinObstacleSpawnTime)
            {
                CurObstacleSpawnTime -= ObstacleSpawnTimeReducingAmount;
            }

            if (CurMonsterSpawnTime > MinMonsterSpawnTime)
            {
                CurMonsterSpawnTime -= MonsterSpawnTimeReducingAmount;
            }

        }

        if (RaidRemainCounter >= RaidRemainTime)
        {
            IsRaidExisted = false;
        }


        if (!ObstacleSpawner) return;

        if (ObstacleSpawnCounter >= CurObstacleSpawnTime)
        {
            ObstacleSpawnCounter -= CurObstacleSpawnTime;
            ObstacleSpawner.SpawnObstacle(IsRaidExisted, curRaidTeam);
        }

        if (!MonsterSpawner) return;

        if (IsRaidExisted)
        {
            if (MonsterSpawnCounter >= CurMonsterSpawnTime)
            {
                MonsterSpawnCounter -= CurMonsterSpawnTime;
                MonsterSpawner.SpawnMonster(curRaidTeam);
            }
        }
    }

    public void SpawnMonster()
    {
        MonsterSpawner.SpawnMonster(curRaidTeam, 2);
    }

    public void SetIsPaused(bool flag)
    {
        if (flag)
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
        //Play Dead Anim
        PCAnimation animRef = SpawnCharacter.Player.GetComponent<PCAnimation>();

        if (animRef != null)
        {
            animRef.PlayDeadAnim();
        }

        //Stop Game
        SetIsPaused(true);

    }


    public void EndTask()
    {
        int bestscore = GameManager.Instance.BestScore;
        if (Score > bestscore)
        {
            GameManager.Instance.BestScore = Score;
        }
        ScoreBoard.SetText(Score);

        //Show PopUp
        GameOverPopUp.SwitchFlag(true);
    }

    public void SetIsRaidExisted(bool flag)
    {
        IsRaidExisted = flag;
    }

    private void SetRaidTeams()
    {
        E_Team team = GameManager.Instance.team;

        switch (team)
        {
            case E_Team.SID:
                RaidTeams[0] = E_Team.Weasel; RaidTeams[1] = E_Team.Twilight; break;
            case E_Team.Weasel:
                RaidTeams[0] = E_Team.SID; RaidTeams[1] = E_Team.Twilight; break;
            case E_Team.Twilight:
                RaidTeams[0] = E_Team.SID; RaidTeams[1] = E_Team.Weasel; break;
        }
    }
}
