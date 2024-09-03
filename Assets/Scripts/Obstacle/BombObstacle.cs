using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombObstacle : Obstacle
{
    const float WaitingTime = 1f;
    public SkeletonAnimation Anim;
    public GameObject Explosion;
    const float StartTime = 5f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(IE_Wait());
        Anim.AnimationState.SetAnimation(0, "idle", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator IE_Wait()
    {
        var time = new WaitForSeconds(WaitingTime);
        yield return time;
        var track = Anim.AnimationState.SetAnimation(0, "P", false);
        track.TrackTime = StartTime;
        track.Complete += Event;
    }

    private void Event(TrackEntry entry)
    {
        var explosion = Instantiate(Explosion);
        explosion.transform.position = transform.position;
        explosion.GetComponentInChildren<Obstacle>().InitializeObstacleStats(Timer);
        Destroy(explosion, 1f);

        Destroy(gameObject);
    }
}
