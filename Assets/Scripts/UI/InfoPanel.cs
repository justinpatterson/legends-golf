using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Localization;
using UnityEngine.UI;

public class InfoPanel : UIPanel
{
    public LocalizationHelper infoTextHelper;
    public Image infoImage;
    public Transform infoDrawingPivot;
    public GameObject _spawnedDrawing;
    public override void OpenPanel()
    {
        //timescale does technically break the cute animation
        Time.timeScale = 0f;
        GP_Gameplay gp = GameManager.instance.GetCurrentGamePhase() as GP_Gameplay;
        if (gp != null)
        {
            LevelObject lo = gp.levels[GP_Gameplay.levelIndexSelected];
            if (infoTextHelper != null)
                infoTextHelper.RefreshLocalization(lo.infoScreenKey);

            if (_spawnedDrawing)
                Destroy(_spawnedDrawing);

            if (infoDrawingPivot != null && lo.infoScreenDrawingPrefab != null) 
            {
                _spawnedDrawing = Instantiate(lo.infoScreenDrawingPrefab, infoDrawingPivot);
            }
        }
        base.OpenPanel();
    }
    public override void ClosePanel()
    {
        Time.timeScale = 1f;
        base.ClosePanel();
    }
    public virtual void OnTTSClicked()
    {

    }

}
