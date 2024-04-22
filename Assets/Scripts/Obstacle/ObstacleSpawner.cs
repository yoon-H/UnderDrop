using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject ObstaclePrefab;


    public GameObject TimerRef;
    private Timer Timer;

    GameObject Obstacle;

    private float ToothSpawnLocDx = 1.8f;
    private float ButtonTypeSpawnLocDx = 1.5f;

    public float MaxTimeForArrival = 5f;
    private float CurTimeForArrival;
    public float MinTimeForArrival = 3.6f;
    public float TimeForArrivalReducingAmount = 0.2f;

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

    public void SpawnObstacle()
    {
        System.Random rand = new System.Random();
        int res = rand.Next(0, 3);
        if (res == 0) // spawn left
        {
            Obstacle = Instantiate(ObstaclePrefab);                                 //TODO : change to ObjectPool
            if (!Obstacle) return;
            ObjectMovement obj = Obstacle.GetComponent<ObjectMovement>();
            ButtonTypeObstacle bto = Obstacle.GetComponent<ButtonTypeObstacle>();
            if (!obj) return;
            if (!bto) return;

            obj.SetTimeForArrival(CurTimeForArrival);
            
            Obstacle.transform.position = new Vector3(0, transform.position.y, 0);
            if (!bto.LaserButtonRef) return;
            bto.LaserButtonRef.gameObject.transform.position = new Vector3(-ButtonTypeSpawnLocDx, transform.position.y, 0);
            if(!bto.LaserObstacleRef) return;
            Obstacle obs = bto.LaserObstacleRef.GetComponent<Obstacle>();
            if (!obs) return;
            obs.InitializeObstacleStats(Timer);

            /*
            Obstacle = Instantiate(ObstaclePrefab);                                 //TODO : change to ObjectPool
            if (!Obstacle) return;
            ObjectMovement obj = Obstacle.GetComponent<ObjectMovement>();
            SawTooth tooth = Obstacle.GetComponent<SawTooth>();
            if (!obj) return;
            if(!tooth) return;
            obj.SetTimeForArrival(CurTimeForArrival);
            tooth.InitializeObstacleStats(Timer);
            tooth.SetRotateDirection(E_Direction.Left);

            Obstacle.transform.position = new Vector3(-SpawnLocDx, transform.position.y, 0);
            */
        }
        else if (res == 1) // spawn right
        {
            Obstacle = Instantiate(ObstaclePrefab);                                 //TODO : change to ObjectPool
            if (!Obstacle) return;
            ObjectMovement obj = Obstacle.GetComponent<ObjectMovement>();
            ButtonTypeObstacle bto = Obstacle.GetComponent<ButtonTypeObstacle>();
            if (!obj) return;
            if (!bto) return;

            obj.SetTimeForArrival(CurTimeForArrival);
            
            Obstacle.transform.position = new Vector3(0, transform.position.y, 0);
            if (!bto.LaserButtonRef) return;
            bto.LaserButtonRef.gameObject.transform.position = new Vector3(ButtonTypeSpawnLocDx, transform.position.y, 0);
            if (!bto.LaserObstacleRef) return;
            Obstacle obs = bto.LaserObstacleRef.GetComponent<Obstacle>();
            if (!obs) return;
            obs.InitializeObstacleStats(Timer);
            
            /*
            Obstacle = Instantiate(ObstaclePrefab);
            if (!Obstacle) return;
            ObjectMovement obj = Obstacle.GetComponent<ObjectMovement>();
            SawTooth tooth = Obstacle.GetComponent<SawTooth>();
            if (!obj) return;
            if (!tooth) return;
            obj.SetTimeForArrival(CurTimeForArrival);
            tooth.InitializeObstacleStats(Timer);
            tooth.SetRotateDirection(E_Direction.Right);

            Obstacle.transform.position = new Vector3(SpawnLocDx, transform.position.y, 0);
            */
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
