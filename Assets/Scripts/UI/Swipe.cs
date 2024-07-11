using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Swipe : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Vector2 fingerDownPosition;
    private Vector2 fingerUpPosition;

    private float CheckDist = 200f;

    public GameObject PlayerRef;
    private Player Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = PlayerRef.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerDown(PointerEventData data)
    {
        fingerDownPosition = data.position;
    }

    public void OnPointerUp(PointerEventData data)
    {
        fingerUpPosition = data.position;
        CheckSwipe();

        fingerDownPosition = Vector2.zero;
        fingerUpPosition = Vector2.zero;
    }

    private void CheckSwipe()
    {
        float dist = Mathf.Abs(fingerDownPosition.y - fingerUpPosition.y);
        print(dist);
        if(dist >= CheckDist)
        {
            
            if(fingerDownPosition.y - fingerUpPosition.y < 0)
            {
                Player.SetCanShoot(true);
                print("swipe");
            }
                
        }
        else
        {
            Player.SwitchDir();
        }
      
    }
}
