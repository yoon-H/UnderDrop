using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibleItem : Item
{
    private const float BuffTime = 2f;
    protected override void OnUse()
    {
        Player.SetInvincibleBuff(BuffTime);
        GameManager.Instance.PlaySound("itemsound");
    }
}
