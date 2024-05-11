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
    public Spine.Bone Hand;

    // Start is called before the first frame update
    void Start()
    {
        if(SkeletonAnimation != null) 
        {
            SkeletonAnimation.AnimationState.SetAnimation(0, "idle", true);
            Hand = SkeletonAnimation.skeleton.FindBone("Arm2");
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
            
            //var skeletonSpacePoint = SkeletonAnimation.transform.InverseTransformPoint(targetLoc);
            //skeletonSpacePoint.x *= SkeletonAnimation.skeleton.ScaleX;
            //skeletonSpacePoint.y *= SkeletonAnimation.skeleton.ScaleY;

            //Hand.Data.X = skeletonSpacePoint.x;
            //Hand.Data.Y = skeletonSpacePoint.y;

            SkeletonAnimation.AnimationState.SetAnimation(0, "attack", false);

            SkeletonAnimation.AnimationState.AddAnimation(0, "idle", true, 0f);
            
        }
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

        print("Play Jump");
    }
}
