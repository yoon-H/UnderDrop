using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CardObstacle : Obstacle
{
    bool IsExpending = false;
    BoxCollider2D Collider;
    SkeletonAnimation Animation;

    const float MaxOffsetX = 1.1f;
    const float MaxSizeX = 2.2f;

    float OffSetAmount = -1f;
    float SizeAmount = -1f;

    const float ExpendTime = 3f;

    // Start is called before the first frame update
    void Start()
    {
        Animation = GetComponentInChildren<SkeletonAnimation>();

        if(Animation != null )
        {
            float timescale = Animation.Skeleton.Data.FindAnimation("motion1").Duration / ExpendTime;

            Animation.AnimationState.SetAnimation(0, "motion1", false).TimeScale = timescale;    // 1 second animation to 3 seconds

            Collider = GetComponentInChildren<BoxCollider2D>();

            if(Collider != null)
            {
                OffSetAmount = (MaxOffsetX - Collider.offset.x) / ExpendTime;
                SizeAmount = (MaxSizeX - Collider.size.x) / ExpendTime;

                IsExpending = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(IsExpending)
        {
            Animation.Update(Time.deltaTime);

            float offsetX = Collider.offset.x;
            float sizeX = Collider.size.x;

            if(offsetX < MaxOffsetX && sizeX < MaxSizeX)
            {
                offsetX += OffSetAmount * Time.deltaTime;
                sizeX += SizeAmount * Time.deltaTime;

                Collider.offset = new Vector2(offsetX, Collider.offset.y);
                Collider.size = new Vector2(sizeX, Collider.size.y);
            }
            else
            {
                IsExpending = false;
            }
            
        }
    }

}
