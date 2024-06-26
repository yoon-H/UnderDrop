using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knock : Player
{
    //Character Stat
    private int _MaxBulletNum = 8;
    private float _ReloadTime = 1.5f;
    private float _AttackTime = 0.4f;
    private int _Damage = 40;

    private float ShieldTime = 2f;
    private bool IsShield = false;

    public GameObject ShieldRef;
    private Shield Shield;

    protected override void Start()
    {
        base.Start();

        MaxBulletNum = _MaxBulletNum;
        ReloadTime = _ReloadTime;
        AttackTime = _AttackTime;
        Damage = _Damage;

        CurBulletNum = MaxBulletNum;

        Shield = ShieldRef.GetComponent<Shield>();
        Shield.SetKnock(this);
        SetIsShield(false);
        BulletSlider.maxValue = MaxBulletNum;
        BulletSlider.value = CurBulletNum;
        BulletText.text = CurBulletNum.ToString();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        IHittable hittable = collision.gameObject.GetComponent<IHittable>();
        if (hittable != null)
        {
            if (IsShield && collision.CompareTag("Obstacle") )
            {
                return;
            }
            else
            {
                if (!collision.gameObject.GetComponent<Collider2D>().isActiveAndEnabled)
                {
                    return;
                }

                hittable.OnHit();
            }
            
        }
    }

    public void SetIsShield(bool flag)
    {
        if(flag)
        {
            IsShield = true;
            ShieldRef.SetActive(true);
        }
        else
        {
            IsShield = false;
            ShieldRef.SetActive(false);
        }
    }

    protected override IEnumerator IE_ReloadBullet()
    {
        GameManager.Instance.PlaySound("norkreloadsound");
        CancelTarget();
        SpawnShield();
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

    private void SpawnShield()
    {
        System.Random rand = new System.Random();
        int res = rand.Next(2);
        if(res == 0)
        {
            StartCoroutine(IE_ShieldRemain());
        }
    }

    private IEnumerator IE_ShieldRemain()
    {
        SetIsShield(true);
        GameManager.Instance.PlaySound("norkskillsound");
        yield return new WaitForSeconds(ShieldTime);
        SetIsShield(false);
    }
}
