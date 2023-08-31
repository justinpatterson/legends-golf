using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GP_Shop : GamePhase
{
    public void TriggerBackButton() 
    {
        GameManager.instance.DoPhaseTransition(GameManager.GamePhases.LevelSelect);
    }
}
