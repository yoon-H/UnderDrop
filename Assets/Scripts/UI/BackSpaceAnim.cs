using Spine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackSpaceAnim : PlayAnim
{
    public SwitchPopUp PopUp;
    protected override void Event(TrackEntry entry)
    {
        base.Event(entry);

        PopUp.SwitchFlag(false);

    }
}
