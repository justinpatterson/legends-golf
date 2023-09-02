using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GP_Init : GamePhase
{
    public override void StartPhase()
    {
        base.StartPhase();
        if (GameManager.instance != null)
            GameManager.instance.DoPhaseTransition(GameManager.GamePhases.MainMenu);
    }
}
