using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawTooth : Obstacle
{
    Vector3 rotationDirection = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationDirection);
    }

    public void SetRotateDirection(E_Direction dir)
    {
        if (dir == E_Direction.Left)
        {
            rotationDirection = new Vector3(0, 0, 2f);
        }
        else
        {
            rotationDirection = new Vector3(0, 0, -2f);
        }
    }
}
