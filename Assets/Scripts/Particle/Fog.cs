using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fog : MonoBehaviour
{
    public float LifeTime = 0.7f;

    // Start is called before the first frame update
    void Start()
    {
        GetComponentInChildren<Renderer>().sortingLayerName = "Monster";

        Destroy(gameObject, LifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
