using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainMenuPanel : UIPanel
{
    public Button startGameButton;
    public Button continueGameButton;
    public override void OpenPanel()
    {
        base.OpenPanel();
        startGameButton.onClick.RemoveAllListeners();
        startGameButton.onClick.AddListener(() => OnStartButtonPressed());
        continueGameButton.onClick.RemoveAllListeners();
        continueGameButton.onClick.AddListener(() => OnStartButtonPressed());
    }

    private void OnStartButtonPressed()
    {
        AudioManager.instance.TriggerSFX(AudioManager.AudioKeys.Click);
        GP_MainMenu mainMenuPhase = (GP_MainMenu)GameManager.instance.GetCurrentGamePhase();
        if (mainMenuPhase != null)
            mainMenuPhase.ReportStartButtonPressed();
    }

    public override void ClosePanel()
    {
        base.ClosePanel();
    }
}
