using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayPanel : UIPanel
{
    public Button goButton;

    public GameObject EditorPhasePanel;
    public GameObject LaunchPhasePanel;
    public TextMeshProUGUI launchPhaseCountdown;

    public override void OpenPanel()
    {
        base.OpenPanel();
        goButton.onClick.RemoveAllListeners();
        goButton.onClick.AddListener(() => OnGoClicked());
        Debug.Log("Open play panel triggered.");
        GP_Gameplay.OnGameplaySubPhaseStarted += SubPhaseHandler;
        GP_Gameplay.OnTimeUpdated += RefreshLaunchCounter;
    }

    private void SubPhaseHandler(GP_Gameplay.GameplayPhases subPhase)
    {
        Debug.Log("Subphase UI listener fired...");
        EditorPhasePanel.SetActive(subPhase == GP_Gameplay.GameplayPhases.EditorMode);
        LaunchPhasePanel.SetActive(subPhase == GP_Gameplay.GameplayPhases.Launch);
    }
    private void LateUpdate()
    {
        
    }
    void RefreshLaunchCounter(float time)
    {
        launchPhaseCountdown.text = Mathf.Ceil(time).ToString("00");
    }
    public override void ClosePanel()
    {
        base.ClosePanel();
        GP_Gameplay.OnGameplaySubPhaseStarted -= SubPhaseHandler;
        GP_Gameplay.OnTimeUpdated -= RefreshLaunchCounter;
    }
    public void OnGoClicked() 
    {
        Debug.Log("Launch clicked!");
        GP_Gameplay gp = (GP_Gameplay) GameManager.instance.GetCurrentGamePhase();
        if (gp != null)
            gp.ReportLaunchButtonPressed();
    }
}
