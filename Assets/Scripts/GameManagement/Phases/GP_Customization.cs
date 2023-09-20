using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GP_Customization : GamePhase
{
   
    public void TriggerBackButton()
    {
        GameManager.instance.DoPhaseTransition(GameManager.GamePhases.LevelSelect);
    }
    public override void StartPhase()
    {
        base.StartPhase();
    }
    public override void EndPhase()
    {
        base.EndPhase();
    }

    
}
