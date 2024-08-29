using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayProfileAnim : MonoBehaviour
{
    public SkeletonAnimation Anim;


    public void PlayAnim()
    {
        var anim = Anim.GetComponent<SkeletonAnimation>();

        anim.AnimationState.SetAnimation(0, "animation", false);
    }

    public void SetProfile(int idx)
    {
        if(idx == 0)
        {
            // TODO : noke
        }
        else
        {
            // TODO : mont
        }
    }
}
