using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Localization;
using UnityEngine.UI;

public class InfoPanel : UIPanel
{
    LocalizationHelper infoTextHelper;
    Image infoImage;
    public override void OpenPanel()
    {
        GP_Gameplay gp = GameManager.instance.GetCurrentGamePhase() as GP_Gameplay;
        if (gp != null) 
        {
            LevelObject lo = gp.levels[GP_Gameplay.levelIndexSelected];
            if(infoTextHelper != null)
                infoTextHelper.RefreshLocalization(lo.infoScreenKey);
            if (infoImage != null)
                infoImage.sprite = lo.infoScreenSprite;
        }
        base.OpenPanel();
    }

}
