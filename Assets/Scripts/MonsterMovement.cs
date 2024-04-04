using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    private float MonsterYSize;
    private float MonsterMoveSpeed;

    public GameObject PlayerRef;

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
        // MoveSpeed
        MonsterYSize = transform.localScale.y;
        float MonsterLoc = Camera.main.orthographicSize - MonsterYSize / 2;

        float PlayerLoc = PlayerRef.transform.position.y + PlayerRef.transform.localScale.y / 2;

        MonsterMoveSpeed = (MonsterLoc - PlayerLoc) / 10;

        //Hp Initialize
        CurHp = MaxHp;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0f, -1f, 0f) * MonsterMoveSpeed * Time.deltaTime;
    }


    public void TakeDamage(int amount)
    {
        CurHp -= amount;
        if (!LifeBar) return;
        LifeBar.Value = CurHp;
        LifeBar.SetWidth();

        if(CurHp <= 0)
        {
            // Monster died
            LifeBar.Value = LifeBar.Maxvalue;
            LifeBar.SetWidth();
            LifeBar.SetActiveProgress(false);
            if (!Spawner) return;
            Spawner.SetDestroyedMonster(Direction);

            Destroy(gameObject);
        }
    }

    public void SetMonsterInfo(GameObject player, E_Direction direction, GameObject lifeBar, GameObject monsterSpawner)
    {
        PlayerRef = player;
        Direction = direction;
        Spawner = monsterSpawner.GetComponent<MonsterSpawner>();
        LifeBarRef = lifeBar;

        if (!LifeBarRef) return;
        LifeBar = LifeBarRef.GetComponent<ProgressBar>();
        if (!LifeBar) return;
        LifeBar.SetActiveProgress(true);
        LifeBar.Maxvalue = MaxHp;
        LifeBar.Value = MaxHp;
        LifeBar.SetWidth();
    }
}
