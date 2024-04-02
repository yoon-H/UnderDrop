using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private GameObject MonsterRef;
    private MonsterMovement Monster;

    private float MoveSpeed;

    private Vector3 MoveVector = Vector3.zero;

    public int Damage = 20;


    // Start is called before the first frame update
    void Start()
    {
        // MoveSpeed
        Vector3 vec = MonsterRef.transform.position - transform.position;
        float dist = Vector3.Distance(MonsterRef.transform.position, transform.position);
        vec.Normalize();

        MoveSpeed = dist * 10f;

        MoveVector = vec;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += MoveVector * MoveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Monster"))
        {
            print("Collision!!!!");
            Monster.TakeDamage(Damage);
            Destroy(gameObject);
        }
    }

    public void SetBulletInfo(GameObject monster)
    {
        MonsterRef = monster;
    }
}
