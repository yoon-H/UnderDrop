using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDirection : MonoBehaviour
{
    E_Direction Direction;

    public float RotationValue = 90f;
    public bool FlipWhenLeftDirection;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDirection(E_Direction direction)
    {
        Direction = direction;

        if (Direction == E_Direction.Left)
        {
            transform.Rotate(new Vector3(0, 0, -RotationValue));

            if (FlipWhenLeftDirection)
            {
                SpriteRenderer renderer = GetComponentInChildren<SpriteRenderer>();
                if (!renderer) return;
                renderer.flipX = true;
            }
        }
        else
        {
            transform.Rotate(new Vector3(0, 0, RotationValue));
        }
    }
}
