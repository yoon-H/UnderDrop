using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject ImpactPrefab;
    public GameObject ExplosionPrefab;
    private GameObject Monster;

    private float MoveSpeed;

    private Vector3 MoveVector = Vector3.zero;

    public int Damage = 20;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!Monster)
        {
            Destroy(gameObject);
            return;
        }

        // MoveSpeed
        Vector3 vec = Monster.transform.position - transform.position;
        float dist = Vector3.Distance(Monster.transform.position, transform.position);
        vec.Normalize();

        MoveSpeed = dist * 10f;
        MoveVector = vec;

        transform.position += MoveVector * MoveSpeed * Time.deltaTime;

        transform.right = vec;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Monster mon = collision.gameObject.GetComponent<Monster>();

        if(mon)
        {
            Instantiate(ImpactPrefab, collision.transform.position, collision.transform.rotation);
            mon.TakeDamage(Damage);
            Destroy(gameObject);
        }
    }

    public void SetBulletInfo(GameObject monster, int damage)
    {
        Monster = monster;
        Damage = damage;

        Monster = monster.GetComponentInParent<Collider2D>().gameObject;
    }
}
