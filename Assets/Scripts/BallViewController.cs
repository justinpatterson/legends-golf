using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallViewController : MonoBehaviour
{
    public SpriteRenderer ballSpriteRenderer;
    private void Awake()
    {
        if (GameDataManager.instance.gameData.playerData.customBallEquipped != null) 
        {
            ballSpriteRenderer.sprite = GameDataManager.instance.gameData.playerData.customBallEquipped.sprites[0];
        }
    }
}
