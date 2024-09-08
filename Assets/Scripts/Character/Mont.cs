using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Mont : Player
{
    #region Character Stat
    private const int _MaxBulletNum = 7;
    private const float _ReloadTime = 0.5f;
    private const float _AttackTime = 0.5f;
    private const int _Damage = 30;
    #endregion


    private const int MaxSkillCount = 7;
    private int CurSkillCount = 0;

    public Transform FireLocation;
    public GameObject SkillFlare;
    public GameObject SkillBullet;

    private bool ArkSkill = false;
    private const float ArkTime = 3f;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        MaxBulletNum = _MaxBulletNum;
        ReloadTime = _ReloadTime;
        AttackTime = _AttackTime;
        Damage = _Damage;
        
        SetBulletImage();

        CurBulletNum = MaxBulletNum;

        BulletSlider.maxValue = MaxBulletNum;
        BulletSlider.value = CurBulletNum;
        BulletText.text = CurBulletNum.ToString();
    }


    public override bool Shoot()
    {
        if (!Target)
        {
            bool flag = SearchTarget();
            if (!flag) { return false; }
        }

        //Animation
        if (AnimationRef != null)
            AnimationRef.PlayAttackAnim(Target.transform.position);

        if(!IsSkillCoolDownStopped)
        {
            CurSkillCount += 1;
        }
       
        GameObject flarePrefab;
        GameObject bulletPrefab;
        

        if (CurSkillCount >= MaxSkillCount)
        {
            CurSkillCount = 0;
            flarePrefab = SkillFlare;
            bulletPrefab = SkillBullet;

            StartCoroutine(IE_Ark());
        }
        else
        {
            flarePrefab = MuzzleFlareRef;
            bulletPrefab = BulletRef;
        }

        //Flame
        GameObject flame = Instantiate(flarePrefab, FireLocation.position, FireLocation.rotation);
        if (!flame) { return false; }
        flame.transform.right = Target.transform.position - FireLocation.position;

        //Bullet
        Bullet = Instantiate(bulletPrefab, FireLocation.position, FireLocation.rotation);
        if (!Bullet) { return false; }
        Bullet bullet = Bullet.GetComponent<Bullet>();
        bullet.SetBulletInfo(Target, CurDamage);
        UseBullet();

        GameManager.Instance.PlaySound("norkshootsound");
        return true;

    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        IHittable hittable = collision.gameObject.GetComponent<IHittable>();
        if (hittable != null)
        {
            if (collision.gameObject.GetComponentInChildren<Obstacle>()) // When collision object is obstacle type
            {
                if (InvincibleBuff) // When player get Invincible Item
                {
                    return;
                }
                else if (ArkSkill) // When ArkSkill is true
                {
                    collision.gameObject.GetComponent<Collider2D>().enabled = false;
                    return;
                }
                else
                {
                    hittable.OnHit();
                }
            }
            else if (collision.gameObject.TryGetComponent<InvincibleItem>(out var item))
            {
                if (ArkSkill)
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

    IEnumerator IE_Ark()
    {
        ArkSkill = true;

        WaitForSeconds time = new WaitForSeconds(ArkTime);

        yield return time;

        ArkSkill = false;
        
    }

}
