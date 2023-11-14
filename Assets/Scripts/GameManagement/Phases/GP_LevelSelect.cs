using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GP_LevelSelect : GamePhase
{
    public UIPanel introPanel;
    bool _firstStart;
    public override void StartPhase()
    {
        base.StartPhase();
        AudioManager.instance.TriggerMusic(AudioManager.AudioKeys.Menu);

        if (!_firstStart)
        {
            _firstStart = true;
            introPanel?.OpenPanel();
        }
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
    public void TriggerEquipButton()
    {
        GameManager.instance.DoPhaseTransition(GameManager.GamePhases.Inventory);
    }
}
