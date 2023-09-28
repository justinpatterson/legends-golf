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

        public void StartTutorial(TutorialElement[] tutorial) 
        {
            currentTutIndex = 0;
            tutorialSteps = tutorial;
            IterateOrEndTutorial();
        }
        public bool TutorialInProgress() 
        {
            return currentTutIndex >= tutorialSteps.Length;
        }
        int GetCurrentTutorialStep()
        {
            return currentTutIndex;
        }
        
        public void TriggerTutorialIteration() 
        {
            if (_lastTutorial.autoProgress)
            {
                ++currentTutIndex;
                IterateOrEndTutorial();
            }
            else 
            {
                //in other games, we'd wait to progress until certain actions were performed.
            }
        }

        void IterateOrEndTutorial()
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

        public TutorialElement GetCurrentTutorialElement() 
        {
            if (_lastTutorial != null)
                return _lastTutorial;
            TutorialElement tutorialElement = null;
            return tutorialElement;
        }
    }
