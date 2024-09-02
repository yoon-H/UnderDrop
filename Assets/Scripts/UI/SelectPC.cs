using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPC : PlayAnim
{
    public CharacterInfo Info;
    public SwitchPopUp PopUp;
    public SkeletonAnimation CharacterImage;
    protected override void Event(TrackEntry entry)
    {
        Info.SelectPC();
        PopUp.SwitchFlag(false);

        if (GameManager.Instance.PlayerIndex == 0)
        {
            CharacterImage.Skeleton.SetSkin("Noke");
            
        }
        else
        {
            CharacterImage.Skeleton.SetSkin("mont");
        }

        CharacterImage.Skeleton.SetSlotsToSetupPose();
        CharacterImage.LateUpdate();
        CharacterImage.AnimationState.ClearTracks();

        CharacterImage.AnimationState.SetAnimation(0, "animation", false);

    }

}
