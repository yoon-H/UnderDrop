using Spine.Unity;
using Spine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SIDLaserMonster : Monster
{
    const float SkillDelay = 2f;
    const float WarningTime = 4f;
    const float LaserTime = 1f;
    SkeletonAnimation Anim;

    private SIDTeam Team;

    public GameObject LaserObject;
    public GameObject SpawnLocation;

    public GameObject SpawnedLaser;

    public GameObject WarningEffect;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        Anim = DataAsset.GetComponent<SkeletonAnimation>();

        StartCoroutine(IE_WaitForDebuff());
    }

    // Update is called once per frame
    void Update()
    {
        if(SpawnedLaser != null)
        {
            SpawnedLaser.transform.position = SpawnLocation.transform.position;
        }
    }

    private void OnDestroy()
    {
        Team.SetSpecialMonsterFlag(false);

        if(LaserObject != null) { Destroy(SpawnedLaser); }
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

        var track = Anim.AnimationState.SetAnimation(0, "skill", false);

        track.Complete += EndEvent;
    }

    public void SetTeam(SIDTeam team)
    {
        Team = team;
    }

    private void EndEvent(TrackEntry entry)
    {
        SpawnedLaser = Instantiate(LaserObject);
        GameManager.Instance.PlaySound("sidattack");

        StartCoroutine(IE_DestroyLaser());
    }

    IEnumerator IE_DestroyLaser()
    {
        var time = new WaitForSeconds(LaserTime);
        yield return time;

        Destroy(SpawnedLaser);
    }
}
