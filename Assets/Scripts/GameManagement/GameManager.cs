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
