using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerObstacle : MonoBehaviour
{
    public GameObject BombObstacleRef;
    public GameObject ButtonRef;

    public Sprite InActiveButtonSprite;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void TurnOnBomb()
    {
        BombObstacleRef.GetComponent<Collider2D>().enabled = true;

        SpriteRenderer buttonRenderer = ButtonRef.GetComponent<SpriteRenderer>();
        if (buttonRenderer)
            buttonRenderer.sprite = InActiveButtonSprite;

        //SpriteRenderer obstacleRenderer = LaserObstacleRef.GetComponentInChildren<SpriteRenderer>();
        //if(obstacleRenderer)
        //    obstacleRenderer.sprite = InActiveObstacleSprite;

    }
}
