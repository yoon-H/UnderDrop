using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    private float MonsterYSize;
    private float MonsterMoveSpeed;

    public GameObject PlayerRef;

    [SerializeField]
    private Int32 CurHp;
    private Int32 MaxHp = 100;

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
        
        //TODO : Set UI

        if(CurHp <= 0)
        {
            // Monster died
            Destroy(gameObject);

            //TODO : Turn Off the UI

        }
    }
}
