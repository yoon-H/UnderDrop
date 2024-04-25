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

    protected override void Start()
    {
        base.Start();

        MaxBulletNum = _MaxBulletNum;
        ReloadTime = _ReloadTime;
        AttackTime = _AttackTime;
        Damage = _Damage;
    }

}
