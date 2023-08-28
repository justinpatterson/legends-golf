using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayPanel : UIPanel
{
    public Button goButton;

    public GameObject EditorPhasePanel;

    public override void OpenPanel()
    {
        base.OpenPanel();
        goButton.onClick.RemoveAllListeners();
        goButton.onClick.AddListener(() => OnGoClicked());

        GP_Gameplay.OnGameplaySubPhaseStarted += SubPhaseHandler;
    }

    private void SubPhaseHandler(GP_Gameplay.GameplayPhases subPhase)
    {
        EditorPhasePanel.SetActive(subPhase == GP_Gameplay.GameplayPhases.EditorMode);
    }

    public override void ClosePanel()
    {
        base.ClosePanel();
        GP_Gameplay.OnGameplaySubPhaseStarted -= SubPhaseHandler;
    }
    public void OnGoClicked() 
    {
        Debug.Log("Launch clicked!");
        GP_Gameplay gp = (GP_Gameplay) GameManager.instance.GetCurrentGamePhase();
        if (gp != null)
            gp.ReportLaunchButtonPressed();
    }
}
