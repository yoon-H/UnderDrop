using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTypeObstacle : MonoBehaviour
{
    public GameObject LaserObstacleRef;
    public GameObject LaserButtonRef;

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
        SpriteRenderer renderer = LaserObstacleRef.GetComponentInChildren<SpriteRenderer>();
        renderer.color = new Color(0,255,255);

    }
}
