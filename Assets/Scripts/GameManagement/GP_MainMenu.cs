using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GP_MainMenu : GamePhase
{

    public override void StartPhase()
    {
        base.StartPhase();
    }

    public void ReportStartButtonPressed() 
    {
        GameManager.instance.DoPhaseTransition(GameManager.GamePhases.Gameplay);
    }

    public override void EndPhase()
    {
        base.EndPhase();
    }
}
