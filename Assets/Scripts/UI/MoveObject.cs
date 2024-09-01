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
    public float moveTime = 1f;

    public SkeletonGraphic Anim;
    public GameObject Objects;

    protected RectTransform Rect;

    private void Awake()
    {
        Rect = GetComponent<RectTransform>();
        HideObjects();
    }

    private void OnDisable()
    {
        HideObjects();
    }
    
    public virtual void Move()
    {
        if (Rect != null)
        {
            Rect.DOAnchorPosX(EndPosition, moveTime).SetEase(Ease.OutQuart).OnComplete(() => PlayAnim()).SetUpdate(true);
        }
       
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

    protected virtual void PlayAnim()
    {
        var entry = Anim.AnimationState.SetAnimation(0, "animation", false);
        entry.Complete += Event;
    }

    protected void Event(TrackEntry entry)
    {
        Objects.SetActive(true);
    }

    public void HideObjects()
    {
        Remove();
        Objects.SetActive(false);
    }
}
