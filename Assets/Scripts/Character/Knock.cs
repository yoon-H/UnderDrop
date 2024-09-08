using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Knock : Player
{
    #region Character Stat
    private const int _MaxBulletNum = 8;
    private const float _ReloadTime = 1.5f;
    private const float _AttackTime = 0.4f;
    private const int _Damage = 40;
    #endregion

    private float ShieldTime = 0.5f;
    private int ShieldCount = 2;
    private bool IsShield = false;

    public GameObject ShieldRef;

    protected override void Start()
    {
        base.Start();

        MaxBulletNum = _MaxBulletNum;
        ReloadTime = _ReloadTime;
        AttackTime = _AttackTime;
        Damage = _Damage;
        

        SetBulletImage();

        CurBulletNum = MaxBulletNum;

        ShieldRef.SetActive(false);
        BulletSlider.maxValue = MaxBulletNum;
        BulletSlider.value = CurBulletNum;
        BulletText.text = CurBulletNum.ToString();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        IHittable hittable = collision.gameObject.GetComponent<IHittable>();
        if (hittable != null)
        {
            if(collision.gameObject.GetComponentInChildren<Obstacle>()) // When collision object is obstacle type
            {
                if (InvincibleBuff) // When player get Invincible Item
                {
                    return;
                }
                else if(!IsSkillCoolDownStopped && ShieldCount > 0) // When Knock can use shield skill
                {
                    collision.gameObject.GetComponent<Collider2D>().enabled = false;
                    SetIsShield(true);
                    return;
                }
                else
                {
                    hittable.OnHit();
                }
            }
            else if (collision.gameObject.TryGetComponent<InvincibleItem>(out var item))
            {
                if (IsShield)
                {
                    return;
                }
                else
                {
                    item.OnHit();
                }
            }
            else if (collision.gameObject.GetComponentInChildren<Monster>())
            {
                if (InvincibleBuff) // When player get Invincible Item
                {
                    return;
                }
                else
                {
                    hittable.OnHit();
                }
            }
            else
            {
                hittable.OnHit();
            }
        }
    }

    public void SetIsShield(bool flag)
    {
        if (flag && !IsShield)
        {
            IsShield = true;
            ShieldRef.SetActive(true);
            ShieldCount -= 1;
            StartCoroutine(IE_ShieldRemain());
        }
        else if (!flag && IsShield)
        {
            IsShield = false;
            ShieldRef.SetActive(false);
        }
    }

    protected override IEnumerator IE_ReloadBullet()
    {
        GameManager.Instance.PlaySound("norkreloadsound");
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

    private IEnumerator IE_ShieldRemain()
    {
        SetIsShield(true);
        GameManager.Instance.PlaySound("norkskillsound");
        yield return new WaitForSeconds(ShieldTime);
        SetIsShield(false);
    }
}
