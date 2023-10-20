using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LoLSDK;
using SimpleJSON;
using System;

public class LoLConnectionMgr : MonoBehaviour
{
    Coroutine _feedbackMethod;
    private void Start()
    {
        // Create the WebGL (or mock) object
#if UNITY_EDITOR
        ILOLSDK sdk = new LoLSDK.MockWebGL();
#elif UNITY_WEBGL
            ILOLSDK sdk = new LoLSDK.WebGL();
#endif

        LOLSDK.Init(sdk, "com.legends-of-learning.gravity-golf");

        // Register event handlers
        LOLSDK.Instance.StartGameReceived += new StartGameReceivedHandler(StartGame);
        LOLSDK.Instance.GameStateChanged += new GameStateChangedHandler(gameState => Debug.Log(gameState));
        LOLSDK.Instance.QuestionsReceived += new QuestionListReceivedHandler(questionList => Debug.Log(questionList));
        LOLSDK.Instance.LanguageDefsReceived += new LanguageDefsReceivedHandler(LanguageUpdate);

        // Used for player feedback. Not required by SDK.
        LOLSDK.Instance.SaveResultReceived += OnSaveResult;

        // Call GameIsReady before calling LoadState or using the helper method.
        LOLSDK.Instance.GameIsReady();
        GameManager.OnPhaseTransition += OnGamePhaseChanged;

#if UNITY_EDITOR
        UnityEditor.EditorGUIUtility.PingObject(this);
        //LoadMockData();
#endif

        // Helper method to hide and show the state buttons as needed.
        // Will call LoadState<T> for you.
        Helper.StateButtonInitialize<GameDataManager.GameData>(newGameButton, continueButton, OnLoad);
    }

    private void OnGamePhaseChanged(GameManager.GamePhases phase)
    {
        if (phase == GameManager.GamePhases.Results)
        {
            //LOLSDK.Instance.SubmitProgress()
            int score, progress, maxProgress = 0;
            CalculateProgress(out score, out progress, out maxProgress);
            LOLSDK.Instance.SubmitProgress(score, progress, maxProgress);
        }
        else if (phase == GameManager.GamePhases.LevelSelect) 
        {
            //make sure we save changing outfits and stuff
            
        }
        else 
        {
            return;
        }
        LOLSDK.Instance.SaveState(GameDataManager.instance.gameData);
    }

    [SerializeField]
    public UnityEngine.UI.Button newGameButton, continueButton;

    void StartGame(string startGameJSON)
    {
        if (string.IsNullOrEmpty(startGameJSON))
            return;

        JSONNode startGamePayload = JSON.Parse(startGameJSON);
        // Capture the language code from the start payload. Use this to switch fonts
        Localization.LocalizationManager.currentLanguage = startGamePayload["languageCode"];
    }

    void OnLoad(GameDataManager.GameData loadedGameData)
    {
        // Overrides serialized state data or continues with editor serialized values.
        if (loadedGameData != null)
        {
            GameDataManager.instance.gameData = loadedGameData;
            OnDataLoaded?.Invoke(true);
        }
        else 
            OnDataLoaded?.Invoke(false);

    }
    public delegate void DataLoaded(bool success);
    public DataLoaded OnDataLoaded;

    void LanguageUpdate(string langJSON)
    {
        if (string.IsNullOrEmpty(langJSON))
            return;
        GameManager.instance.localizationManager.SerializeLocalization(langJSON);
        //I don't believe we'll need to update the GameManager.instance.localizationManager.json but we could if want.
    }
    void Save()
    {
        GameDataManager.GameData data = GameDataManager.instance.gameData;
        LOLSDK.Instance.SaveState(data);
    }
    void OnSaveResult(bool success)
    {
        if (!success)
        {
            Debug.LogWarning("Saving not successful");
            return;
        }

        if (_feedbackMethod != null)
            StopCoroutine(_feedbackMethod);
        // ...Auto Saving Complete
        //_feedbackMethod = StartCoroutine(_Feedback(GetText("autoSave")));
    }


    void CalculateProgress(out int score, out int progress, out int maxProgress) 
    {
        score = 0;
        progress = 0;
        maxProgress = 0;
        for(int i = 0; i <= 6; i++) 
        {
            bool success = false;
            GameDataManager.GameData.LevelData ld = GameDataManager.instance.GetLevelData(i, out success);
            if (success) 
            {
                score += ld.starCount;
                progress += ld.starCount > 0 ? 1 : 0;
            }
            maxProgress += 1;
        }
    }
}
