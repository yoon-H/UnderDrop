using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static UnityEngine.GridBrushBase;

public class DartObstacle : Obstacle
{
    public float CountTime = 1f;
    public float RotateTime = 1.5f;
    public float MoveTime = 0.1f;

    Vector3 RotateDirection = new Vector3(0, 0, 2f);

    private bool IsRotating = false;

    E_Direction Dir;

    float LeftLocX = - 1.4f;
    float RightLocX = 1.4f;

    public Ease ease = Ease.Linear;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(IE_Count());
    }

    // Update is called once per frame
    void Update()
    {
        if (IsRotating) transform.Rotate(RotateDirection);
    }

    IEnumerator IE_Count()
    {
        while(true)
        {
            System.Random rand = new System.Random();
            int res = rand.Next(2);
            if (res == 0)
            {
                IsRotating = true;
                break;
            }
            yield return new WaitForSeconds(CountTime);
        }

        StartCoroutine(IE_Rotate());
    }

    IEnumerator IE_Rotate()
    {
        yield return new WaitForSeconds(RotateTime);
        IsRotating=false;
        MoveToOtherSide();
        StartCoroutine(IE_Count());
    }

    private void MoveToOtherSide()
    {
        if(Dir == E_Direction.Left)
        {
            transform.DOMoveX(RightLocX, MoveTime).SetEase(ease);
            Dir = E_Direction.Right;
        }
        else
        {
            transform.DOMoveX(LeftLocX, MoveTime).SetEase(ease);
            Dir = E_Direction.Left;
        }
    }

    public void SetDirection(E_Direction dir)
    {
        Dir = dir;
    }
}
