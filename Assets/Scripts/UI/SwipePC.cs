using Spine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipePC : PlayAnim
{
    public CharacterInfo Info;
    public int Amount;

    protected override void Event(TrackEntry entry)
    {
        Info.AddIndex(Amount);
    }
}
