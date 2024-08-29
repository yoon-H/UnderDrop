using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinDoubleItem : Item
{
    public Timer Timer;

    private const float BuffTime = 10f;
    protected override void OnUse()
    {
        Timer.SetCoinDoubleBuff(BuffTime);
        GameManager.Instance.PlaySound("itemsound");
    }

    public void SetTimer(Timer timer)
    {
        Timer = timer;
    }
}
