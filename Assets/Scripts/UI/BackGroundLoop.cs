using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundLoop : MonoBehaviour
{
    //Movement
    private float MoveSpeed = 0.3f;
    private float CameraSize;

    //Scrolling
    public int StartIndex;
    public int EndIndex;
    public Transform[] Sprites;
    private const float MaxYLoc = 15f;
    public float YSize = 13.8f;

    //BackGround Sprites;
    public Sprite[] BackGrounds;

    public int SpriteLength = 3;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Sprites[StartIndex].position.y > MaxYLoc)
        {
            Vector3 DownSpritePos = Sprites[EndIndex].localPosition;
            Sprites[StartIndex].transform.localPosition = DownSpritePos - new Vector3(0f, 1f, 0f) * YSize;

            StartIndex += 1;
            if (StartIndex >= Sprites.Length) StartIndex = 0;
            EndIndex += 1;
            if (EndIndex >= Sprites.Length) EndIndex = 0;

            SelectNextSprite();
        }
    }

    private void SelectNextSprite()
    {
        System.Random rand = new System.Random();
        int res = rand.Next(SpriteLength);

        if(Sprites[EndIndex].gameObject.TryGetComponent<SpriteRenderer>(out var renderer))
        {
            renderer.sprite = BackGrounds[res];
        }
    }

}
