using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaidEvent : MonoBehaviour
{
    public float WarningTime = 3f;

    Timer Timer;

    public GameObject RaidMarkRef;
    public GameObject[] WarningPanels;
    public GameObject TeamPanel;

    public GameObject RaidBar;
    public ProgressBar Bar;

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
        TeamPanel.SetActive(true);

        yield return new WaitForSeconds(WarningTime);


        Timer.SetIsRaidExisted(true);
        TeamPanel.SetActive(false);
        RaidBar.SetActive(true);
        RaidMarkRef.SetActive(true);
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
}
