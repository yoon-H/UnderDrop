using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaselTeam : TeamRegion
{
    [Header("Obstacle")]
    public GameObject BambooObstacleRef;
    public GameObject DartObstacleRef;

    private const float BambooSpawnLocDx = 1.4f;
    //private const float DartSpawnLocDx = 1.4f;

    private GameObject Obstacle;


    //[Header("Monster")]
    //public GameObject LowHpMonsterRef;
    //public GameObject MiddleHpMonsterRef;
    //public GameObject HighHpMonsterRef;

    //private const float MonsterSpawnLocDx = 1.4f;

    //private const int LowHp = 50;
    //private const int MiddleHp = 70;
    //private const int HighHp = 100;

    //private GameObject Monster;

    public override GameObject SpawnObstacle(E_Direction dir, Timer timer, float timeForArrival, float locY)
    {
        System.Random rand = new System.Random();
        int res = rand.Next(100);
        if (res <= 69)
        {
            SpawnBambooObstacle(dir, timer, timeForArrival, locY);
        }
        else
        {
            SpawnDartObstacle(dir, timer, timeForArrival, locY);

        }

        return Obstacle;
    }

    public override void SpawnMonster(E_Direction dir, GameObject player, GameObject spawner, Timer timer, float timeForArrival, float locY)
    {
        //    System.Random rand = new System.Random();
        //    int res = rand.Next(100);
        //    if (res <= 39)
        //    {
        //        SpawnLowHpMonster(dir, player, spawner, timer, timeForArrival, locY);
        //    }
        //    else if (res <= 79)
        //    {
        //        SpawnMiddleHpMonster(dir, player, spawner, timer, timeForArrival, locY);
        //    }
        //    else
        //    {
        //        SpawnHighHpMonster(dir, player, spawner, timer, timeForArrival, locY);

        //    }
    }


    //Obstacle
    private void SpawnBambooObstacle(E_Direction dir, Timer timer, float timeForArrival, float locY)
    {
        //Set LocX
        float locX;
        if (E_Direction.Left == dir) { locX = - BambooSpawnLocDx; }
        else locX = BambooSpawnLocDx;

        //Spawn Obstacle
        Obstacle = Instantiate(BambooObstacleRef);                                 //TODO : change to ObjectPool
        if (!Obstacle) return;
        ObjectMovement obj = Obstacle.GetComponent<ObjectMovement>();

        //Set Speed variable
        if (!obj) return;
        obj.SetTimeForArrival(timeForArrival);

        //Set Timer
        Obstacle obs = Obstacle.GetComponent<Obstacle>();
        if (!obs) return;
        obs.InitializeObstacleStats(timer);

        //Set Location
        Obstacle.transform.position = new Vector3(locX, locY, 0);
    }

    private void SpawnDartObstacle(E_Direction dir, Timer timer, float timeForArrival, float locY)
    {
        //Set LocX
        float locX;
        if (E_Direction.Left == dir) { locX = - BambooSpawnLocDx; }
        else locX = BambooSpawnLocDx;

        //Spawn Obstacle
        Obstacle = Instantiate(DartObstacleRef);                                 //TODO : change to ObjectPool
        if (!Obstacle) return;
        ObjectMovement obj = Obstacle.GetComponent<ObjectMovement>();

        //Set Speed variable
        if (!obj) return;
        obj.SetTimeForArrival(timeForArrival);

        //Set Timer
        DartObstacle dart = Obstacle.GetComponent<DartObstacle>();
        if (!dart) return;
        dart.InitializeObstacleStats(timer);
        dart.SetDirection(dir);

        //Set Location
        Obstacle.transform.position = new Vector3(locX, locY, 0);
    }
}
