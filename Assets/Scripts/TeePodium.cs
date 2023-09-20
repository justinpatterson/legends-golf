using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeePodium : MoveableObject
{
    //public SpriteRenderer characterSprite;
    //public Sprite characterIdle, characterSwing;
    public CharacterViewController characterViewCtr;
    private void Start()
    {
        GP_Gameplay.OnGameplaySubPhaseStarted += GameplaySubphaseStarted;
    }

    private void GameplaySubphaseStarted(GP_Gameplay.GameplayPhases subPhase)
    {
        characterViewCtr.SetCharacterState(
            (subPhase == GP_Gameplay.GameplayPhases.EditorMode || subPhase == GP_Gameplay.GameplayPhases.Load) ? 
            CharacterViewController.CharacterStates.Idle 
            : CharacterViewController.CharacterStates.Swing 
        );
        //characterSprite.sprite = (subPhase == GP_Gameplay.GameplayPhases.EditorMode || subPhase == GP_Gameplay.GameplayPhases.Load) ? characterIdle : characterSwing;
    }
    private void OnDestroy()
    {
        GP_Gameplay.OnGameplaySubPhaseStarted -= GameplaySubphaseStarted;
    }
    protected override void TriggerBehavior(Collider2D collision)
    {
        //Podium doesn't need to have collisions
    }
}
