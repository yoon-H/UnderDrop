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
    private const float LongSpawnLocDx = 2f;

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
        if (res >-1)
        {
            SpawnNormalObstacle(dir, timer, timeForArrival, locY);
        }
        else
        {
            //SpawnCardObstacle(dir, timer, timeForArrival, locY);
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
}
