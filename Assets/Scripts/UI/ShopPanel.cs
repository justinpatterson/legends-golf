using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopPanel : UIPanel
{
    public TextMeshProUGUI currencyText;
    public override void OpenPanel()
    {
        base.OpenPanel();

        if (currencyText != null)
            currencyText.text = GameDataManager.instance.gameData.playerData.currencyAmt.ToString("00");
    }

    public void ReportPurchaseClicked(int index) 
    {
        GP_Shop gp = GameManager.instance.GetCurrentGamePhase() as GP_Shop;
        if (gp != null)
        {
            Debug.Log("PURCHASING ITEM: " + index);
            gp.PurchaseItem(index);
        }
        if (currencyText != null)
            currencyText.text = GameDataManager.instance.gameData.playerData.currencyAmt.ToString("00");
    }
    public void ReportBackButtonPressed()
    {
        AudioManager.instance.TriggerSFX(AudioManager.AudioKeys.Back);

        GamePhase gamePhase = GameManager.instance.GetCurrentGamePhase();
        GP_Shop gpCast = (GP_Shop)gamePhase;
        if (gpCast!= null)
        {
            gpCast.TriggerBackButton();
        }
        //GameManager.instance.DoPhaseTransition(GameManager.GamePhases.LevelSelect);
    }
}
