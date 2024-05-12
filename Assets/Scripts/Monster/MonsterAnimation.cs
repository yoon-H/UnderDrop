using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class MonsterAnimation : MonoBehaviour
{
    public GameObject ParentObject;
    public GameObject Object;
    public SkeletonAnimation SkeletonAnimation;
    public GameObject DeadObject;
    public SkeletonAnimation DeadSkeleton;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        if (SkeletonAnimation != null)
        {
            SkeletonAnimation.AnimationState.SetAnimation(0, "idle", true);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayTakeDamageAnimation()
    {
        if (SkeletonAnimation != null)
        {

            Spine.TrackEntry trackEntry = SkeletonAnimation.AnimationState.SetAnimation(0, "Taking Damage", false);
            trackEntry.End += DamageEndEvent;

        }
    }

    public void DamageEndEvent(TrackEntry trackEntry)
    {
        SkeletonAnimation.AnimationState.SetAnimation(0, "idle", true);
    }

    public void PlayDieAnimation()
    {
        if(Object != null) { Object.SetActive(false); }

        if(DeadObject != null) { DeadObject.SetActive(true); }

        if (DeadSkeleton != null && DeadSkeleton.AnimationName != "die")
        {
            Spine.TrackEntry trackEntry = DeadSkeleton.AnimationState.SetAnimation(0, "die", false);
            trackEntry.Complete += DeadEvent;
        }

       
    }

    public void DeadEvent(TrackEntry trackEntry)
    {
        Destroy(ParentObject);
    }



}
