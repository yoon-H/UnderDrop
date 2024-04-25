using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knock : Player
{
    //Character Stat
    public int _MaxBulletNum = 30;
    public float _ReloadTime = 1.2f;
    public float _AttackTime = 0.05f;
    public int _Damage = 7;

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

        Shield = ShieldRef.GetComponent<Shield>();
        SetIsShield(false);
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
        CancelTarget();
        SpawnShield();
        yield return new WaitForSeconds(ReloadTime);

        Reloading = false;
        if(IsShield)
        {
            SetIsShield(false);
        }
        if (!Bar) { yield break; }
        Bar.SetActiveProgress(true);
        Bar.Value = MaxBulletNum;
        CurBulletNum = MaxBulletNum;

        if (CanShoot)
        {
            StartCoroutine(IE_ShootBullet());
        }

    }

    private void SpawnShield()
    {
        System.Random rand = new System.Random();
        int res = 0;
            //rand.Next(2);
        if(res == 0)
        {
            SetIsShield(true);
            print("zzzzzzzzzzzzzzz");
        }
    }
}
