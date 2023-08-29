using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GP_Gameplay : GamePhase
{
    [SerializeField]
    GameObject _spawnedLevelInstance; //eventually this will be spawned from PF
    [SerializeField]
    GameObject levelPrefabReference;

    public enum GameplayPhases { Load, EditorMode, Launch, Evaluate }
    public GameplayPhases currentGameplayPhase = GameplayPhases.Load;

    public delegate void GameplaySubphaseTransition(GameplayPhases subPhase);
    public static GameplaySubphaseTransition OnGameplaySubPhaseStarted;
    bool _startup = false;
    public override void StartPhase()
    {
        base.StartPhase();
        _startup = true;//getting around a UI race condition for listening to phase transitions
    }
    public override void UpdatePhase()
    {
        base.UpdatePhase();
        if (_startup) //getting around a UI race condition for listening to phase transitions
            SubPhaseUpdate();
    }
    public void SubPhaseTransition(GameplayPhases phase) 
    {
        if(currentGameplayPhase != phase) 
        {
            SubPhaseEnd();
        }
        currentGameplayPhase = phase;
        SubPhaseStart();
    }

    void SubPhaseStart() 
    {
        switch (currentGameplayPhase)
        {
            case GameplayPhases.Load:
                _spawnedLevelInstance = Instantiate(levelPrefabReference, Vector3.zero, Quaternion.identity) as GameObject;
                _spawnedLevelInstance.SetActive(true);
                SubPhaseTransition(GameplayPhases.EditorMode);
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
                //Calculate score before going to RESULTS
                GameManager.instance.DoPhaseTransition(GameManager.GamePhases.Results);
                break;
        }
        Debug.Log("STARTING SUBPHASE " + currentGameplayPhase.ToString());
        OnGameplaySubPhaseStarted?.Invoke(currentGameplayPhase);
    }
    void SubPhaseUpdate() 
    {
        if (_startup) //race condition if we do it all on StartPhase(); So we essentially need to wait a frame before loading and going into editor.
        {
            _startup = false;
            SubPhaseTransition(GameplayPhases.Load);
        }
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
            _ballReference.ResetObject();
            _ballReference.gameObject.SetActive(false);
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
        Destroy(_spawnedLevelInstance);
    }
}
