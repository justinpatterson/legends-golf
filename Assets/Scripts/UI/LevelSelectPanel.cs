using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LevelSelectPanel : UIPanel
{
    int _selectedLevel = -1;
    public Button backButton;
    public Button shopButton;
    public TextMeshProUGUI currencyText;

    public override void OpenPanel()
    {
        base.OpenPanel();
        _selectedLevel = GetDefaultLevelSelect();
        if (currencyText != null)
            currencyText.text = GameDataManager.instance.gameData.playerData.currencyAmt.ToString("00");
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
        AudioManager.instance.TriggerSFX(AudioManager.AudioKeys.Click);

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
        AudioManager.instance.TriggerSFX(AudioManager.AudioKeys.Back);

        GamePhase gamePhase = GameManager.instance.GetCurrentGamePhase();
        GP_LevelSelect gpCast = (GP_LevelSelect)gamePhase;
        if (gpCast!= null)
        {
            gpCast.TriggerBackButton();
        }
    }

    public void ReportEquipButtonPressed()
    {
        AudioManager.instance.TriggerSFX(AudioManager.AudioKeys.Click);
        GamePhase gamePhase = GameManager.instance.GetCurrentGamePhase();
        GP_LevelSelect gpCast = (GP_LevelSelect)gamePhase;
        if (gpCast!= null)
        {
            gpCast.TriggerEquipButton();
        }
    }
    public void ReportShopButtonPressed()
    {
        AudioManager.instance.TriggerSFX(AudioManager.AudioKeys.Click);
        GamePhase gamePhase = GameManager.instance.GetCurrentGamePhase();
        GP_LevelSelect gpCast = (GP_LevelSelect)gamePhase;
        if (gpCast!= null)
        {
            gpCast.TriggerShopButton();
        }
    }

}
