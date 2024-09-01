using Spine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToMainAnim : PlayAnim
{
    public ChangeScene Change;
    protected override void Event(TrackEntry entry)
    {
        base.Event(entry);

        Change.MoveToMainScene();

    }
}
