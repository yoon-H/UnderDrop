using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject MonsterPrefab;
    public GameObject Player;

    GameObject Monster;

    float CurrentTime;
    float MaxTime = 4f;

    private float SpawnLocDx = 1.53f;

    private bool LeftIsExisted = false;
    private bool RightIsExisted = false;

    // Start is called before the first frame update
    void Start()
    {
        CurrentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentTime >= MaxTime)
        {
            int res = Random.Range(0, 3);
            if (res == 0) // spawn left
            {
                if (!LeftIsExisted)
                {
                    Monster = Instantiate(MonsterPrefab);                                 //TODO : change to ObjectPool
                    MonsterMovement mon = Monster.GetComponent<MonsterMovement>();
                    mon.PlayerRef = Player;
                    Monster.transform.position = new Vector3(-SpawnLocDx, transform.position.y, 0);
                    LeftIsExisted=true;
                }
                
            }
            else if (res == 1) // spawn right
            {
                if (!RightIsExisted)
                {
                    Monster = Instantiate(MonsterPrefab);
                    MonsterMovement mon = Monster.GetComponent<MonsterMovement>();
                    mon.PlayerRef = Player;
                    Monster.transform.position = new Vector3(SpawnLocDx, transform.position.y, 0);
                    RightIsExisted=true;
                }
                
            }

            CurrentTime -= MaxTime;
        }

        CurrentTime += Time.deltaTime;
    }
}
