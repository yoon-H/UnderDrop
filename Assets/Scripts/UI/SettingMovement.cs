using DG.Tweening;
using Spine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingMovement : MoveObject
{
    public float WaitingTime = 0.1f;
    public bool IsInMain = false;

    private void Update()
    {
        if(IsInMain)
        {
            Anim.Update(Time.deltaTime);
        }
        else
        {
            Anim.Update(Time.unscaledDeltaTime);
        }
    }

    public override void Move()
    {
        if (Rect != null)
        {
            Anim.AnimationState.TimeScale = 0f;
            Rect.DOAnchorPosX(EndPosition, moveTime).SetUpdate(true).SetEase(Ease.OutQuart).OnComplete(() => StartCoroutine(IE_WaitForAnim()));
        }
    }

    IEnumerator IE_WaitForAnim()
    {
        var time = new WaitForSecondsRealtime(WaitingTime);
        yield return time;

        PlayAnim();
    }

    protected override void PlayAnim()
    {
        Anim.AnimationState.TimeScale = 1f;
        base.PlayAnim();
    }
}
