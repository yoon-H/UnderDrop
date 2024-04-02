using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
using static UnityEngine.GraphicsBuffer;

public class Player : MonoBehaviour
{

    private E_Direction Dir = E_Direction.Right;
    private Vector3 LeftLoc = new Vector3(-1.53f,1.7f,0);
    private Vector3 RightLoc = new Vector3(1.53f,1.7f,0);
    private const float JumpTime = 0.15f;
    public Ease ease = Ease.Linear;

    public float PlayerYSize;
    public float PlayerYLoc;

    public GameObject BulletRef;
    public GameObject Bullet;

    // Start is called before the first frame update
    void Start()
    {
        PlayerYSize = transform.localScale.y;
        PlayerYLoc = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Jump(bool isLeft)
    {
        E_Direction dir = isLeft ? E_Direction.Left : E_Direction.Right;
        SwitchDir(dir);
    }

    private void SwitchDir(E_Direction dir)
    {
        if (dir == E_Direction.Left && Dir == E_Direction.Right) 
        {
            Dir= E_Direction.Left;
            transform.DOMoveX(LeftLoc.x, JumpTime).SetEase(ease);
        }
        else if (dir == E_Direction.Right && Dir == E_Direction.Left)
        {
            Dir=E_Direction.Right;
            transform.DOMoveX(RightLoc.x, JumpTime).SetEase(ease);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Obstacle"))
        {
            print("Tag is Obstacle");             // TODO : GameOver
        }

        if(collision.CompareTag("Monster"))
        {
            print("Tag is Monster");             // TODO : GameOver
        }
    }

    public void Shoot()
    {
        List<GameObject> Targets = new List<GameObject>(GameObject.FindGameObjectsWithTag("Monster"));
        GameObject Target = null;

        double dist = double.MaxValue;
        foreach (var item in Targets)
        {
            if(Target == null)
            {
                Target = item;
                dist = Vector3.Distance(Target.transform.position, transform.position);
            }
            else
            {
                double itemDist = Vector3.Distance(item.transform.position, transform.position);
                if(itemDist <= dist)
                {
                    Target = item;
                    dist = itemDist;
                }
            }
        }
        

        // Loop
        Bullet = Instantiate(BulletRef);
        Bullet bullet = Bullet.GetComponent<Bullet>();
        bullet.SetBulletInfo(Target);
        

    }

}
