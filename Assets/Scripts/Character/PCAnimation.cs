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
    float InitRotation;

    // Start is called before the first frame update
    void Start()
    {
        if (SkeletonAnimation != null)
        {
            Arm = SkeletonAnimation.skeleton.FindBone("Arm2");

            if (InitRotation == 0)
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
        
    }

    public void PlayAttackAnim(Vector3 targetLoc)
    {
        if (SkeletonAnimation != null)
        {

            var targetRotation = Vector2.Angle(Vector2.up, targetLoc - transform.position);
            Arm.Data.Rotation = targetRotation;

            Spine.TrackEntry trackEntry = SkeletonAnimation.AnimationState.SetAnimation(0, "attack", false);
            trackEntry.End += EndEvent;

        }
    }

    public void EndEvent(TrackEntry trackEntry)
    {
        Arm.Data.Rotation = InitRotation;
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
}
