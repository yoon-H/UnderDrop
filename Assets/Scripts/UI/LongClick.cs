using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongClick : MonoBehaviour
{
    private bool IsClicked = false;
    private float ClickTime = 1f;
    private float ClickCounter = 0f;

    public GameObject PlayerRef;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(IsClicked)
        {
            ClickCounter += Time.deltaTime;

            if (ClickCounter >= ClickTime)
            {
                Player player = PlayerRef.GetComponent<Player>();
                player.SetCanShoot(true);
            }
        }
        else
        {
            ClickCounter = 0f;
        }
    }


    public void ButtonDown()
    {
        IsClicked = true;
    }
    
    public void ButtonUp()
    {
        IsClicked=false;
        Player player = PlayerRef.GetComponent<Player>();
        player.SetCanShoot(false);
    }   

}
