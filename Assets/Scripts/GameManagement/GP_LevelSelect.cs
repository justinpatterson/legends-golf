using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GP_LevelSelect : GamePhase
{
    public override void StartPhase()
    {
        base.StartPhase();
    }

    public void TriggerLevelLoad(int levelIndex) 
    {
        GameManager.instance.DoPhaseTransition(GameManager.GamePhases.Gameplay);
    }
    public void TriggerBackButton()
    {
        GameManager.instance.DoPhaseTransition(GameManager.GamePhases.MainMenu);
    }
    public void TriggerShopButton()
    {
        GameManager.instance.DoPhaseTransition(GameManager.GamePhases.Shop);
    }
}
