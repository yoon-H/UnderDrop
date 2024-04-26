using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    private bool LeftIsExisted = false;
    private bool RightIsExisted = false;

    public GameObject LeftEnemyBar;
    public GameObject RightEnemyBar;

    public float MaxTimeForArrival = 10f;
    private float CurTimeForArrival;
    public float MinTimeForArrival = 8.6f;
    public float TimeForArrivalReducingAmount = 0.2f;

    public GameObject TimerRef;
    private Timer Timer;

    public GameObject PlayerRef;

    // Start is called before the first frame update
    void Start()
    {
        CurTimeForArrival = MaxTimeForArrival;

        if (!TimerRef) return;
        Timer = TimerRef.GetComponent<Timer>();
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
                if (!Timer) return;
                Timer.NormalTeam.SpawnMonster(E_Direction.Left, PlayerRef, gameObject, Timer, CurTimeForArrival, gameObject.transform.position.y);
                LeftIsExisted = true;
            }

        }
        else if (res == 1) // spawn right
        {
            if (!RightIsExisted)
            {
                Timer.NormalTeam.SpawnMonster(E_Direction.Right, PlayerRef, gameObject, Timer, CurTimeForArrival, gameObject.transform.position.y);
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

    public void ReduceTimeForArrival()
    {
        if(CurTimeForArrival > MinTimeForArrival)
        {
            CurTimeForArrival -= TimeForArrivalReducingAmount;
        }
    }
}
