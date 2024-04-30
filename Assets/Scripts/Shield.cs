using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public Knock Knock;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            Obstacle obstacle = collision.gameObject.GetComponent<Obstacle>();
            if(obstacle != null)
            {
                obstacle.SetInActive();
                print("Shield");
                Knock.SetIsShield(false);
                print(obstacle.gameObject.GetComponent<Collider2D>().isActiveAndEnabled);
            }
                
        }
    }

    public void SetKnock(Knock knock)
    {
        Knock = knock;
    }
}
