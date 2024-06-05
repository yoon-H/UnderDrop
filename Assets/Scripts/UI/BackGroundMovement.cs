using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
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

    //Wall Sprites
    public Sprite[] LeftWalls;
    public Sprite[] RightWalls;

    public int SpriteLength = 3 ;

    public int CurrentSpriteIndex = 0;
    public int ChangeCount = 0;
    public bool SpriteChanging;


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

            print("out");
            print(SpriteChanging);
            print("ChangeCount :: " + ChangeCount);
            if(SpriteChanging && ChangeCount < 3)
            {
                print("In");
                ChangeWallSprite();

                ChangeCount += 1;

                if(ChangeCount >=3)
                {
                    ChangeCount = 0;
                    Setflag(false);
                }

            }
        }

        //print(SpriteChanging);
    }

    public void ReduceTimeForArrival()
    {
        CurTimeForArrival -= TimeForArrivalReducingAmount;
    }

    private void ChangeWallSprite()
    {
        print("Change");

        SpriteRenderer[] renderers = Sprites[EndIndex].gameObject.GetComponentsInChildren<SpriteRenderer>();

        foreach (var item in renderers)
        {
            if(item.gameObject.name == "LeftWall")
            {
                item.sprite = LeftWalls[CurrentSpriteIndex];
            }
            else
            {
                item.sprite = RightWalls[CurrentSpriteIndex];
            }
        }
    }

    public void Setflag(bool flag)
    {
        if(SpriteChanging != flag)
        {
            if (!flag)
            {
                SpriteChanging = false;
            }
            else
            {
                SpriteChanging = true;

                AddSpriteIndex();

                print("flag :: " + SpriteChanging);
            }
        }
        
    }

    public void AddSpriteIndex()
    {
        print("ADD Length::" + CurrentSpriteIndex + " " + SpriteLength);

        CurrentSpriteIndex += 1;

        if (CurrentSpriteIndex >= SpriteLength)
        {
            print("return");
            CurrentSpriteIndex = SpriteLength - 1;
            Setflag(false);
        }
    }
}
