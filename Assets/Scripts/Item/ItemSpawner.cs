using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject TimerRef;
    private Timer Timer;

    public GameObject CoinRef;

    #region SpawnTime
    public float MaxTimeForArrival = 3f;
    public float CurTimeForArrival;
    public float MinTimeForArrival = 2.4f;
    public float TimeForArrivalReducingAmount = 0.2f;
    #endregion

    private float SpawnXLoc = 1.5f;
    private float SpawnTime = 0.2f;
    public float SpawnPeriod = 4f;

    // Start is called before the first frame update
    void Start()
    {
        CurTimeForArrival = MaxTimeForArrival;

        // Timer Initializing
        Timer = TimerRef.GetComponent<Timer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateCoinLocation()
    {
        System.Random rand = new System.Random();
        int res = rand.Next(3);

        if(res == 0)//Spawn Left
        {
            StartCoroutine(nameof(IE_SpawnCoin), E_Direction.Left);
        }
        else if (res == 1)  //Spawn Right
        {
            StartCoroutine(nameof(IE_SpawnCoin), E_Direction.Right);
        }

    }

    //Set Random Coin Number
    int SetCoinNum()
    {
        System.Random rand = new System.Random();
        int res = rand.Next(3, 7);

        return res;
    }

    //ReduceTime
    public void ReduceTimeForArrival()
    {
        if (CurTimeForArrival > MinTimeForArrival)
        {
            CurTimeForArrival -= TimeForArrivalReducingAmount;
        }
    }

    //Spawn Coin Sequence
    IEnumerator IE_SpawnCoin(E_Direction direction)
    {
        int num = SetCoinNum();

        int cnt = 0;

        while(num > cnt)
        {
            SpawnCoinTask(direction, cnt);

            var time = new WaitForSeconds(SpawnTime);
            cnt++;
            yield return time;
        }
        


        
    }

    void SpawnCoinTask(E_Direction dir, int zValue)
    {
        //Set LocX
        float locX;
        if (E_Direction.Left == dir) { locX = -SpawnXLoc; }
        else locX = SpawnXLoc;

        //Spawn Coin
        
        GameObject CoinObject= Instantiate(CoinRef);                                 //TODO : change to ObjectPool
        if (!CoinObject) return;
        Coin coin = CoinObject.GetComponent<Coin>();

        //Set Speed variable
        if (!coin) return;
        coin.SetTimer(Timer);

        //Set Location
        CoinObject.transform.position = new Vector3(locX, gameObject.transform.position.y, zValue);

        Destroy(CoinObject, 6f);
    }



}
