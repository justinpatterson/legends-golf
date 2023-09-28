using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterViewController : MonoBehaviour
{

    public SpriteRenderer characterRenderer;
    public enum CharacterStates
    {
        Idle, Swing    
    }
    [System.Serializable]
    public struct CharacterStateSprites
    {
        public CharacterStates state;
        public Sprite sprite;
    }
    [SerializeField]
    public CharacterStateSprites[] charSprites;

    public CharacterViewController[] subCharacters;
    
    private void Awake()
    {
        InitializeCharacter();
    }
    protected virtual void InitializeCharacter() 
    {
        if (GameDataManager.instance.gameData.playerData.customCharId != -1)
        {
            int charTargetId = GameDataManager.instance.gameData.playerData.customCharId;
            InventoryObject io = GameDataManager.instance.GetInventoryItem(charTargetId);
            SetCharacterSprite(CharacterStates.Idle, io.sprites[0]);
            SetCharacterSprite(CharacterStates.Swing, io.sprites[1]);
            SetCharacterState(CharacterStates.Idle);
        }
    }
    public void SetCharacterState(CharacterStates state) 
    {
        //eventually this could be in an animator to play a sprite series, but we'll both change a state AND swap textures here.
        characterRenderer.sprite = GetCharacterSprite(state);
        foreach (CharacterViewController controller in subCharacters) 
        {
            controller.SetCharacterState(state);
        }
    }
    protected void SetCharacterSprite(CharacterStates state, Sprite sprite)
    {
        for (int i = 0; i < charSprites.Length; i++)
        {
            if (charSprites[i].state == state)
            {
                charSprites[i].sprite = sprite;
                return;
            }
        }
    }

    protected Sprite GetCharacterSprite(CharacterStates state) 
    {
        Sprite returnSprite = null;
        foreach(CharacterStateSprites charSp in charSprites) 
        {
            if (charSp.state == state)
                return charSp.sprite;
        }
        return returnSprite;
    }
}
