using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObastacleMovement : MonoBehaviour
{
    private float ObstacleMoveSpeed;
    Vector3 rotationDirection = Vector3.zero;

    private void Start()
    {
        ObstacleMoveSpeed = Camera.main.orthographicSize *2 / 5f;
    }

    // Update is called once per frame
    void Update()
    {
            transform.position += new Vector3(0f, 1f, 0f) * ObstacleMoveSpeed * Time.deltaTime;
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
