using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnim : MonoBehaviour
{
    public SkeletonGraphic Anim;

    public void Play()
    {
        var track = Anim.AnimationState.SetAnimation(0, "animation", false);
        track.Complete += Event;
        
    }

    protected virtual void Event(TrackEntry entry)
    {

    }
}
