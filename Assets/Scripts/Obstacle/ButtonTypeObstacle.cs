using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTypeObstacle : MonoBehaviour
{
    public GameObject LaserObstacleRef;
    public GameObject LaserButtonRef;

    public Sprite InActiveButtonSprite;
    public Sprite InActiveObstacleSprite;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnOffLaserCollision()
    {
        LaserObstacleRef.GetComponent<BoxCollider2D>().enabled = false;

        SpriteRenderer buttonRenderer = LaserButtonRef.GetComponentInChildren<SpriteRenderer>();
        if (buttonRenderer)
            buttonRenderer.sprite = InActiveButtonSprite;

        //SpriteRenderer obstacleRenderer = LaserObstacleRef.GetComponentInChildren<SpriteRenderer>();
        //if(obstacleRenderer)
        //    obstacleRenderer.sprite = InActiveObstacleSprite;

    }
}
