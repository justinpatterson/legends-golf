using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectPanel : UIPanel
{
    int _selectedLevel = -1;
    public Button backButton;
    public Button shopButton;
    public override void OpenPanel()
    {
        base.OpenPanel();
        _selectedLevel = GetDefaultLevelSelect();
        //backButton was hooked up in editor via events
        //shopButton was hooked up in editor via events
    }

    int GetDefaultLevelSelect() 
    {
        //likely related to player prefs
        return 0; 
    }

    public void ReportLevelLaunchPressed(int levelIndex) 
    {
        _selectedLevel = levelIndex;
        if (GameManager.instance)
        {
            GamePhase gamePhase = GameManager.instance.GetCurrentGamePhase();
            GP_LevelSelect gpCast = (GP_LevelSelect)gamePhase;
            if (gpCast!= null) 
            {
                gpCast.TriggerLevelLoad(levelIndex);
            }
        }


    }

    public void ReportBackButtonPressed() 
    {
        GamePhase gamePhase = GameManager.instance.GetCurrentGamePhase();
        GP_LevelSelect gpCast = (GP_LevelSelect)gamePhase;
        if (gpCast!= null)
        {
            gpCast.TriggerBackButton();
        }
    }
    public void ReportShopButtonPressed()
    {
        GamePhase gamePhase = GameManager.instance.GetCurrentGamePhase();
        GP_LevelSelect gpCast = (GP_LevelSelect)gamePhase;
        if (gpCast!= null)
        {
            gpCast.TriggerShopButton();
        }
    }

}
