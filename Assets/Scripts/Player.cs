using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
using static UnityEngine.GraphicsBuffer;

public class Player : MonoBehaviour
{

    private E_Direction Dir = E_Direction.Right;
    private Vector3 LeftLoc = new Vector3(-1.6f,3,0);
    private Vector3 RightLoc = new Vector3(1.6f,3,0);
    private const float JumpTime = 0.15f;
    public Ease ease = Ease.Linear;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Jump(bool isLeft)
    {
        E_Direction dir = isLeft ? E_Direction.Left : E_Direction.Right;
        SwitchDir(dir);
    }

    private void SwitchDir(E_Direction dir)
    {
        if (dir == E_Direction.Left && Dir == E_Direction.Right) 
        {
            Dir= E_Direction.Left;
            transform.DOMoveX(LeftLoc.x, JumpTime).SetEase(ease);
        }
        else if (dir == E_Direction.Right && Dir == E_Direction.Left)
        {
            Dir=E_Direction.Right;
            transform.DOMoveX(RightLoc.x, JumpTime).SetEase(ease);
        }
    }

}
