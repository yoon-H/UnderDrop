using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Timer : MonoBehaviour
{
    public GameObject ObstacleSpawnerRef;
    private ObstacleSpawner ObstacleSpawner;
    public GameObject MonsterSpawnerRef;
    private MonsterSpawner MonsterSpawner;

    private float ObstacleSpawnCounter = 0f;
    private float MonsterSpawnCounter = 0f;

    public float ObstacleSpawnTime = 1f;
    public float MonsterSpawnTime = 4f;

    private bool IsPaused = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ObstacleSpawnCounter += Time.deltaTime;
        MonsterSpawnCounter += Time.deltaTime;

        if (ObstacleSpawner == null)
        {
            ObstacleSpawner = ObstacleSpawnerRef.GetComponent<ObstacleSpawner>();
        }

        if (ObstacleSpawnCounter >= ObstacleSpawnTime)
        {
            ObstacleSpawnCounter -= ObstacleSpawnTime;
            ObstacleSpawner.SpawnObstacle();
        }

        if (MonsterSpawner == null)
        {
            MonsterSpawner = MonsterSpawnerRef.GetComponent<MonsterSpawner>();
        }

        if (MonsterSpawnCounter >= MonsterSpawnTime)
        {
            MonsterSpawnCounter -= MonsterSpawnTime;
            MonsterSpawner.SpawnMonster();
        }
    }

    public void SetIsPaused(bool flag)
    {
        if(flag)
        {
            IsPaused = true;
            Time.timeScale = 0;
        }
        else
        {
            IsPaused = false;
            Time.timeScale = 1;
        }
    }
}
