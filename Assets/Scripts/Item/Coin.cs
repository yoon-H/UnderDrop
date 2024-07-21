using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Item
{
    public Timer Timer;
    protected override void OnUse()
    {
        Timer.CoinCount += 1;
    }

    public void SetTimer(Timer timer)
    {
        Timer = timer;
    }

}
