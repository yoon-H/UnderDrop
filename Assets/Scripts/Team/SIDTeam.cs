using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class SIDTeam : TeamRegion
{
    [Header("Obstacle")]
    public GameObject NormalObstacleRef;
    public GameObject LongObstacleRef;
    public GameObject BombObstacleRef;

    private const float NormalSpawnLocDx = 1.5f;
    private const float LongSpawnLocDx = 1.6f;
    private const float BombDxAmount = 0.5f;

    private GameObject Obstacle;


    [Header("Monster")]
    public GameObject NormalMonsterRef;
    public GameObject LaserMonsterRef;

    private const float MonsterSpawnLocDx = 1.55f;

    private const int NormalHp = 70;
    private const int BigHp = 70;

    private GameObject Monster;

    public override GameObject SpawnObstacle(E_Direction dir, Timer timer, float timeForArrival, float locY)
    {
        System.Random rand = new System.Random();
        int res = rand.Next(100);
        if (res <0)
        {
            SpawnNormalObstacle(dir, timer, timeForArrival, locY);
        }
        else if (res <0)
        {
            SpawnButtonTypeObstacle(dir, timer, timeForArrival, locY);
        }
        else
        {
            SpawnBombObstacle(dir, timer, timeForArrival, locY);
        }

        return Obstacle;
    }

    public override void SpawnMonster(E_Direction dir, GameObject player, GameObject spawner, Timer timer, float timeForArrival, float locY)
    {
        System.Random rand = new System.Random();
        int res = rand.Next(100);
        if (HasSpecialMonsterSpawned)
        {
            SpawnNormalMonster(dir, player, spawner, timer, timeForArrival, locY);
        }
        else
        {
            if (res <0)
            {
                SpawnNormalMonster(dir, player, spawner, timer, timeForArrival, locY);
            }
            else
            {
                SpawnLaserMonster(dir, player, spawner, timer, timeForArrival, locY);
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

    private void SpawnBombObstacle(E_Direction dir, Timer timer, float timeForArrival, float locY)
    {
        //Set LocX
        float locX;
        float obsLocX;

        if (E_Direction.Left == dir)
        {
            locX = -LongSpawnLocDx;
            obsLocX = locX - BombDxAmount;

        }
        else
        {
            locX = LongSpawnLocDx;
            obsLocX = locX + BombDxAmount;
        }


        //Spawn Obstacle
        Obstacle = Instantiate(BombObstacleRef);                                //TODO : change to ObjectPool

        if (!Obstacle) return;
        ObjectMovement obj = Obstacle.GetComponent<ObjectMovement>();
        BombObstacle bomb = Obstacle.GetComponentInChildren<BombObstacle>();

        if (!obj) return;

        //Set Speed variable
        obj.SetTimeForArrival(timeForArrival);

        //Set Location
        Obstacle.transform.position = new Vector3(locX, locY, 0);

        //Rotate
        ObjectDirection[] directions = Obstacle.GetComponentsInChildren<ObjectDirection>();

        foreach (var item in directions)
        {
            item.SetDirection(dir);
        }

        Vector3 vec = Obstacle.transform.position;
        vec.x = obsLocX;

        Obstacle.transform.position = vec;

        //Set Timer
        if (!bomb) return;
        bomb.InitializeObstacleStats(timer);

        
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

    private void SpawnLaserMonster(E_Direction dir, GameObject player, GameObject spawner, Timer timer, float timeForArrival, float locY)
    {
        HasSpecialMonsterSpawned = true;
        //Set LocX
        float locX;
        if (E_Direction.Left == dir) { locX = -MonsterSpawnLocDx; }
        else locX = MonsterSpawnLocDx;

        //Spawn Moster
        Monster = Instantiate(LaserMonsterRef);                                 //TODO : change to ObjectPool


        if (!Monster) return;
        MonsterMovement movement = Monster.GetComponent<MonsterMovement>();
        SIDLaserMonster sMon = Monster.GetComponent<SIDLaserMonster>();

        //Set Movement variables
        if (!movement) return;
        movement.SetMonsterMovementInfo(player, timeForArrival);


        if (!sMon) return;
        sMon.SetMonsterInfo(dir, spawner, BigHp, timer);
        sMon.SetTeam(this);

        if (dir != E_Direction.Left)
        {
            Vector3 vec = sMon.SpawnLocation.transform.position;
            vec.x = -vec.x;
            sMon.SpawnLocation.transform.position = vec;
        }


        //Set location
        Monster.transform.position = new Vector3(locX, locY, 0);
    }

}
