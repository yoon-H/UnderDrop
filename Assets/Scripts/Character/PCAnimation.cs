using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PCAnimation : MonoBehaviour
{
    public SkeletonAnimation SkeletonAnimation;
    public GameObject Object;

    public SkeletonAnimation JumpSkeletonAnimation;
    public GameObject JumpObject;
    public Spine.Bone Arm;
    float InitRotation =  720f;

    public Timer Timer;
    bool UnscaledTime = false;

    // Start is called before the first frame update
    void Start()
    {
        if (SkeletonAnimation != null)
        {
            Arm = SkeletonAnimation.skeleton.FindBone("Arm2");
            if (InitRotation == 720f)
            {
                InitRotation = Arm.Data.Rotation;
            }
            Arm.Data.Rotation = InitRotation;

            SkeletonAnimation.AnimationState.SetAnimation(0, "idle", true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(UnscaledTime)
            SkeletonAnimation.Update(Time.unscaledDeltaTime);
        else
            SkeletonAnimation.Update(Time.deltaTime);
    }

    public void PlayAttackAnim(Vector3 targetLoc)
    {
        if (SkeletonAnimation != null)
        {

            var targetRotation = Vector2.Angle(Vector2.up, targetLoc - transform.position);
            Arm.Data.Rotation = targetRotation;

            if(SkeletonAnimation.AnimationName != "attack2")
                SkeletonAnimation.AnimationState.SetAnimation(0, "attack2", true);

            //Spine.TrackEntry trackEntry = SkeletonAnimation.AnimationState.SetAnimation(0, "attack2", false);
            //trackEntry.End += EndEvent;

        }
    }

    public void PlayIdleAnim()
    {
        Arm.Data.Rotation = InitRotation;
        if(SkeletonAnimation)
            SkeletonAnimation.AnimationState.SetAnimation(0, "idle", true);
    }

    public void PlayJumpAnim()
    {
        StartCoroutine(IE_PlayJumpAnim());
    }

    public IEnumerator IE_PlayJumpAnim()
    {
        if (Object != null) { Object.SetActive(false); }
        if (JumpObject != null) { JumpObject.SetActive(true); }

        if (JumpSkeletonAnimation != null)
        {
            JumpSkeletonAnimation.AnimationState.SetAnimation(0, "jumpS", false);
        }

        yield return new WaitForSeconds(0.07f);

        if (JumpObject != null) { JumpObject.SetActive(false); }
        if (Object != null) { Object.SetActive(true); }
        if (SkeletonAnimation != null)
        {
            SkeletonAnimation.AnimationState.SetAnimation(0, "P", false);

            SkeletonAnimation.AnimationState.AddAnimation(0, "idle", true, 0f);
        }
    }

    public void PlayDeadAnim()
    {
        UnscaledTime = true;
        if (SkeletonAnimation != null)
        {
            Spine.TrackEntry trackEntry = SkeletonAnimation.AnimationState.SetAnimation(0, "die", false);
            trackEntry.Complete += EndEvent;
        }
    }

    public void EndEvent(TrackEntry trackEntry)
    {
        gameObject.SetActive(false);
        Timer.EndTask();
        UnscaledTime = false;
        Destroy(gameObject);
    }
}
