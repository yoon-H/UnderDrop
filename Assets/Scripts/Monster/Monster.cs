using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour, IHittable
{
    Timer Timer;

    [SerializeField]
    private int CurHp;
    private int MaxHp = 100;

    public E_Direction Direction;

    public GameObject LifeBarRef;
    private ProgressBar LifeBar;

    public MonsterSpawner Spawner;

    // Start is called before the first frame update
    void Start()
    {
        //if(!LifeBarRef) return;
        //LifeBar = LifeBarRef.GetComponent<ProgressBar>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnHit()
    {
        Timer.GameOver();
    }

    public void TakeDamage(int amount)
    {
        CurHp -= amount;
        //if (!LifeBar) return;
        //LifeBar.Value = CurHp;
        //LifeBar.SetWidth();
        print(CurHp);

        if (CurHp <= 0)
        {
            // Monster died
            //LifeBar.Value = LifeBar.Maxvalue;
            //LifeBar.SetWidth();
            //LifeBar.SetActiveProgress(false);
            if (!Spawner) return;
            Spawner.SetDestroyedMonster(Direction);

            Destroy(gameObject);
        }
    }

    public void SetMonsterInfo(E_Direction direction, GameObject monsterSpawner, int maxHp, Timer timer)
    {
        Direction = direction;
        Spawner = monsterSpawner.GetComponent<MonsterSpawner>();
        MaxHp = maxHp;
        CurHp = MaxHp;
        Timer = timer;

        InitializeLifeBar();
    }

    private void InitializeLifeBar()
    {
        //Initialize LifeBar
        if(!LifeBarRef) return;
        LifeBar = LifeBarRef.GetComponent<ProgressBar>();
        if (!LifeBar) return;
        //LifeBar.SetActiveProgress(true);
        LifeBar.Maxvalue = MaxHp;
        LifeBar.Value = MaxHp;
        LifeBar.SetWidth();
    }
}
