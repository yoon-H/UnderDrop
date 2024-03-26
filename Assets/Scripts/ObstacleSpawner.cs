using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject ObstaclePrefab;

    GameObject Obstacle;

    float CurrentTime;
    float MaxTime = 1f;

    private float SpawnLocDx = 1.934f;

    // Start is called before the first frame update
    void Start()
    {
        CurrentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(CurrentTime >= MaxTime)
        {
            int res = Random.Range(0, 3);
            print(res);
            if(res == 0 ) // spawn left
            {
                Obstacle = Instantiate(ObstaclePrefab);                                 //TODO : change to ObjectPool
                ObastacleMovement obs= Obstacle.GetComponent<ObastacleMovement>();
                obs.SetRotateDirection(E_Direction.Left);

                Obstacle.transform.position = new Vector3(-SpawnLocDx, transform.position.y, 0);
            }
            else if(res == 1 ) // spawn right
            {
                Obstacle = Instantiate(ObstaclePrefab);
                ObastacleMovement obs = Obstacle.GetComponent<ObastacleMovement>();
                obs.SetRotateDirection(E_Direction.Right);
                Obstacle.transform.position = new Vector3(SpawnLocDx, transform.position.y, 0);
            }

            CurrentTime -= MaxTime;
        }

        CurrentTime += Time.deltaTime;
        Destroy(Obstacle, 8f);
        
    }
}
