using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCharacter : MonoBehaviour
{
    public GameObject Timer;
    public GameObject BulletPanel;
    public GameObject BackgroundBullet;
    public GameObject FillBullet;

    public GameObject[] PlayerRefs;
    private E_Team[] Teams = { E_Team.SID, E_Team.Twilight };
    public GameObject Player;

    public GameObject MoveButton;

    public Vector3 SpawnPoisition = new Vector3(2.025f, 2f , 0);


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnPC(int index)
    {
        if (PlayerRefs[index])
        {
            Player = Instantiate(PlayerRefs[index]);

            if(Player == null)
            {
                print("null");
            }

            Player player = Player.GetComponent<Player>();

            player.SetPCInfo(Timer, BulletPanel);

            GameManager.Instance.team = Teams[index];

            Swipe click =  MoveButton.GetComponent<Swipe>();

            click.SetPlayer(Player);

            Player.transform.position = SpawnPoisition;

            if(TryGetComponent<Timer>(out var timer))
            {
                timer.SetPlayerRef(Player);
            }

            if(index == 0)
            {
                Player.GetComponent<PCAnimation>().CheckIsNoke(true);
            }

        }
    }
}
