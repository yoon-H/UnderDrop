using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAnimation : MonoBehaviour
{
    public SkeletonAnimation SkAnim;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        SkAnim.AnimationState.SetAnimation(0, "id", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
