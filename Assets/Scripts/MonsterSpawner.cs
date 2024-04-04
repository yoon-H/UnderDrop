using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject MonsterPrefab;
    public GameObject Player;

    GameObject Monster;

    private float SpawnLocDx = 1.53f;

    private bool LeftIsExisted = false;
    private bool RightIsExisted = false;

    public GameObject LeftEnemyBar;
    public GameObject RightEnemyBar;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnMonster()
    {
        int res = Random.Range(0, 3);
        if (res == 0) // spawn left
        {
            if (!LeftIsExisted)
            {
                Monster = Instantiate(MonsterPrefab);                                 //TODO : change to ObjectPool
                if (!Monster) return;
                MonsterMovement mon = Monster.GetComponent<MonsterMovement>();
                if (!mon) return;
                mon.SetMonsterInfo(Player, E_Direction.Left, LeftEnemyBar, gameObject);
                Monster.transform.position = new Vector3(-SpawnLocDx, transform.position.y, 0);
                LeftIsExisted = true;
            }

        }
        else if (res == 1) // spawn right
        {
            if (!RightIsExisted)
            {
                Monster = Instantiate(MonsterPrefab);
                if (!Monster) return;
                MonsterMovement mon = Monster.GetComponent<MonsterMovement>();
                if (!mon) return;
                mon.SetMonsterInfo(Player, E_Direction.Right, RightEnemyBar, gameObject);
                Monster.transform.position = new Vector3(SpawnLocDx, transform.position.y, 0);
                RightIsExisted = true;
            }

        }
    }

    public void SetDestroyedMonster(E_Direction dir)
    {
        if(dir == E_Direction.Left)
        {
            LeftIsExisted = false;
        }
        else
        {
            RightIsExisted = false;
        }
    }
}
