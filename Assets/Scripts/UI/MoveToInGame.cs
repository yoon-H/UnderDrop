using Spine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToInGame : PlayAnim
{
    public ChangeScene Change;

    protected override void Event(TrackEntry entry)
    {
        Change.MoveToInGameScene();
    }
}
