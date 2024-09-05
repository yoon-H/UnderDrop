using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayProfileAnim : MonoBehaviour
{
    public SkeletonAnimation Anim;

    private void OnEnable()
    {
        if(GameManager.Instance)
        {
            SetProfile(GameManager.Instance.PlayerIndex);
        }
        
        PlayAnim();
    }

    public void PlayAnim()
    {
        Anim.AnimationState.SetAnimation(0, "animation", false);
    }

    public void SetProfile(int idx)
    {
        if(idx == 0)
        {
            Anim.Skeleton.SetSkin("Noke");
        }
        else
        {
            Anim.Skeleton.SetSkin("mont");
        }
    }
}
