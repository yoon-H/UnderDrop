using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaselTeam : TeamRegion
{
    [Header("Obstacle")]
    public GameObject BambooObstacleRef;
    public GameObject DartObstacleRef;

    private const float BambooSpawnLocDx = 1.55f;
    //private const float DartSpawnLocDx = 1.4f;

    private GameObject Obstacle;


    [Header("Monster")]
    public GameObject NormalMonsterRef;
    public GameObject ObstacleMonsterRef;

    private const float MonsterSpawnLocDx = 1.55f;

    private const int NormalHp = 60;
    private const int HighHp = 110;

    private GameObject Monster;

    private bool IsObstaclMonsterSpawned = false;

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
        System.Random rand = new System.Random();
        int res = rand.Next(100);
        if(IsObstaclMonsterSpawned)
        {
            SpawnNormalMonster(dir, player, spawner, timer, timeForArrival, locY);
        }
        else
        {
            if (res <= 64)
            {
                SpawnNormalMonster(dir, player, spawner, timer, timeForArrival, locY);
            }
            else
            {
                SpawnObstacleMonster(dir, player, spawner, timer, timeForArrival, locY);
            }
        }
        
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

    // Monster
    private void SpawnNormalMonster(E_Direction dir, GameObject player, GameObject spawner, Timer timer, float timeForArrival, float locY)
    {
        //Set LocX
        float locX;
        if (E_Direction.Left == dir) { locX = -MonsterSpawnLocDx; }
        else locX = MonsterSpawnLocDx;

        //Spawn Moster
        Monster = Instantiate(NormalMonsterRef);                                 //TODO : change to ObjectPool


        if (!Monster) return;
        MonsterMovement movement = Monster.GetComponent<MonsterMovement>();
        Monster mon = Monster.GetComponent<Monster>();

        //Set Movement variables
        if (!movement) return;
        movement.SetMonsterMovementInfo(player, timeForArrival);

        if (!mon) return;
        mon.SetMonsterInfo(dir, spawner, NormalHp, timer);

        //Set location
        Monster.transform.position = new Vector3(locX, locY, 0);
    }

    private void SpawnObstacleMonster(E_Direction dir, GameObject player, GameObject spawner, Timer timer, float timeForArrival, float locY)
    {
        IsObstaclMonsterSpawned = true;
        //Set LocX
        float locX;
        if (E_Direction.Left == dir) { locX = -MonsterSpawnLocDx; }
        else locX = MonsterSpawnLocDx;

        //Spawn Moster
        Monster = Instantiate(ObstacleMonsterRef);                                 //TODO : change to ObjectPool


        if (!Monster) return;
        MonsterMovement movement = Monster.GetComponent<MonsterMovement>();
        WeaselObstacleMonster wMon = Monster.GetComponent<WeaselObstacleMonster>();

        //Set Movement variables
        if (!movement) return;
        movement.SetMonsterMovementInfo(player, timeForArrival);


        if (!wMon) return;
        wMon.SetMonsterInfo(dir, spawner, HighHp, timer);
        wMon.SetTeam(this);

        //Set location
        Monster.transform.position = new Vector3(locX, locY, 0);
    }

    public void SetIsObstacleMonsterSpawned(bool flag)
    {
        IsObstaclMonsterSpawned = flag;
    }
}
