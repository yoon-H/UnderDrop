using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserButton : MonoBehaviour, IHittable
{
    public GameObject ButtonTypeObstacleRef;
    private ButtonTypeObstacle ButtonTypeObstacle;

    // Start is called before the first frame update
    void Start()
    {
        ButtonTypeObstacle = ButtonTypeObstacleRef.GetComponent<ButtonTypeObstacle>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnHit()
    {
        ButtonTypeObstacle.TurnOffLaserCollision();
    }
}
