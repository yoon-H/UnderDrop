using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject ObstaclePrefab;

    GameObject Obstacle;

    private float SpawnLocDx = 1.8f;

    public float MaxTimeForArrival = 5f;
    private float CurTimeForArrival;
    public float MinTimeForArrival = 3.6f;
    public float TimeForArrivalReducingAmount = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        CurTimeForArrival = MaxTimeForArrival;
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
            ObastacleMovement obs = Obstacle.GetComponent<ObastacleMovement>();
            if (!obs) return;
            obs.SetRotateDirection(E_Direction.Left);
            obs.SetTimeForArrival(CurTimeForArrival);

            Obstacle.transform.position = new Vector3(-SpawnLocDx, transform.position.y, 0);
        }
        else if (res == 1) // spawn right
        {
            Obstacle = Instantiate(ObstaclePrefab);
            if (!Obstacle) return;
            ObastacleMovement obs = Obstacle.GetComponent<ObastacleMovement>();
            if (!obs) return;
            obs.SetRotateDirection(E_Direction.Right);
            obs.SetTimeForArrival(CurTimeForArrival);

            Obstacle.transform.position = new Vector3(SpawnLocDx, transform.position.y, 0);
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
