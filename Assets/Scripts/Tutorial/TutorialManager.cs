using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

    public class TutorialManager : MonoBehaviour
    {
    
        [SerializeField]
        TutorialElement _lastTutorial;
        [SerializeField]
        TutorialElement[] tutorialSteps;
        int currentTutIndex = 0;
        public bool firstTutorialShown = false;
        
        public bool IsTutorialInProgress() 
        {
            return currentTutIndex < tutorialSteps.Length;
        }
        public TutorialPanel tutPanel;

        public void StartTutorial()
        {            
            DoIntroTutorial();
        }
        public void StartTutorial(TutorialElement[] tutorial) 
        {
            currentTutIndex = 0;
            tutorialSteps = tutorial;
            IterateOrEndTutorial();
        }
        public void SkipIntroTutorial()
        {
            firstTutorialShown = true;
            currentTutIndex = tutorialSteps.Length;
        }
        public int GetCurrentTutorialStep()
        {
            return currentTutIndex;
        }
        
        public void DoIntroTutorial()
        {
            Debug.Log("FIRST TUTORIAL STEP...");
            if (firstTutorialShown)
                return;
            firstTutorialShown = true;
            currentTutIndex = 0;
            IterateOrEndTutorial();
        }

        public void CheckAutoProgress() 
        {
            if (_lastTutorial.autoProgress)
            {
                ++currentTutIndex;
                IterateOrEndTutorial();
            }
        }

        public void IterateOrEndTutorial()
        {
            if (currentTutIndex == tutorialSteps.Length)
            {
                Debug.Log("finished tutorial");
            }
            else
            {
                _lastTutorial = tutorialSteps[currentTutIndex];
                tutPanel.LoadTutorial(_lastTutorial);
            }
        }
    }
