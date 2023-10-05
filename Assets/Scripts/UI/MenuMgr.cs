using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMgr : MonoBehaviour
{
    [SerializeField]
    public Menu[] phaseMenus;
    [System.Serializable]
    public struct Menu 
    {
        public GameManager.GamePhases phase;
        public UIPanel menu; 
    }

    public UIPanel _lastPhasePanel;
    public UIPanel gameOverPanel;
    private void Awake()
    {
        GameManager.OnPhaseTransition += PhaseTransitionMenuBehavior;
    }
    private void OnDestroy()
    {
        GameManager.OnPhaseTransition -= PhaseTransitionMenuBehavior;   
    }
    private void PhaseTransitionMenuBehavior(GameManager.GamePhases phase)
    {
        _lastPhasePanel?.ClosePanel();
        _lastPhasePanel = GetMenuForPhase(phase);
        _lastPhasePanel?.OpenPanel();

        if (phase == GameManager.GamePhases.LevelSelect)
        {
            if (GameManager.instance.HasFinishedAllLevels())
            {
                gameOverPanel.OpenPanel();
            }
        }
    }
    UIPanel GetMenuForPhase(GameManager.GamePhases phase) 
    {
        foreach(Menu pm in phaseMenus) 
        {
            if(pm.phase == phase) { return pm.menu; }
        }
        return null;
    }
}
