using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject TimerRef;
    private Timer Timer;

    GameObject Obstacle;

    public float MaxTimeForArrival = 3f;
    public float CurTimeForArrival;
    public float MinTimeForArrival = 2.4f;
    public float TimeForArrivalReducingAmount = 0.2f;

    public float AddTimeAmountWhenRaid = 0.5f; 

    // Start is called before the first frame update
    void Start()
    {
        CurTimeForArrival = MaxTimeForArrival;
        
        // Timer Initializing
        Timer = TimerRef.GetComponent<Timer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnObstacle(bool isRaid, E_Team team)
    {
        System.Random rand = new System.Random();
        int res = rand.Next(0, 3);
        if (res == 0) // spawn left
        {
            if(isRaid)
            {
                if (!Timer) return;
                switch (team)
                {
                    case E_Team.SID:
                        break;
                    case E_Team.Weasel:
                        Obstacle = Timer.WeaselTeam.SpawnObstacle(E_Direction.Left, Timer, CurTimeForArrival + AddTimeAmountWhenRaid, transform.position.y);
                        break;
                    case E_Team.Twilight:
                        break;
                }
            }
            else
            {
                Obstacle = Timer.NormalTeam.SpawnObstacle(E_Direction.Left, Timer, CurTimeForArrival, transform.position.y);
            }
            
        }
        else if (res == 1) // spawn right
        {
            if (isRaid)
            {
                if (!Timer) return;
                switch (team)
                {
                    case E_Team.SID:
                        break;
                    case E_Team.Weasel:
                        Obstacle = Timer.WeaselTeam.SpawnObstacle(E_Direction.Right, Timer, CurTimeForArrival + AddTimeAmountWhenRaid, transform.position.y);
                        break;
                    case E_Team.Twilight:
                        break;
                }
            }
            else
            {
                Obstacle = Timer.NormalTeam.SpawnObstacle(E_Direction.Right, Timer, CurTimeForArrival, transform.position.y);
            }
        }

        Destroy(Obstacle, 8f);
    }

    public void ReduceTimeForArrival()
    {
        if (CurTimeForArrival > MinTimeForArrival)
        {
            CurTimeForArrival -= TimeForArrivalReducingAmount;
        }
    }
}
