using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GP_Gameplay : GamePhase
{
    [SerializeField]
    GameObject _spawnedLevelInstance; //eventually this will be spawned from PF

    public enum GameplayPhases { Load, EditorMode, Launch, Evaluate }
    public GameplayPhases currentGameplayPhase = GameplayPhases.Load;

    public delegate void GameplaySubphaseTransition(GameplayPhases subPhase);
    public static GameplaySubphaseTransition OnGameplaySubPhaseStarted;

    public override void StartPhase()
    {
        base.StartPhase();
        SubPhaseTransition(GameplayPhases.Load);
        SubPhaseTransition(GameplayPhases.EditorMode);
    }

    public void SubPhaseTransition(GameplayPhases phase) 
    {
        if(currentGameplayPhase != phase) 
        {
            SubPhaseEnd();
        }
        currentGameplayPhase = phase;
        SubPhaseStart();
        OnGameplaySubPhaseStarted?.Invoke(phase);
    }

    void SubPhaseStart() 
    {
        switch (currentGameplayPhase)
        {
            case GameplayPhases.Load:
                _spawnedLevelInstance.SetActive(true);
                break;
            case GameplayPhases.EditorMode:
                {
                    if (_ballReference) { }
                    else
                    {
                        GravitySim gs = FindObjectOfType<GravitySim>();
                    }
                    //maybe turn this into a subscription event
                    if (_ballReference)
                    {
                        _ballReference.gameObject.SetActive(true);
                        _ballReference.ResetObject();
                    }
                }
                break;
            case GameplayPhases.Launch:
                {
                    //maybe turn this into a subscription event
                    GravitySim gs = FindObjectOfType<GravitySim>();
                    if (gs)
                    {
                        Debug.Log("Shoot!");
                        gs.Shoot();
                    }
                    else
                        Debug.Log("Can't shoot!");
                }
                GoalObject.OnGoalTriggered += ReportGoalTriggered;
                break;
            case GameplayPhases.Evaluate:
                break;
        }
    }
    void SubPhaseUpdate() 
    {
        switch (currentGameplayPhase)
        {
            case GameplayPhases.Load:
                break;
            case GameplayPhases.EditorMode:
                break;
            case GameplayPhases.Launch:
                break;
            case GameplayPhases.Evaluate:
                break;
        }
    }
    void SubPhaseEnd() 
    {
        switch (currentGameplayPhase)
        {
            case GameplayPhases.Load:
                break;
            case GameplayPhases.EditorMode:
                break;
            case GameplayPhases.Launch:
                GoalObject.OnGoalTriggered -= ReportGoalTriggered;
                break;
            case GameplayPhases.Evaluate:
                break;
        }
    }

    public void ReportLaunchButtonPressed() 
    {
        Debug.Log("LAUNCH BUTTON");
        if (currentGameplayPhase == GameplayPhases.EditorMode)
            SubPhaseTransition(GameplayPhases.Launch);
    }
    GravitySim _ballReference;
    private void ReportGoalTriggered(GoalObject goal, Collider2D collision)
    {
        if (collision.gameObject.GetComponent<GravitySim>() != null) 
        {
            _ballReference = collision.gameObject.GetComponent<GravitySim>();
            _ballReference.gameObject.SetActive(false);
            _ballReference.ResetObject();
            SubPhaseTransition(GameplayPhases.Evaluate);
        }
    }

    public void ReportGravityObjectCollision() 
    {
        //restart level
    }

    public override void EndPhase()
    {
        base.EndPhase();
        _spawnedLevelInstance.SetActive(false);
    }
}
