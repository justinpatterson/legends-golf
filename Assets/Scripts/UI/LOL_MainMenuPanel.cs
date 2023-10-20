using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LOL_MainMenuPanel : MainMenuPanel
{
    public LoLConnectionMgr LoLConnectionMgr;
    public override void OpenPanel()
    {
        _isOpen = true;
        gameObject.SetActive(_isOpen);

        startGameButton.onClick.RemoveAllListeners();
        startGameButton.onClick.AddListener(() => OnStartButtonPressed());
        continueGameButton.onClick.RemoveListener(() => OnStartButtonPressed());

        if (LoLConnectionMgr != null)
        {
            LoLConnectionMgr.OnDataLoaded += LoadCompleted;
            Debug.Log("SUBSCRIBED...");
        }
        else 
        {
            continueGameButton.onClick.AddListener(() => OnStartButtonPressed());
        }
    }
    protected virtual void LoadCompleted(bool success) 
    {
        Debug.Log("Load successful? " + success);
        if (success) 
        {
            Debug.Log("First level star count: " + GameDataManager.instance.gameData.levelDataMap[0].starCount);
        }
        Debug.Log("Current phase... " + GameManager.instance.currentPhase.ToString());
        if (GameManager.instance.currentPhase != GameManager.GamePhases.MainMenu)
        {
            Debug.Log("LOAD is running twice...");
            return;
        }
        LoLConnectionMgr.OnDataLoaded -= LoadCompleted;
        OnStartButtonPressed();
    }
}
