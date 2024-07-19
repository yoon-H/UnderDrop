using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SIDTeam : TeamRegion
{
    [Header("Obstacle")]
    public GameObject NormalObstacleRef;
    public GameObject LongObstacleRef;
    public GameObject BombObstacleRef;

    private const float NormalSpawnLocDx = 1.5f;
    private const float LongSpawnLocDx = 1.6f;

    private GameObject Obstacle;


    [Header("Monster")]
    public GameObject NormalMonsterRef;
    public GameObject LazerMonsterRef;

    private const float MonsterSpawnLocDx = 1.55f;

    private const int Hp = 80;

    private GameObject Monster;

    public override GameObject SpawnObstacle(E_Direction dir, Timer timer, float timeForArrival, float locY)
    {
        System.Random rand = new System.Random();
        int res = rand.Next(100);
        if (res <-1)
        {
            SpawnNormalObstacle(dir, timer, timeForArrival, locY);
        }
        else
        {
            SpawnButtonTypeObstacle(dir, timer, timeForArrival, locY);
        }

        return Obstacle;
    }

    public override void SpawnMonster(E_Direction dir, GameObject player, GameObject spawner, Timer timer, float timeForArrival, float locY)
    {
        System.Random rand = new System.Random();
        int res = rand.Next(100);
        if (HasSpecialMonsterSpawned)
        {
            //SpawnNormalMonster(dir, player, spawner, timer, timeForArrival, locY);
        }
        else
        {
            if (res < 0)
            {
                //SpawnNormalMonster(dir, player, spawner, timer, timeForArrival, locY);
            }
            else
            {
                //SpawnDebuffMonster(dir, player, spawner, timer, timeForArrival, locY);
            }
        }

    }

    //Obstacle
    private void SpawnNormalObstacle(E_Direction dir, Timer timer, float timeForArrival, float locY)
    {
        //Set LocX
        float locX;
        if (E_Direction.Left == dir) { locX = -NormalSpawnLocDx; }
        else locX = NormalSpawnLocDx;

        //Spawn Obstacle
        Obstacle = Instantiate(NormalObstacleRef);                                 //TODO : change to ObjectPool
        if (!Obstacle) return;
        ObjectMovement obj = Obstacle.GetComponent<ObjectMovement>();

        //Set Speed variable
        if (!obj) return;
        obj.SetTimeForArrival(timeForArrival);

        //Set Timer
        Obstacle obs = Obstacle.GetComponent<Obstacle>();
        if (!obs) return;
        obs.InitializeObstacleStats(timer);

        ObjectDirection direction = Obstacle.GetComponent<ObjectDirection>();
        if (direction)
            direction.SetDirection(dir);

        //Set Location
        Obstacle.transform.position = new Vector3(locX, locY, 0);
    }

    private void SpawnButtonTypeObstacle(E_Direction dir, Timer timer, float timeForArrival, float locY)
    {
        //Set LocX
        float locX;
        if (E_Direction.Left == dir) { locX = -LongSpawnLocDx; }
        else locX = LongSpawnLocDx;

        //Set Obstcle direction

        System.Random rand = new System.Random();
        int res = rand.Next(2);

        E_Direction obsDir;

        if (res ==0) { obsDir = E_Direction.Left; }
        else obsDir = E_Direction.Right;

        float obsLocX;
        if (obsDir == E_Direction.Left) { obsLocX = -LongSpawnLocDx; }
        else obsLocX = LongSpawnLocDx;


        //Spawn Obstacle
        Obstacle = Instantiate(LongObstacleRef);                                //TODO : change to ObjectPool

        if (!Obstacle) return;
        ObjectMovement obj = Obstacle.GetComponent<ObjectMovement>();
        ButtonTypeObstacle bto = Obstacle.GetComponent<ButtonTypeObstacle>();

        if (!obj) return;
        if (!bto) return;

        //Set Speed variable
        obj.SetTimeForArrival(timeForArrival);

        //Set Location
        Obstacle.transform.position = new Vector3(0, locY, 0);

        //Set Button Location
        if (!bto.LaserButtonRef) return;
        bto.LaserButtonRef.gameObject.transform.position = new Vector3(locX, locY, 0);

        if (bto.LaserButtonRef.TryGetComponent<ObjectDirection>(out var direction))
        {
            direction.SetDirection(dir);
        }


        if (!bto.LaserObstacleRef) return;
        Obstacle obs = bto.LaserObstacleRef.GetComponent<Obstacle>();

        if (!obs) return;

        if(obs.TryGetComponent<ObjectDirection>(out var obsDirection))
        {
            obsDirection.SetDirection(obsDir);
        }

        Vector3 vec = obs.transform.position;
        vec.x = obsLocX;

        obs.transform.position = vec;

        //Set Timer
        if (!obs) return;
        obs.InitializeObstacleStats(timer);
    }

}
