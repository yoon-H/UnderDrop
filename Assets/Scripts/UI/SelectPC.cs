using Spine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPC : PlayAnim
{
    public CharacterInfo Info;
    public SwitchPopUp PopUp;
    protected override void Event(TrackEntry entry)
    {
        Info.SelectPC();
        PopUp.SwitchFlag(false);
    }

}
