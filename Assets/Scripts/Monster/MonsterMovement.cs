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

    // Prev movement
    private float PrevMonsterSpeed;
    private float PrevTimeForArrival = 0.4f;
    private float PrevYLoc = 4f;

    private float TimeForArrival;

    // Start is called before the first frame update
    void Start()
    {
        // MoveSpeed
        MonsterYSize = transform.localScale.y;
        float MonsterLoc = Camera.main.orthographicSize - MonsterYSize / 2;
        float PlayerLoc = PlayerRef.transform.position.y + PlayerRef.transform.localScale.y / 2;

        MonsterMoveSpeed = (MonsterLoc - PlayerLoc) / TimeForArrival;

        //PrevSpeed
        PrevMonsterSpeed = Math.Abs(transform.position.y - MonsterLoc) / PrevTimeForArrival;

        PrevYLoc = Camera.main.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y >= PrevYLoc)
        {
            transform.position += new Vector3(0f, -1f, 0f) * PrevMonsterSpeed * Time.deltaTime;
        }
        else
        {
            if(transform.position.y >= PlayerRef.transform.position.y)
            {
                transform.position += new Vector3(0f, -1f, 0f) * MonsterMoveSpeed * Time.deltaTime;
            }
        }
    }

    public void SetMonsterMovementInfo(GameObject player, float timeForArrival)
    {
        PlayerRef = player;
        TimeForArrival = timeForArrival;
    }

}
