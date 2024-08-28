using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDoubleItem : Item
{
    private const float BuffTime = 5f;
    protected override void OnUse()
    {
        Player.SetDamageDoubleBuff(BuffTime);
        GameManager.Instance.PlaySound("itemsound");
    }
}
