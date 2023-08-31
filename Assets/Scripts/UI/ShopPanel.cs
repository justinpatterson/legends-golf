using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPanel : UIPanel
{
    public void ReportBackButtonPressed()
    {
        GamePhase gamePhase = GameManager.instance.GetCurrentGamePhase();
        GP_Shop gpCast = (GP_Shop)gamePhase;
        if (gpCast!= null)
        {
            gpCast.TriggerBackButton();
        }
        //GameManager.instance.DoPhaseTransition(GameManager.GamePhases.LevelSelect);
    }
}
