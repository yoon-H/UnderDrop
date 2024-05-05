using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaselObstacleMonster : Monster
{
    public float LifeTime = 2f;
    public float WarningTime = 3f;

    public GameObject FogRef;

    public float SpawnLocY = -4.5f;
    private WeaselTeam Team;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(IE_ChangeTransform());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator IE_ChangeTransform()
    {
        yield return new WaitForSeconds(LifeTime);
        StartCoroutine(IE_Warning());

    }

    IEnumerator IE_Warning()
    {
        //TODO :: Animation
        yield return new WaitForSeconds(WarningTime);
        //Spawn Fog Object
        DestroyTasks();
     
    }

    private void DestroyTasks()
    {
        if (!Spawner) return;
        Spawner.SetDestroyedMonster(Direction);
        if (!Team) return;
        Team.SetIsObstacleMonsterSpawned(false);
        
        GameObject Fog = Instantiate(FogRef);
        Fog.transform.position = transform.position;

        System.Random rand = new System.Random();
        int res = rand.Next(2);
        E_Direction dir;
        if(res == 0)
        {
            dir = E_Direction.Left;
        }
        else
        {
            dir = E_Direction.Right;
        }

        GameObject obs = Timer.WeaselTeam.SpawnObstacle(dir, Timer, Timer.ObstacleSpawner.CurTimeForArrival, SpawnLocY);
        GameObject obsFog = Instantiate(FogRef);
        obs.GetComponentInChildren<SpriteRenderer>().color = Color.red;
        obsFog.transform.position = obs.transform.position;

        Destroy(obs, 8f);
        Destroy(gameObject);

    }

    public void SetTeam(WeaselTeam team)
    {
        Team = team;
    }

    protected override void DeadTask()
    {
        if (!Spawner) return;
        Spawner.SetDestroyedMonster(Direction);
        if (!Team) return;
        Team.SetIsObstacleMonsterSpawned(false);

        Destroy(gameObject);
    }
}
