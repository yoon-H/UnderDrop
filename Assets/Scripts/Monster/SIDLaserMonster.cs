using Spine.Unity;
using Spine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SIDLaserMonster : Monster
{
    const float SkillDelay = 2f;
    const float WarningTime = 4f;
    SkeletonAnimation Anim;

    private SIDTeam Team;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        StartCoroutine(IE_WaitForDebuff());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {
        Team.SetSpecialMonsterFlag(false);
    }

    IEnumerator IE_WaitForDebuff()
    {
        var time = new WaitForSeconds(SkillDelay);
        yield return time;


        StartCoroutine(IE_Debuff());

    }

    IEnumerator IE_Debuff()
    {
        //TODO Warning

        var time = new WaitForSeconds(WarningTime);
        yield return time;

        //TODO Anim

        //TODO Spawn Obstacle


    }

    public void SetTeam(SIDTeam team)
    {
        Team = team;
    }
}
