using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BambooObstacle : Obstacle
{
    private bool hasCollider;   // If true collider will be active.
    private const float Time = 1f;

    private Collider2D Collider;

    private SkeletonAnimation Animation;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(IE_ChangeShape());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator IE_ChangeShape()
    {
        yield return Time;
        
        if(Collider != null) 
        {
            if (hasCollider)
            {
                Collider.enabled = true;
                Animation.AnimationState.SetAnimation(0, "UP", false);
            }
            else
            {
                Collider.enabled = false;
            }
        }

        
    }

    public void SetCollider(bool flag)
    {
        if(flag)
        {
            hasCollider = true;
        }
        else
        {
            hasCollider=false;
        }
        
        if(TryGetComponent<Collider2D>(out Collider))
        {
            Collider.enabled = !hasCollider;
        }

        Animation = GetComponent<SkeletonAnimation>();

    }
}
