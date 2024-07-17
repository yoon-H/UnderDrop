using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaselMonsterAnimation : MonsterAnimation
{
    // Start is called before the first frame update
    protected override void Start()
    {
      base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayWarningAnimation()
    {
        if (SkeletonAnimation != null)
        {

            SkeletonAnimation.AnimationState.SetAnimation(0, "casting", false);
            SkeletonAnimation.AnimationState.AddAnimation(0, "idle", true, 0);

        }
    }
}
