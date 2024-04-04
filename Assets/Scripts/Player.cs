using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
using static UnityEngine.GraphicsBuffer;
using System;

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
    GameObject Target = null;
    public int MaxBulletNum = 30;
    public int CurBulletNum;
    public float ReloadTime = 1.5f;

    public GameObject BulletBar;
    private ProgressBar Bar;

    [SerializeField]
    private bool CanShoot = false;

    [SerializeField]
    private bool Reloading = false;

    // Start is called before the first frame update
    void Start()
    {
        PlayerYSize = transform.localScale.y;
        PlayerYLoc = transform.position.y;

        CurBulletNum = MaxBulletNum;

        Bar = GetComponent<ProgressBar>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SwitchDir(E_Direction dir)
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
            //print("Tag is Obstacle");             // TODO : GameOver
        }

        if(collision.CompareTag("Monster"))
        {
            //print("Tag is Monster");             // TODO : GameOver
        }
    }

    public void Shoot()
    {
        if(Target == null)
        {
            SearchTarget();
        }

        if (Bar == null)
        {
            Bar = BulletBar.GetComponent<ProgressBar>();
        }
        Bar.SetActiveProgress(true);

        if(Target != null)
        {
            Bullet = Instantiate(BulletRef);
            Bullet.transform.position = transform.position;
            Bullet bullet = Bullet.GetComponent<Bullet>();
            bullet.SetBulletInfo(Target);
            UseBullet();
        }
        
    }
    public void SetCanShoot(bool flag)
    {
        if(flag && CanShoot != flag)
        {
            CanShoot = true;
            if(!Reloading)
            {
                StartCoroutine(IE_ShootBullet());
            }
        }
        else if(!flag && CanShoot != flag)
        {
            CanShoot = false;

            StopCoroutine(IE_ShootBullet());
            CancelTarget();
        }
    }

    private void SearchTarget()
    {
        List<GameObject> Targets = new List<GameObject>(GameObject.FindGameObjectsWithTag("Monster"));


        double dist = double.MaxValue;
        foreach (var item in Targets)
        {
            if (Target == null)
            {
                Target = item;
                dist = Vector3.Distance(Target.transform.position, transform.position);
            }
            else
            {
                double itemDist = Vector3.Distance(item.transform.position, transform.position);
                if (itemDist <= dist)
                {
                    Target = item;
                    dist = itemDist;
                }
            }
        }
    }

    public void CancelTarget()
    {
        Target = null;
    }

    private void UseBullet()
    {
        CurBulletNum -= 1;

        Bar.Value = CurBulletNum;

        if(CurBulletNum <=0 )
        {
            Reloading = true;

            Bar.SetActiveProgress(false);

            StopCoroutine(IE_ShootBullet());
            StartCoroutine(IE_ReloadBullet());
        }
    }

    IEnumerator IE_ShootBullet()
    {
        while(CanShoot && !Reloading)
        {
            yield return new WaitForSeconds(0.1f);
            Shoot();
        }
        
    }

    IEnumerator IE_ReloadBullet()
    {
        yield return new WaitForSeconds(ReloadTime);

        Reloading = false;
        Bar.SetActiveProgress(true);
        Bar.Value = MaxBulletNum;
        CurBulletNum = MaxBulletNum;

        if(CanShoot) 
        {
            StartCoroutine(IE_ShootBullet());
        }

    }

}
