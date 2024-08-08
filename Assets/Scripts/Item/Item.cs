using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour, IHittable
{
    protected Player Player;

    public void OnHit()
    {
        OnUse();
        Destroy(gameObject);
    }

    protected abstract void OnUse();

    public void SetPlayer(Player player)
    {
        Player = player;
    }

}
