using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneshotAnim : MonoBehaviour
{
    private SkeletonAnimation Anim;
    float Delay;
    string Name;

    // Start is called before the first frame update
    void Start()
    {
        Anim = GetComponentInChildren<SkeletonAnimation>();

        if(Anim != null )
        {
            StartCoroutine(IE_PlayAnim());
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator IE_PlayAnim()
    {
        var time = new WaitForSeconds(Delay);

        yield return time;

        Anim.AnimationState.SetAnimation(0, Name, false);


    }

    public void SetTime(float time, string name)
    {
        Delay = time;
        Name = name;
    }
}
