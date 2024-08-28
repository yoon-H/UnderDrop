using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Item
{
    public Timer Timer;
    protected override void OnUse()
    {
        Timer.AddCoin(1);
        GameManager.Instance.PlaySound("coinsound");
    }

    public void SetTimer(Timer timer)
    {
        Timer = timer;
    }

}
