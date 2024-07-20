using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerButton : MonoBehaviour, IHittable
{
    private TriggerObstacle TriggerObstacle;

    // Start is called before the first frame update
    void Start()
    {
        TriggerObstacle = GetComponentInParent<TriggerObstacle>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnHit()
    {
        TriggerObstacle.TurnOnBomb();
    }
}
