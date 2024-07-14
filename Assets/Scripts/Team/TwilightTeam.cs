using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwilightTeam : TeamRegion
{
    [Header("Obstacle")]
    public GameObject FeatherObstacleRef;
    public GameObject CardObstacleRef;

    private const float FeatherSpawnLocDx = 1.5f;
    private const float CardSpawnLocDx = 2f;

    private GameObject Obstacle;


    [Header("Monster")]
    public GameObject NormalMonsterRef;
    public GameObject DebuffMonsterRef;

    private const float MonsterSpawnLocDx = 1.55f;

    private const int Hp = 80;

    private GameObject Monster;

    private bool IsObstacleMonsterSpawned = false;

    public override GameObject SpawnObstacle(E_Direction dir, Timer timer, float timeForArrival, float locY)
    {
        System.Random rand = new System.Random();
        int res = rand.Next(100);
        if (res <=59)
        {
            SpawnFeatherObstacle(dir, timer, timeForArrival, locY);
        }
        else
        {
            SpawnCardObstacle(dir, timer, timeForArrival, locY);
        }

        return Obstacle;
    }

    public override void SpawnMonster(E_Direction dir, GameObject player, GameObject spawner, Timer timer, float timeForArrival, float locY)
    {
        System.Random rand = new System.Random();
        int res = rand.Next(100);
        if (IsObstacleMonsterSpawned)
        {
            //SpawnNormalMonster(dir, player, spawner, timer, timeForArrival, locY);
        }
        else
        {
            if (res <= 64)
            {
                //SpawnNormalMonster(dir, player, spawner, timer, timeForArrival, locY);
            }
            else
            {
                //SpawnObstacleMonster(dir, player, spawner, timer, timeForArrival, locY);
            }
        }

    }

    //Obstacle
    private void SpawnFeatherObstacle(E_Direction dir, Timer timer, float timeForArrival, float locY)
    {
        //Set LocX
        float locX;
        if (E_Direction.Left == dir) { locX = -FeatherSpawnLocDx; }
        else locX = FeatherSpawnLocDx;

        //Spawn Obstacle
        Obstacle = Instantiate(FeatherObstacleRef);                                 //TODO : change to ObjectPool
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

    //Obstacle
    private void SpawnCardObstacle(E_Direction dir, Timer timer, float timeForArrival, float locY)
    {
        //Set LocX
        float locX;
        if (E_Direction.Left == dir) { locX = -CardSpawnLocDx; }
        else locX = CardSpawnLocDx;

        //Spawn Obstacle
        Obstacle = Instantiate(CardObstacleRef);                                 //TODO : change to ObjectPool
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

        if(dir == E_Direction.Right)
        {
            Vector3 vec = Obstacle.transform.localScale;

            vec.x *= -1;

            Obstacle.transform.localScale = vec;
        }
    }
}
