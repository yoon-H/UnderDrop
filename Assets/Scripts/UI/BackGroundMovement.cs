using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class BackGroundMovement : MonoBehaviour
{
    //Movement
    private float MoveSpeed;
    private float CameraSize;

    public float MaxTimeForArrival = 3f;
    public float CurTimeForArrival;
    public float MinTimeForArrival = 2.4f;
    public float TimeForArrivalReducingAmount = 0.2f;

    //Scrolling
    public int StartIndex;
    public int EndIndex;
    public Transform[] Sprites;
    public float MaxYLoc = 60f;
    public float YSize = 0.888f;


    // Start is called before the first frame update
    void Start()
    {
        CurTimeForArrival = MaxTimeForArrival;
        CameraSize = Camera.main.orthographicSize *2 ;

    }

    // Update is called once per frame
    void Update()
    {
        MoveSpeed = CameraSize / CurTimeForArrival;
        transform.position += new Vector3(0f, 1f, 0f) * MoveSpeed * Time.deltaTime;

        if (Sprites[StartIndex].position.y > 30f )
        {
            Vector3 UpSpritePos = Sprites[StartIndex].localPosition;
            Vector3 DownSpritePos = Sprites[EndIndex].localPosition;
            Sprites[StartIndex].transform.localPosition = DownSpritePos - new Vector3(0f, 1f, 0f) * YSize;

            StartIndex += 1;
            if (StartIndex >= Sprites.Length) StartIndex = 0;
            EndIndex += 1;
            if (EndIndex >= Sprites.Length) EndIndex = 0;
        }
    }

    public void ReduceTimeForArrival()
    {
        CurTimeForArrival -= TimeForArrivalReducingAmount;
    }
}
