using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCharacter : MonoBehaviour
{
    public GameObject Timer;
    public GameObject BulletPanel;

    public GameObject PlayerRef;
    public GameObject Player;

    public GameObject MoveButton;

    public Vector3 SpawnPoisition = new Vector3(1.98f, 2f , 0);


    // Start is called before the first frame update
    void Start()
    {
        SpawnPC();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnPC()
    {
        if(PlayerRef)
        {
            Player = Instantiate(PlayerRef);
            Knock knock = Player.GetComponent<Knock>();

            knock.SetPCInfo(Timer, BulletPanel);

            LongClick[] clicks =  MoveButton.GetComponentsInChildren<LongClick>();

            foreach(LongClick click in clicks)
            {
                click.PlayerRef = Player;
            }

            Player.transform.position = SpawnPoisition;

        }
    }
}
