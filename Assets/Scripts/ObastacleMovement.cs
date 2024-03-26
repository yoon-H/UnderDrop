using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObastacleMovement : MonoBehaviour
{
    private float ObstacleMoveSpeed;


    private void Start()
    {
        ObstacleMoveSpeed = Camera.main.orthographicSize * 2 / 5f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0f, 1f, 0f) * ObstacleMoveSpeed * Time.deltaTime;
        transform.Rotate(new Vector3(0f , 0f, -2f));
    }
}
