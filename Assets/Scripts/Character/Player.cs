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
    private Vector3 LeftLoc = new Vector3(-1.4f,1.7f,0);
    private Vector3 RightLoc = new Vector3(1.4f,1.7f,0);
    private const float JumpTime = 0.15f;
    public Ease ease = Ease.Linear;

    public float PlayerYSize;
    public float PlayerYLoc;

    public GameObject BulletRef;
    public GameObject Bullet;
    GameObject Target = null;
    
    public int CurBulletNum;
    

    //Character Stat
    public int MaxBulletNum = 30;
    public float ReloadTime = 1.5f;
    public float AttackTime = 0.1f;
    public int Damage = 20;



    public GameObject BulletBar;
    private ProgressBar Bar;

    public GameObject TimerRef;
    private Timer Timer;

    [SerializeField]
    private bool CanShoot = false;

    [SerializeField]
    private bool Reloading = false;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        PlayerYSize = transform.localScale.y;
        PlayerYLoc = transform.position.y;

        CurBulletNum = MaxBulletNum;

        Bar = BulletBar.GetComponent<ProgressBar>();
        Timer = TimerRef.GetComponent<Timer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SwitchDir(E_Direction dir)
    {
        if (!transform) return;
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


    protected void OnTriggerEnter2D(Collider2D collision)
    {
        //if(collision.CompareTag("Monster"))
        //{
        //    if (!Timer) return;
        //    Timer.GameOver();

        //}

        IHittable hittable = collision.gameObject.GetComponent<IHittable>();
        if(hittable != null)
        {
            hittable.OnHit();
        }
    
    }

    public bool Shoot()
    {
        if (!Bar) { return false; }
        Bar.SetActiveProgress(true);

        if (!Target)
        {
            bool hasTarget = SearchTarget();
            return hasTarget;
        }
        else
        {
            Bullet = Instantiate(BulletRef);
            if(!Bullet) { return false; }
            Bullet.transform.position = transform.position;
            Bullet bullet = Bullet.GetComponent<Bullet>();
            bullet.SetBulletInfo(Target, Damage);
            UseBullet();
            return true;
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

    protected bool SearchTarget()
    {
        List<GameObject> Targets = new List<GameObject>(GameObject.FindGameObjectsWithTag("Monster"));
        bool flag = false;

        double dist = double.MaxValue;
        foreach (var item in Targets)
        {
            if (!Target)
            {
                Target = item;
                dist = Vector3.Distance(Target.transform.position, transform.position);
                flag = true;
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

        return flag;
    }

    public void CancelTarget()
    {
        Target = null;
    }

    protected void UseBullet()
    {
        CurBulletNum -= 1;

        if (!Bar) { return; }
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
            yield return new WaitForSeconds(AttackTime);
            bool flag = Shoot();
            if(!flag) break;
        }
        
    }

    IEnumerator IE_ReloadBullet()
    {
        CancelTarget();
        yield return new WaitForSeconds(ReloadTime);

        Reloading = false;
        if (!Bar) { yield break; }
        Bar.SetActiveProgress(true);
        Bar.Value = MaxBulletNum;
        CurBulletNum = MaxBulletNum;

        if(CanShoot) 
        {
            StartCoroutine(IE_ShootBullet());
        }

    }

}
