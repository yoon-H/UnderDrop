using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaidEvent : MonoBehaviour
{
    public float WarningTime = 3f;

    Timer Timer;

    public GameObject RaidMarkRef;
    public GameObject BackPanel;
    public GameObject[] WarningPanels;
    public GameObject TeamPanel;

    public GameObject RaidBar;
    public ProgressBar Bar;

    public GameObject RaidBackGroundRef;
    public Sprite[] RaidBackGrounds;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var panel in WarningPanels)
        {
            panel.SetActive(false);
        }

        //RaidBar
        if(Bar == null)
        {
            Bar = RaidBar.GetComponent<ProgressBar>();
        }
        
        RaidBar.SetActive(false);
        RaidMarkRef.SetActive(false);
        BackPanel.SetActive(false);

        RaidBackGroundRef.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator IE_Warning(E_Team team)
    {
        //Select Team Raid Warning Panel 
        SetRaidPanel(team);

        // Warning Animation
        Time.timeScale = 0.3f;
        BackPanel.SetActive(true);
        TeamPanel.SetActive(true);

        //Start Animation
        
        if(TeamPanel.TryGetComponent<SkeletonGraphic>(out var graph))
        {
            graph.AnimationState.SetAnimation(0, "animation", false);
        }

        // Play Sound
        GameManager.Instance.PlaySound("raidbgm");

        yield return new WaitForSeconds(WarningTime);

        //SpawnMonster
        Timer.SpawnMonster();


        TeamPanel.SetActive(false);
        BackPanel.SetActive(false);

        Timer.SetIsRaidExisted(true);
        RaidBar.SetActive(true);
        RaidMarkRef.SetActive(true);
        SetRaidBackGround(true, team);
        
        Time.timeScale = 1f;
    }

    public void SetTimer(Timer timer)
    {
        if (Bar == null)
        {
            Bar = RaidBar.GetComponent<ProgressBar>();
        }
        Timer = timer;
        Bar.Maxvalue = Timer.RaidRemainTime;
    }
    
    public void SetRaidPanel(E_Team team)
    {
        switch(team)
        {
            case E_Team.SID:
                TeamPanel = WarningPanels[0]; break;
            case E_Team.Weasel:
                TeamPanel = WarningPanels[1]; break;
            case E_Team.Twilight:
                TeamPanel = WarningPanels[2]; break;
        }
    }

    public void SetRaidBackGround(bool flag, E_Team team)
    {
        RaidBackGroundRef.SetActive(flag);

        if(flag)
        {
            SpriteRenderer renderer = RaidBackGroundRef.GetComponent<SpriteRenderer>();

            switch (team)
            {
                case E_Team.SID:
                    renderer.sprite = RaidBackGrounds[0]; break;
                case E_Team.Weasel:
                    renderer.sprite = RaidBackGrounds[1]; break;
                case E_Team.Twilight:
                    renderer.sprite = RaidBackGrounds[2]; break;
            }
        }
    }
}
