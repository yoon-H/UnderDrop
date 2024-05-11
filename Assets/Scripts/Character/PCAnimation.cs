using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PCAnimation : MonoBehaviour
{
    public SkeletonAnimation SkeletonAnimation;
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
            //print("targetLoc : " + targetLoc);
            var targetRotation = Mathf.Atan2(Hand.WorldY - targetLoc.y, Hand.WorldX - targetLoc.x);

            //print(targetRotation);

            //print("HAnd : " + Hand.Data.Rotation);
            Hand.Data.Rotation = Mathf.Abs(Hand.Data.Rotation -  targetRotation);

            //Hand.UpdateWorldTransform();



            //var skeletonSpacePoint = SkeletonAnimation.transform.InverseTransformPoint(targetLoc);
            //skeletonSpacePoint.x *= SkeletonAnimation.skeleton.ScaleX;
            //skeletonSpacePoint.y *= SkeletonAnimation.skeleton.ScaleY;

            //Hand.Data.X = skeletonSpacePoint.x;
            //Hand.Data.Y = skeletonSpacePoint.y;

            SkeletonAnimation.AnimationState.SetAnimation(0, "attack", false);

            //SkeletonAnimation.AnimationState.AddAnimation(0, "idle", true, 0f);
            
        }
    }
}
