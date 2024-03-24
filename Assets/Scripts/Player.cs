using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
using static UnityEngine.GraphicsBuffer;

public class Player : MonoBehaviour
{
    private E_Direction Dir = E_Direction.Right;
    private Vector3 LeftLoc = new Vector3(-2,3,0);
    private Vector3 RightLoc = new Vector3(2,3,0);
    public Ease ease = Ease.Linear;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Jump()
    {
        SwitchDir();
    }

    private void SwitchDir()
    {
        if (Dir == E_Direction.Right) 
        {
            Dir= E_Direction.Left;
            transform.DOMoveX(LeftLoc.x, 0.15f).SetEase(ease); 
        }
        else
        {
            Dir=E_Direction.Right;
            transform.DOMoveX(RightLoc.x, 0.15f).SetEase(ease);
        }
    }

}
