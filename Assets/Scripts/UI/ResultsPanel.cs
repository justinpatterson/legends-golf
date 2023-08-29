using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ResultsPanel : UIPanel
{
    public Button confirmButton;

    public override void OpenPanel()
    {
        base.OpenPanel();
        confirmButton.onClick.RemoveAllListeners();
        confirmButton.onClick.AddListener(() => OnResultsClicked());
        //probably populate some Angry Birds Stars or something 
    }


    void OnResultsClicked() 
    {
        GameManager.instance.DoPhaseTransition(GameManager.GamePhases.MainMenu);
    }

    public override void ClosePanel()
    {
        base.ClosePanel();
    }
}
