using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject ObstaclePrefab;

    GameObject Obstacle;

    private float SpawnLocDx = 1.8f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnObstacle()
    {
        int res = Random.Range(0, 3);
        if (res == 0) // spawn left
        {
            Obstacle = Instantiate(ObstaclePrefab);                                 //TODO : change to ObjectPool
            if (!Obstacle) return;
            ObastacleMovement obs = Obstacle.GetComponent<ObastacleMovement>();
            if (!obs) return;
            obs.SetRotateDirection(E_Direction.Left);

            Obstacle.transform.position = new Vector3(-SpawnLocDx, transform.position.y, 0);
        }
        else if (res == 1) // spawn right
        {
            Obstacle = Instantiate(ObstaclePrefab);
            if (!Obstacle) return;
            ObastacleMovement obs = Obstacle.GetComponent<ObastacleMovement>();
            if (!obs) return;
            obs.SetRotateDirection(E_Direction.Right);
            Obstacle.transform.position = new Vector3(SpawnLocDx, transform.position.y, 0);
        }

        Destroy(Obstacle, 8f);
    }
}
