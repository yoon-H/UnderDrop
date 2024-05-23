using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{

    private float ObjectMoveSpeed;

    [SerializeField]
    private float TimeForArrival;

    // Start is called before the first frame update
    void Start()
    {
        ObjectMoveSpeed = Camera.main.orthographicSize * 2 / TimeForArrival;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0f, 1f, 0f) * ObjectMoveSpeed * Time.deltaTime;
    }

    public void SetTimeForArrival(float value)
    {
        TimeForArrival = value;
    }
}
