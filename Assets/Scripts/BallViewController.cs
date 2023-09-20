using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallViewController : MonoBehaviour
{
    public SpriteRenderer ballSpriteRenderer;
    private void Awake()
    {
        if (GameDataManager.instance.gameData.playerData.customBallId != -1) 
        {
            int itemId = GameDataManager.instance.gameData.playerData.customBallId;
            InventoryObject io = GameDataManager.instance.GetInventoryItem(itemId);
            ballSpriteRenderer.sprite = io.sprites[0];
        }
    }
}
