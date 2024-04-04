using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongClick : MonoBehaviour
{
    private bool IsClicked = false;
    private float ClickTime = 1f;
    private float ClickCounter = 0f;

    public GameObject PlayerRef;
    private Player Player;

    public E_Direction Dir;

    // Start is called before the first frame update
    void Start()
    {
        Player = PlayerRef.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(IsClicked)
        {
            ClickCounter += Time.deltaTime;

            if (ClickCounter >= ClickTime)
            {
                if (!Player) { return; }
                Player.SetCanShoot(true);
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
        if(!Player) { return; }
        Player.SwitchDir(Dir);
    }
    
    public void ButtonUp()
    {
        IsClicked=false;
        if (!Player) { return; }
        Player.SetCanShoot(false);
    }   

}
