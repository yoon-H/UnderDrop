using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaidEvent : MonoBehaviour
{
    public float WarningTime = 1f;

    Timer Timer;

    public GameObject WarningPanel;

    public GameObject RaidBar;
    public ProgressBar Bar;

    // Start is called before the first frame update
    void Start()
    {
        WarningPanel.SetActive(false);

        //RaidBar
        Bar = RaidBar.GetComponent<ProgressBar>();
        RaidBar.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator IE_Warning()
    {
        // Warning Animation
        Time.timeScale = 0.5f;
        WarningPanel.SetActive(true);
        yield return new WaitForSeconds(WarningTime);
        Timer.SetIsRaidExisted(true);
        WarningPanel.SetActive(false);
        RaidBar.SetActive(true);
        Time.timeScale = 1f;
    }

    public void SetTimer(Timer timer)
    {
        Timer = timer;
        Bar.Maxvalue = Timer.RaidRemainTime;
    }
}
