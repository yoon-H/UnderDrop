using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour, IHittable
{
    public void OnHit()
    {
        OnUse();
        Destroy(gameObject);
    }

    protected abstract void OnUse();

}
