using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class NormalTeam : TeamRegion
{
    [Header("Obstacle")]
    public GameObject ThornObstacleRef;
    public GameObject SawToothObstacleRef;
    public GameObject ButtonTypeObstacleRef;

    private const float ToothSpawnLocDx = 1.94f;
    private const float ButtonTypeSpawnLocDx = 1.55f;

    private GameObject Obstacle;


    [Header("Monster")]
    public GameObject LowHpMonsterRef;
    public GameObject MiddleHpMonsterRef;
    public GameObject HighHpMonsterRef;

    private const float MonsterSpawnLocDx = 1.55f;

    private const int LowHp = 50;
    private const int MiddleHp = 70;
    private const int HighHp = 100;

    private GameObject Monster;

    public override GameObject SpawnObstacle(E_Direction dir, Timer timer, float timeForArrival, float locY)
    {
        System.Random rand = new System.Random();
        int res = rand.Next(100);
        if (res <= 39)
        {
            SpawnThornObstacle(dir, timer, timeForArrival, locY);
        }
        else if (res <= 79)
        {
            SpawnSawToothObstacle(dir, timer, timeForArrival, locY);
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
        if (res <= 39)
        {
            SpawnLowHpMonster(dir, player, spawner, timer, timeForArrival, locY);
        }
        else if (res <= 79)
        {
            SpawnMiddleHpMonster(dir, player, spawner, timer, timeForArrival, locY);
        }
        else
        {
            SpawnHighHpMonster(dir, player, spawner, timer, timeForArrival, locY);

        }
    }

    //Obstacle
    private void SpawnThornObstacle(E_Direction dir, Timer timer, float timeForArrival, float locY)
    {
        //Set LocX
        float locX;
        if (E_Direction.Left == dir) { locX = -ButtonTypeSpawnLocDx; }
        else locX = ButtonTypeSpawnLocDx;

        //Spawn Obstacle
        Obstacle = Instantiate(ThornObstacleRef);                                 //TODO : change to ObjectPool
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

    private void SpawnSawToothObstacle(E_Direction dir, Timer timer, float timeForArrival, float locY)
    {
        //Set LocX
        float locX;
        if (E_Direction.Left == dir) { locX = -ToothSpawnLocDx; }
        else locX = ToothSpawnLocDx;

        //Spawn Obstacle
        Obstacle = Instantiate(SawToothObstacleRef);                                 //TODO : change to ObjectPool
        if (!Obstacle) return;
        ObjectMovement obj = Obstacle.GetComponent<ObjectMovement>();
        SawTooth tooth = Obstacle.GetComponent<SawTooth>();
        
        if (!obj) return;
        if (!tooth) return;

        //Set Speed variable
        obj.SetTimeForArrival(timeForArrival);

        //Set Timer, Direction
        tooth.InitializeObstacleStats(timer);
        tooth.SetRotateDirection(E_Direction.Left);

        //Set Location
        Obstacle.transform.position = new Vector3(locX, locY, 0);
    }

    private void SpawnButtonTypeObstacle(E_Direction dir, Timer timer, float timeForArrival, float locY)
    {
        //Set LocX
        float locX;
        if (E_Direction.Left == dir) { locX = - ButtonTypeSpawnLocDx; }
        else locX = ButtonTypeSpawnLocDx;

        //Spawn Obstacle
        Obstacle = Instantiate(ButtonTypeObstacleRef);                                //TODO : change to ObjectPool

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

        if (!bto.LaserObstacleRef) return;
        Obstacle obs = bto.LaserObstacleRef.GetComponent<Obstacle>();
        
        //Set Timer
        if (!obs) return;
        obs.InitializeObstacleStats(timer);
    }

    // Monster
    private void SpawnLowHpMonster(E_Direction dir, GameObject player, GameObject spawner, Timer timer, float timeForArrival, float locY)
    {
        //Set LocX
        float locX;
        if (E_Direction.Left == dir) { locX = -MonsterSpawnLocDx; }
        else locX = MonsterSpawnLocDx;

        //Spawn Moster
        Monster = Instantiate(LowHpMonsterRef);                                 //TODO : change to ObjectPool


        if (!Monster) return;
        MonsterMovement movement = Monster.GetComponent<MonsterMovement>();
        Monster mon = Monster.GetComponent<Monster>();

        //Set Movement variables
        if (!movement) return;
        movement.SetMonsterMovementInfo(player, timeForArrival);


        if (!mon) return;
        mon.SetMonsterInfo(dir, spawner, LowHp, timer);



        //Set location
        Monster.transform.position = new Vector3(locX, locY, 0);
    }

    private void SpawnMiddleHpMonster(E_Direction dir, GameObject player, GameObject spawner, Timer timer, float timeForArrival, float locY)
    {
        //Set LocX
        float locX;
        if (E_Direction.Left == dir) { locX = -MonsterSpawnLocDx; }
        else locX = MonsterSpawnLocDx;

        //Spawn Moster
        Monster = Instantiate(MiddleHpMonsterRef);                                 //TODO : change to ObjectPool


        if (!Monster) return;
        MonsterMovement movement = Monster.GetComponent<MonsterMovement>();
        Monster mon = Monster.GetComponent<Monster>();

        //Set Movement variables
        if (!movement) return;
        movement.SetMonsterMovementInfo(player, timeForArrival);


        if (!mon) return;
        mon.SetMonsterInfo(dir, spawner, MiddleHp, timer);



        //Set location
        Monster.transform.position = new Vector3(locX, locY, 0);
    }

    private void SpawnHighHpMonster(E_Direction dir, GameObject player, GameObject spawner, Timer timer, float timeForArrival, float locY)
    {
        //Set LocX
        float locX;
        if (E_Direction.Left == dir) { locX = -MonsterSpawnLocDx; }
        else locX = MonsterSpawnLocDx;

        //Spawn Moster
        Monster = Instantiate(HighHpMonsterRef);                                 //TODO : change to ObjectPool


        if (!Monster) return;
        MonsterMovement movement = Monster.GetComponent<MonsterMovement>();
        Monster mon = Monster.GetComponent<Monster>();

        //Set Movement variables
        if (!movement) return;
        movement.SetMonsterMovementInfo(player, timeForArrival);


        if (!mon) return;
        mon.SetMonsterInfo(dir, spawner, HighHp, timer);



        //Set location
        Monster.transform.position = new Vector3(locX, locY, 0);
    }
}
