using DG.Tweening;
using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public float StartPosition;
    public float EndPosition;
    public float Time = 1f;

    public SkeletonGraphic Anim;
    public GameObject Objects;

    private RectTransform Rect;

    private void Start()
    {
        Rect = GetComponent<RectTransform>();
        HideObjects();
    }

    public void Move()
    {
        if(Rect != null)
            Rect.DOAnchorPosX(EndPosition, Time).SetEase(Ease.OutQuart).OnComplete(() => PlayAnim());
    }

    private void Remove()
    {
        if (Rect != null)
        {
            var rect = Rect.anchoredPosition;
            rect.x = StartPosition;

            Rect.anchoredPosition = rect;

            Anim.Initialize(true);
        }
            
    }

    private void PlayAnim()
    {
        Anim.AnimationState.ClearTrack(0);
        Anim.Skeleton.SetToSetupPose();
        var entry = Anim.AnimationState.SetAnimation(0, "animation", false);
        entry.Complete += Event;
    }

    private void Event(TrackEntry entry)
    {
        Objects.SetActive(true);
    }

    public void HideObjects()
    {
        Remove();
        Objects.SetActive(false);
    }
}
