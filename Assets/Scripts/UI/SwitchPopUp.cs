using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPopUp : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchFlag(bool isVisible)
    {
        if (isVisible) 
        {
            gameObject.SetActive(true);
            //TODO : Stop time
        }
        else
        {
            gameObject.SetActive(false);
            //TODO : Resume game
        }
    }
}
