using Spine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpAnim : PlayAnim
{
    public GameObject Objects;

    private void OnEnable()
    {
        Objects.SetActive(false);
    }

    private void OnDisable()
    {
        Objects.SetActive(false);
        Anim.Initialize(true);
    }

    protected override void Event(TrackEntry entry)
    {
        Objects.SetActive(true);
    }
}
