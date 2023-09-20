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
    //public GameObject[] levelPrefabs;
    public LevelObject[] levels;
    public static int levelIndexSelected = 0;

    public enum GameplayPhases { Load, EditorMode, Launch, Evaluate }
    public GameplayPhases currentGameplayPhase = GameplayPhases.Load;

    public delegate void GameplaySubphaseTransition(GameplayPhases subPhase);
    public static GameplaySubphaseTransition OnGameplaySubPhaseStarted;
    public delegate void GameplayTimerUpdate(float time);
    public static GameplayTimerUpdate OnTimeUpdated;
    bool _startup = false;
    public float countdown = 5f;
    public float maxCountdown = 10f;

    Dictionary<string,int> _levelCollectables = new Dictionary<string,int>();

    public override void StartPhase()
    {
        base.StartPhase();
        _startup = true;//getting around a UI race condition for listening to phase transitions
        AudioManager.instance.TriggerMusic(AudioManager.AudioKeys.Gameplay);
    }
    public override void UpdatePhase()
    {
        base.UpdatePhase(); 
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

                if (_spawnedLevelInstance != null)
                    Destroy(_spawnedLevelInstance);

                if (levelIndexSelected > levels.Length)
                    levelIndexSelected = 0;

                strokeCount = 0;

                levelPrefabReference = levels[levelIndexSelected].levelPrefab;

                _spawnedLevelInstance = Instantiate(levelPrefabReference, Vector3.zero, Quaternion.identity) as GameObject;
                _spawnedLevelInstance.SetActive(true);

                _levelCollectables.Clear();

                SubPhaseTransition(GameplayPhases.EditorMode);
                break;
            case GameplayPhases.EditorMode:
                {
                    if (strokeCount == 0 && GameManager.instance.GetComponent<TutorialManager>() != null) 
                    {
                        GameManager.instance.GetComponent<TutorialManager>().StartTutorial(levels[levelIndexSelected].tutorialElements);
                    }
                    if (_ballReference) { }
                    else
                    {
                        //GravitySim gs = FindObjectOfType<GravitySim>();
                        _ballReference = FindObjectOfType<GravitySim>();
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
                    strokeCount++;
                    countdown = maxCountdown;
                    //maybe turn this into a subscription event
                    if (_ballReference == null)
                    {
                        _ballReference = FindObjectOfType<GravitySim>();
                    }

                    if (_ballReference)
                    {
                        Debug.Log("Shoot!");
                        _ballReference.Shoot();
                    }
                    else
                        Debug.Log("Can't shoot!");

                    AudioManager.instance.TriggerSFX(AudioManager.AudioKeys.Hit);
                }
                GoalObject.OnGoalTriggered += ReportGoalTriggered;
                break;
            case GameplayPhases.Evaluate:
                //Calculate score before going to RESULTS
                int starScore = 3;
                if (strokeCount == 1) { starScore = 3; }
                else if (strokeCount < levels[levelIndexSelected].parCount) { starScore = 2; }
                else starScore = 1;
                GameDataManager.instance.UpdateStarData(levelIndexSelected, starScore);
                GameDataManager.instance.gameData.playerData.currencyAmt += ((starScore)*2);

                //ResolveCollectedItems() is run in SetUpEvaluation(TRUE)
                _levelCollectables.Clear();

                GameManager.instance.DoPhaseTransition(GameManager.GamePhases.Results);
                break;
        }
        Debug.Log("STARTING SUBPHASE " + currentGameplayPhase.ToString());
        OnGameplaySubPhaseStarted?.Invoke(currentGameplayPhase);
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
                countdown-=Time.deltaTime;
                OnTimeUpdated?.Invoke(countdown);
                if (countdown<=0f) 
                {
                    SetUpEvaluation(false);   
                }
                break;
            case GameplayPhases.Evaluate:
                break;
        }
        if (_startup) //race condition if we do it all on StartPhase(); So we essentially need to wait a frame before loading and going into editor.
        {
            _startup = false;
            SubPhaseTransition(GameplayPhases.Load);
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
    Coroutine _goalSuctionRoutine;
    bool goalInProgress = false;
    private void ReportGoalTriggered(GoalObject goal, Collider2D collision)
    {
        Debug.Log("Goal Triggered!");
        if (_goalSuctionRoutine != null || goalInProgress == true)
            return;

        Debug.Log("Starting Routine!");
        if (collision.gameObject.GetComponent<GravitySim>() != null) 
        {
            _ballReference = collision.gameObject.GetComponent<GravitySim>();
            goalInProgress = true;
            _goalSuctionRoutine = StartCoroutine(SuctionRoutine(goal, _ballReference));
            AudioManager.instance.TriggerSFX(AudioManager.AudioKeys.Suction);
            //SetUpEvaluation(true);
        }
    }
    IEnumerator SuctionRoutine(GoalObject goal, GravitySim gs) 
    {
        Debug.Log("SUCTION!");
        gs.SetGoalSuction(true);
        
        while (gs.IsSuctionComplete() == false)
            yield return new WaitForEndOfFrame();

        SetUpEvaluation(true);
        _goalSuctionRoutine = null;
        goalInProgress = false;
        yield return null;
    }


    public static int strokeCount = 0;
    public void SetUpEvaluation(bool isSuccess) 
    {
        if (_ballReference != null)
        {
            Debug.Log("Reset ball...");
            _ballReference.ResetObject();
            _ballReference.gameObject.SetActive(false);
        }
        if (isSuccess) 
        {
            ResolveCollectedItems();
            SubPhaseTransition(GameplayPhases.Evaluate);
        }
        else 
        {
            //we can't "fail" in an educational game for LoL, let them stroke again.
            SubPhaseTransition(GameplayPhases.EditorMode);
        }
    }

    public void ReportGravityObjectCollision() 
    {
        //restart level
        SetUpEvaluation(false);
    }

    public void ReportCollectibleGathered(string id, int amt) 
    {
        AudioManager.instance.TriggerSFX(AudioManager.AudioKeys.Collect);
        if (_levelCollectables.ContainsKey(id)) 
        {
            int og_amt = _levelCollectables[id];
            _levelCollectables[id] = (og_amt + amt);
        }
        else
            _levelCollectables.Add(id,amt);
    }
    void ResolveCollectedItems() 
    {
        foreach(string item in _levelCollectables.Keys) 
        {
            switch (item) 
            {
                case "coin":
                    GameDataManager.instance.gameData.playerData.currencyAmt += _levelCollectables[item];
                    break;
                default:
                    break;
            }
        }
    }

    public override void EndPhase()
    {
        base.EndPhase();
        Destroy(_spawnedLevelInstance);
    }
}
