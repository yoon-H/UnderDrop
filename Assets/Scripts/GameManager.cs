using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    private void Awake()
    {
        if(!Instance)    // Singleton pattern
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  
        }
        else
        {
            if(Instance != this)
                Destroy(gameObject);
        }        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
