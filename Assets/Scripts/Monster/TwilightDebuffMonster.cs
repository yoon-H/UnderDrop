using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwilightDebuffMonster : Monster
{

    const float SkillDelay = 2f;
    const float WarningTime = 1f;
    SkeletonAnimation Anim;

    Player PC;
    private TwilightTeam Team;

    public GameObject WarningEffect;

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
        PC.SetIsSkillCoolDownStopped(false);
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
        WarningEffect.SetActive(true);

        var time = new WaitForSeconds(WarningTime);
        yield return time;

        WarningEffect.SetActive(false);

        //Hat Animation
        Anim = GetComponentInChildren<SkeletonAnimation>();

        if(Anim != null)
        {
            TrackEntry track = Anim.AnimationState.SetAnimation(0, "skill", false);
            track.Complete += EndEvent;
            GameManager.Instance.PlaySound("hwanghonattack");
        }

    }

    void EndEvent(TrackEntry track)
    {
        if(Anim !=null)
        {
            Anim.AnimationState.SetAnimation(0, "idle", true);
        }
        
        PC.SetIsSkillCoolDownStopped(true);
    }

    public void SetPC(Player player)
    {
        PC = player;
    }

    public void SetTeam(TwilightTeam team)
    {
        Team = team;
    }
}
