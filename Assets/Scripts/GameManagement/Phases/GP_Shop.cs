using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GP_Shop : GamePhase
{

    public void TriggerBackButton()
    {
        GameManager.instance.DoPhaseTransition(GameManager.GamePhases.LevelSelect);
    }



    public void PurchaseItem(int index) 
    {
        if (CanAfford(index)) 
        {
            Debug.Log("Can Afford... completing purchase...");
            bool success = GameDataManager.instance.AttemptPurchase(index);
            if (success)
                AudioManager.instance.TriggerSFX(AudioManager.AudioKeys.Purchase);
            //at this point, I don't do anything if failed
        }
    }
    bool CanAfford(int index) 
    {
        return true; //GameDataManager.instance.AttemptPurchase already checks if we can afford, so eh. 
    }
}
