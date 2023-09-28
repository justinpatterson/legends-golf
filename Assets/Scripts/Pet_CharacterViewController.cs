using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pet_CharacterViewController : CharacterViewController
{
    bool _isActive;
    protected override void InitializeCharacter()
    {
        _isActive = GameDataManager.instance.gameData.playerData.customPetId != -1;

        characterRenderer.enabled = _isActive;

        if (_isActive)
        {
            int petTargetId = GameDataManager.instance.gameData.playerData.customPetId;
            InventoryObject io = GameDataManager.instance.GetInventoryItem(petTargetId);
            SetCharacterSprite(CharacterStates.Idle, io.sprites[0]);
            SetCharacterSprite(CharacterStates.Swing, io.sprites[1]);
            SetCharacterState(CharacterStates.Idle); // <-- parent character should set state to avoid race condition, not that it should ever matter with only two states
        }
        
    }
}
