using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private E_Direction Dir = E_Direction.Right;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Jump()
    {
        ChangeDir();
    }

    void ChangeDir()
    {
        if (Dir == E_Direction.Right) 
        {
            Dir= E_Direction.Left;
        }
        else
        {
            Dir=E_Direction.Right;
        }
    }
}
