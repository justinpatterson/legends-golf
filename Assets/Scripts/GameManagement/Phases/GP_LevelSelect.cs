using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GP_LevelSelect : GamePhase
{
    public override void StartPhase()
    {
        base.StartPhase();
        AudioManager.instance.TriggerMusic(AudioManager.AudioKeys.Menu);
    }

    public void TriggerLevelLoad(int levelIndex) 
    {
        GP_Gameplay.levelIndexSelected = levelIndex;
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
