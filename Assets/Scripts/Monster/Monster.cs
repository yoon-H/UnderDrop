using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;
using UnityEngine;

public class Monster : MonoBehaviour, IHittable
{
    protected Timer Timer;

    [SerializeField]
    private int CurHp;
    private int MaxHp = 100;

    public E_Direction Direction;

    public GameObject LifeBarRef;
    private ProgressBar LifeBar;

    public MonsterSpawner Spawner;

    private MonsterAnimation AnimationRef;

    public GameObject DataAsset;
    public GameObject DeadAsset;

    // Start is called before the first frame update
    void Start()
    {
        if(!LifeBarRef) return;
        LifeBar = LifeBarRef.GetComponent<ProgressBar>();

        AnimationRef = GetComponent<MonsterAnimation>();
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
        if(CurHp > 0)
        {
            CurHp -= amount;
            if(CurHp < 0) CurHp = 0;
            if (!LifeBar) return;
            LifeBar.Value = CurHp;
            LifeBar.SetWidth();

            if (AnimationRef && CurHp > 0)
                AnimationRef.PlayTakeDamageAnimation();
        }
        

        if (CurHp <= 0)
        {
            DeadTask();
        }
    }

    public void SetMonsterInfo(E_Direction direction, GameObject monsterSpawner, int maxHp, Timer timer)
    {
        Direction = direction;
        Spawner = monsterSpawner.GetComponent<MonsterSpawner>();
        MaxHp = maxHp;
        CurHp = MaxHp;
        Timer = timer;

        if(direction != E_Direction.Left) { FlipMesh(); }

        InitializeLifeBar();
    }

    private void InitializeLifeBar()
    {
        //Initialize LifeBar
        if(!LifeBarRef) return;
        LifeBar = LifeBarRef.GetComponent<ProgressBar>();
        if (!LifeBar) return;
        LifeBar.SetActiveProgress(true);
        LifeBar.Maxvalue = MaxHp;
        LifeBar.Value = MaxHp;
        LifeBar.InitProgressBar();
    }

    protected virtual void DeadTask()
    {

        if (!Spawner) return;
        Spawner.SetDestroyedMonster(Direction);

        AnimationRef.PlayDieAnimation();

    }

    private void FlipMesh()
    {
        if(DataAsset)
        {
            Vector3 vec = DataAsset.transform.localScale;
            vec.x = -vec.x;
            DataAsset.transform.localScale = vec;
        }

        if (DeadAsset)
        {
            Vector3 vec = DeadAsset.transform.localScale;
            vec.x = -vec.x;
            DeadAsset.transform.localScale = vec;
        }
    }
}
