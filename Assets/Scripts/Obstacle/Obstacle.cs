using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour, IHittable
{
    Timer Timer;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnHit()
    {
        Timer.GameOver();
    }

    public void InitializeObstacleStats(Timer timer)
    {
        Timer = timer;
    }

    public void SetInActive()
    {
        gameObject.GetComponent<Collider2D>().enabled = false;
        SpriteRenderer renderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        Color color = renderer.color;
        float a = 40f / 255f;
        color = new Color(color.r, color.g, color.b, a);        
        renderer.color = color;
    }
}
