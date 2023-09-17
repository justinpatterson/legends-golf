using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GP_MainMenu : GamePhase
{

    public override void StartPhase()
    {
        base.StartPhase();
        AudioManager.instance.TriggerMusic(AudioManager.AudioKeys.Menu);
    }

    public void ReportStartButtonPressed() 
    {
        GameManager.instance.DoPhaseTransition(GameManager.GamePhases.LevelSelect);
    }

    public override void EndPhase()
    {
        base.EndPhase();
    }
}
