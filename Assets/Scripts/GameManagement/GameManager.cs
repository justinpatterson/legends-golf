using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    

    public enum GamePhases
    {
        Init,
        MainMenu,
        Gameplay,
        Shop,
        Results,
        LevelSelect,
        Inventory
    }

    [System.Serializable]
    public struct PhaseInfo
    {
        public GamePhases phase;
        public GamePhase phaseLogic;
    }
    public delegate void PhaseTransition(GamePhases phase);
    public static PhaseTransition OnPhaseTransition;

    public PhaseInfo[] phaseLogics;
    public GamePhases currentPhase = GamePhases.Init;
    GamePhase _currentPhaseLogic;
    public Localization.LocalizationManager localizationManager;
    public TutorialManager tutorialManager;
    public TTSManager ttsManager;

    public static GameManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        DoPhaseTransition(GamePhases.Init);
    }

    public void DoPhaseTransition(GamePhases nextPhase)
    {
        if (currentPhase != nextPhase)
        {
            _currentPhaseLogic?.EndPhase();
        }
        else
        {
            //no point in ending a phase that is also being started.
        }

        currentPhase = nextPhase;
        foreach (PhaseInfo pl in phaseLogics)
        {
            if (pl.phase == currentPhase)
            {
                _currentPhaseLogic = pl.phaseLogic;
                break;
            }
        }
        _currentPhaseLogic?.StartPhase();
        OnPhaseTransition?.Invoke(currentPhase);

    }

    public bool HasFinishedAllLevels() 
    {
        bool hasFinished = true;
        for (int i = 0; i < 7; i++)
        {
            bool foundLevel = false;
            GameDataManager.GameData.LevelData ld = GameDataManager.instance.GetLevelData(i, out foundLevel);
            if (foundLevel)
            {
                hasFinished &= ld.starCount>0;
            }
            else
                return false;
        }
        return hasFinished;
    }

    public GamePhase GetCurrentGamePhase() {
        return _currentPhaseLogic;
    }
    private void Update()
    {
        _currentPhaseLogic?.UpdatePhase();


    }
    
    public bool IsGameOver()
    {
        return false;
    }
}
