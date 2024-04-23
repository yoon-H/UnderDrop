using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class NormalTeam : TeamRegion
{
    public GameObject ThornObstacleRef;
    public GameObject SawToothObstacleRef;
    public GameObject ButtonTypeObstacleRef;

    private const float ToothSpawnLocDx = 1.8f;
    private const float ButtonTypeSpawnLocDx = 1.4f;

    private GameObject Obstacle;

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

    public override void SpawnMonster(E_Direction dir, Timer timer, float timeForArrival, float locY)
    {
        
    }

    private void SpawnThornObstacle(E_Direction dir, Timer timer, float timeForArrival, float locY)
    {
        float locX;
        if (E_Direction.Left == dir) { locX = -ButtonTypeSpawnLocDx; }
        else locX = ButtonTypeSpawnLocDx;


        Obstacle = Instantiate(ThornObstacleRef);                                 //TODO : change to ObjectPool
        if (!Obstacle) return;
        ObjectMovement obj = Obstacle.GetComponent<ObjectMovement>();
        if (!obj) return;
        obj.SetTimeForArrival(timeForArrival);

        Obstacle obs = Obstacle.GetComponent<Obstacle>();
        if (!obs) return;
        obs.InitializeObstacleStats(timer);

        Obstacle.transform.position = new Vector3(locX, locY, 0);
    }

    private void SpawnSawToothObstacle(E_Direction dir, Timer timer, float timeForArrival, float locY)
    {
        float locX;
        if (E_Direction.Left == dir) { locX = -ToothSpawnLocDx; }
        else locX = ToothSpawnLocDx;

        Obstacle = Instantiate(SawToothObstacleRef);                                 //TODO : change to ObjectPool
        if (!Obstacle) return;
        ObjectMovement obj = Obstacle.GetComponent<ObjectMovement>();
        SawTooth tooth = Obstacle.GetComponent<SawTooth>();
        if (!obj) return;
        if (!tooth) return;
        obj.SetTimeForArrival(timeForArrival);
        tooth.InitializeObstacleStats(timer);
        tooth.SetRotateDirection(E_Direction.Left);

        Obstacle.transform.position = new Vector3(locX, locY, 0);
    }

    private void SpawnButtonTypeObstacle(E_Direction dir, Timer timer, float timeForArrival, float locY)
    {
        float locX;
        if (E_Direction.Left == dir) { locX = - ButtonTypeSpawnLocDx; }
        else locX = ButtonTypeSpawnLocDx;

        Obstacle = Instantiate(ButtonTypeObstacleRef);                                //TODO : change to ObjectPool

        if (!Obstacle) return;
        ObjectMovement obj = Obstacle.GetComponent<ObjectMovement>();
        ButtonTypeObstacle bto = Obstacle.GetComponent<ButtonTypeObstacle>();
        
        if (!obj) return;
        if (!bto) return;
        obj.SetTimeForArrival(timeForArrival);

        Obstacle.transform.position = new Vector3(0, locY, 0);

        if (!bto.LaserButtonRef) return;
        bto.LaserButtonRef.gameObject.transform.position = new Vector3(locX, locY, 0);

        if (!bto.LaserObstacleRef) return;
        Obstacle obs = bto.LaserObstacleRef.GetComponent<Obstacle>();
        
        if (!obs) return;
        obs.InitializeObstacleStats(timer);
    }
}
