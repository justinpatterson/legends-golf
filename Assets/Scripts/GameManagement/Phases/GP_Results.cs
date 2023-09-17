using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GP_Results : GamePhase
{
    public override void StartPhase()
    {
        base.StartPhase();

        AudioManager.instance.TriggerMusic(AudioManager.AudioKeys.Results);
    }
}
