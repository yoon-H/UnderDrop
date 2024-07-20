using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaselTeam : TeamRegion
{
    [Header("Obstacle")]
    public GameObject KnifeObstacleRef;
    public GameObject DartObstacleRef;
    public GameObject BambooObstacleRef;

    private const float KnifeSpawnLocDx = 1.5f;
    //private const float DartSpawnLocDx = 1.4f;
    private const float BambooSpawnLocDx = 2f;

    private GameObject Obstacle;


    [Header("Monster")]
    public GameObject NormalMonsterRef;
    public GameObject ObstacleMonsterRef;

    private const float MonsterSpawnLocDx = 1.55f;

    private const int NormalHp = 60;
    private const int HighHp = 110;

    private GameObject Monster;

    public override GameObject SpawnObstacle(E_Direction dir, Timer timer, float timeForArrival, float locY)
    {
        System.Random rand = new System.Random();
        int res = rand.Next(100);
        if (res <= 59)
        {
            SpawnKnifeObstacle(dir, timer, timeForArrival, locY);
        }
        else if (res <= 79)
        {
            SpawnDartObstacle(dir, timer, timeForArrival, locY);
        }
        else
        {
            SpawnBambooObstacle(dir, timer, timeForArrival, locY);
        }

        return Obstacle;
    }

    public override void SpawnMonster(E_Direction dir, GameObject player, GameObject spawner, Timer timer, float timeForArrival, float locY)
    {
        System.Random rand = new System.Random();
        int res = rand.Next(100);
        if(HasSpecialMonsterSpawned)
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
    private void SpawnKnifeObstacle(E_Direction dir, Timer timer, float timeForArrival, float locY)
    {
        //Set LocX
        float locX;
        if (E_Direction.Left == dir) { locX = - KnifeSpawnLocDx; }
        else locX = KnifeSpawnLocDx;

        //Spawn Obstacle
        Obstacle = Instantiate(KnifeObstacleRef);                                 //TODO : change to ObjectPool
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

        OneshotAnim anim = Obstacle.GetComponent<OneshotAnim>();
        if(anim)
        {
            anim.SetTime(0.3f, "UP");
        }

        //Set Location
        Obstacle.transform.position = new Vector3(locX, locY, 0);
    }

    private void SpawnDartObstacle(E_Direction dir, Timer timer, float timeForArrival, float locY)
    {
        //Set LocX
        float locX;
        if (E_Direction.Left == dir) { locX = - KnifeSpawnLocDx; }
        else locX = KnifeSpawnLocDx;

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

    private void SpawnBambooObstacle(E_Direction dir, Timer timer, float timeForArrival, float locY)
    {
        //Set LocX
        float locX;
        if (E_Direction.Left == dir) { locX = -BambooSpawnLocDx; }
        else locX = BambooSpawnLocDx;

        //Spawn Obstacle
        Obstacle = Instantiate(BambooObstacleRef);                                 //TODO : change to ObjectPool
        if (!Obstacle) return;
        ObjectMovement obj = Obstacle.GetComponent<ObjectMovement>();

        //Set Speed variable
        if (!obj) return;
        obj.SetTimeForArrival(timeForArrival);

        ObjectDirection direction = Obstacle.GetComponent<ObjectDirection>();
        if (direction)
            direction.SetDirection(dir);

        //Set collider of BambooObstacle
        BambooObstacle bamboo = Obstacle.GetComponent<BambooObstacle>();
        if(!bamboo) return;

        bamboo.InitializeObstacleStats(timer);

        System.Random random = new System.Random();
        int res = random.Next(2);
        if(res == 0)
        {
            bamboo.SetCollider(true);
        }
        else
        {
            bamboo.SetCollider(false);
        }

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
        HasSpecialMonsterSpawned = true;
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
}
