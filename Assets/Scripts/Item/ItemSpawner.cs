using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject TimerRef;
    private Timer Timer;
    private Player Player;

    public GameObject CoinRef;
    public GameObject CoinDoubleItemRef;
    public GameObject DamageDoubleItemRef;
    public GameObject InvincibleItemRef;

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
        Player = Timer.GetPlayer();

        if (Player == null)
        {
            print("ItemSpawner :: Player is null");
        }
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

        bool willSpawn = GenerateSpawnItem();

        int cnt = 0;

        while(num > cnt)
        {
            SpawnCoinTask(direction, cnt);

            var time = new WaitForSeconds(SpawnTime);
            cnt++;
            yield return time;
        }

        if(willSpawn)
        {
            var time = new WaitForSeconds(SpawnTime);
            yield return time;

            SelectSpawnItem(direction,cnt);

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
        
        ObjectMovement movement = CoinObject.GetComponent<ObjectMovement>();
        if(!movement) return;
        movement.SetTimeForArrival(CurTimeForArrival);


        Coin coin = CoinObject.GetComponent<Coin>();

        //Set Timer
        if (!coin) return;
        coin.SetTimer(Timer);

        //Set Location
        CoinObject.transform.position = new Vector3(locX, gameObject.transform.position.y, zValue);

        Destroy(CoinObject, 6f);
    }

    public void SetPlayer(GameObject playerObject)
    {
        Player = playerObject.GetComponent<Player>();
    }

    private bool GenerateSpawnItem()
    {
        System.Random rand = new System.Random();
        int res = rand.Next(10);

        //if (res <= 2) return true;
        //else return false;

        return true;
    }

    private void SelectSpawnItem(E_Direction dir, int zValue)
    {
        System.Random random = new System.Random();
        int res = 2;//random.Next(3);

        //Set LocX
        float locX;
        if (E_Direction.Left == dir) { locX = -SpawnXLoc; }
        else locX = SpawnXLoc;

        switch(res)
        {
            case 0:
                SpawnDamageDoubleItem(locX, zValue);
                break;
            case 1:
                SpawnCoinDoubleItem(locX, zValue);
                break;
            case 2:
                SpawnInvincibleItem(locX, zValue);
                break;
        }
    }

    private void SpawnCoinDoubleItem(float locX, float zValue)
    {

        //Spawn Item
        GameObject ItemObject = Instantiate(CoinDoubleItemRef);                                 //TODO : change to ObjectPool
        if (!ItemObject) return;

        ObjectMovement movement = ItemObject.GetComponent<ObjectMovement>();
        if (!movement) return;
        movement.SetTimeForArrival(CurTimeForArrival);


        CoinDoubleItem coinDouble = ItemObject.GetComponent<CoinDoubleItem>();

        //Set Timer
        if (!coinDouble) return;
        coinDouble.SetTimer(Timer);

        //Set Location
        ItemObject.transform.position = new Vector3(locX, gameObject.transform.position.y, zValue);

        Destroy(ItemObject, 6f);
    }

    private void SpawnDamageDoubleItem(float locX, float zValue)
    {

        //Spawn Item
        GameObject ItemObject = Instantiate(DamageDoubleItemRef);                                 //TODO : change to ObjectPool
        if (!ItemObject) return;

        ObjectMovement movement = ItemObject.GetComponent<ObjectMovement>();
        if (!movement) return;
        movement.SetTimeForArrival(CurTimeForArrival);

        DamageDoubleItem damageDouble = ItemObject.GetComponent<DamageDoubleItem>();

        //Set Timer
        if (!damageDouble) return;
        damageDouble.SetPlayer(Player);

        //Set Location
        ItemObject.transform.position = new Vector3(locX, gameObject.transform.position.y, zValue);

        Destroy(ItemObject, 6f);
    }

    private void SpawnInvincibleItem(float locX, float zValue)
    {

        //Spawn Item
        GameObject ItemObject = Instantiate(InvincibleItemRef);                                 //TODO : change to ObjectPool
        if (!ItemObject) return;

        ObjectMovement movement = ItemObject.GetComponent<ObjectMovement>();
        if (!movement) return;
        movement.SetTimeForArrival(CurTimeForArrival);

        InvincibleItem invincible = ItemObject.GetComponent<InvincibleItem>();

        //Set Timer
        if (!invincible) return;
        invincible.SetPlayer(Player);

        //Set Location
        ItemObject.transform.position = new Vector3(locX, gameObject.transform.position.y, zValue);

        Destroy(ItemObject, 6f);
    }

}
