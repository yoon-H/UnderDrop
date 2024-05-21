using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
using static UnityEngine.GraphicsBuffer;
using System;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    private E_Direction Dir = E_Direction.Right;
    private Vector3 LeftLoc = new Vector3(-1.98f,1.7f,0);
    private Vector3 RightLoc = new Vector3(1.98f,1.7f,0);
    private const float JumpTime = 0.07f;
    public Ease ease = Ease.Linear;

    public float PlayerYSize;
    public float PlayerYLoc;

    public GameObject BulletRef;
    public GameObject Bullet;
    GameObject Target = null;
    
    public int CurBulletNum;

    //Animation
    protected PCAnimation AnimationRef;

    //Character Stat
    public int MaxBulletNum = 30;
    public float ReloadTime = 1.5f;
    public float AttackTime = 0.1f;
    public int Damage = 20;

    public float ReloadWaitingTime = 1f;

    //public GameObject BulletPanel;
    public Slider BulletSlider;

    public GameObject BulletCount;
    protected Text BulletText;


    //public GameObject TimerRef;
    private Timer Timer;

    [SerializeField]
    protected bool CanShoot = false;

    [SerializeField]
    protected bool Reloading = false;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        PlayerYSize = transform.localScale.y;
        PlayerYLoc = transform.position.y;

        CurBulletNum = MaxBulletNum;

        if (!AnimationRef)
            AnimationRef = GetComponent<PCAnimation>();
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

            if (AnimationRef != null)
                AnimationRef.PlayJumpAnim();
            Flip();
        }
        else if (dir == E_Direction.Right && Dir == E_Direction.Left)
        {
            Dir=E_Direction.Right;
            transform.DOMoveX(RightLoc.x, JumpTime).SetEase(ease);

            if (AnimationRef != null)
                AnimationRef.PlayJumpAnim();
            Flip();
        }


    }

    public void Flip()
    {
        Vector3 vec = transform.localScale;
        vec.x = -transform.localScale.x;
        transform.localScale = vec;
    }


    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        IHittable hittable = collision.gameObject.GetComponent<IHittable>();
        if(hittable != null)
        {
            hittable.OnHit();
        }
    
    }

    public bool Shoot()
    {

        if (!Target)
        {
            bool hasTarget = SearchTarget();
            return hasTarget;
        }
        else
        {
            //Animation
            if (AnimationRef != null)
                AnimationRef.PlayAttackAnim(Target.transform.position);

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
            if(CurBulletNum > 0)
            {
                StopCoroutine(IE_ReloadBullet());
                StopCoroutine(IE_WaitForReloading());

                StartCoroutine(IE_ShootBullet());

            }
        }
        else if(!flag && CanShoot != flag)
        {
            CanShoot = false;

            StopCoroutine(IE_ShootBullet());
            CancelTarget();
            
            if(CurBulletNum < MaxBulletNum)
            {
                StartCoroutine(IE_WaitForReloading());
            }
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

        if(!BulletText) { return; }
        BulletText.text = CurBulletNum.ToString();
        BulletSlider.value = CurBulletNum;

        if(CurBulletNum <=0 )
        {
            Reloading = true;

            StopCoroutine(IE_ShootBullet());
            StartCoroutine(IE_ReloadBullet());
        }
    }

    protected IEnumerator IE_ShootBullet()
    {
        while(CanShoot && !Reloading)
        {
            yield return new WaitForSeconds(AttackTime);
            bool flag = Shoot();
            if(!flag) break;
        }

        if (AnimationRef)
            AnimationRef.PlayIdleAnim();
        
    }

    protected virtual IEnumerator IE_ReloadBullet()
    {
        CancelTarget();
        yield return new WaitForSeconds(ReloadTime);
        Reloading = false;

        CurBulletNum = MaxBulletNum;

        if (!BulletText) { yield break; }
        BulletText.text = CurBulletNum.ToString();
        BulletSlider.value = CurBulletNum;

        if (CanShoot) 
        {
            StartCoroutine(IE_ShootBullet());
        }

    }

    IEnumerator IE_WaitForReloading()
    {
        yield return new WaitForSeconds(ReloadWaitingTime);
        StartCoroutine(IE_ReloadBullet());
    }


    public void SetPCInfo(GameObject timer, GameObject bulletPanel)
    {
        Timer = timer.GetComponent<Timer>();

        BulletText = bulletPanel.GetComponentInChildren<Text>();
        BulletSlider = bulletPanel.GetComponentInChildren<Slider>();
        if(BulletSlider)
        {
            BulletSlider.value = CurBulletNum;
        }
       
    }

}
