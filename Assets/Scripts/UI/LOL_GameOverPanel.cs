using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LOL_GameOverPanel : GameOverPanel
{
    public RectTransform meterContainer, meterFill;
    //public LoLConnectionMgr LoLConnectionMgr;
    public Button completeButton;
    public override void TriggerTTS()
    {
        base.TriggerTTS();
        if (localizationHelper.localizationId != "")
        {
#if UNITY_EDITOR
            GameManager.instance.ttsManager.TextToSpeech(localizationHelper.ProTextTarget.text);
            Debug.Log("TTS received: " + localizationHelper.ProTextTarget.text);
#else
            GameManager.instance.ttsManager.TextToSpeech(localizationHelper.localizationId);
            Debug.Log("TTS received: " + localizationHelper.localizationId);
#endif

        }
    }

    public override void OpenPanel()
    {
        base.OpenPanel();

        //NOTE - WE MIGHT NEED TO DELAY BECAUSE OF RACE CONDITIONS
        //NOTE - Removed ties to LolConnectionMgr because it doesn't receive an update until Results screen on load
        if (true){ //LoLConnectionMgr != null) {
            if (meterContainer != null && meterFill != null)
            {
                float maxX = meterContainer.sizeDelta.x;
                float progress = GetStarProgress();
                float sizeY = meterFill.sizeDelta.y;
                meterFill.sizeDelta = new Vector2(maxX * progress, sizeY);
            }
            if (completeButton != null)
            {
                float progress = GetStarProgress();
                completeButton.interactable = progress == 1f ? true : false;
            }
        }
    }
    float GetStarProgress() 
    {
        int levelCount = GameDataManager.instance.gameData.levelDataMap.Count;
        float maxStars = 0f;
        float currStars = 0f;
        for (int i = 0; i < levelCount; i++) {
            bool success = false;
            GameDataManager.GameData.LevelData ld = GameDataManager.instance.GetLevelData(i, out success);
            if (success) 
            {
                currStars += (float) ld.starCount;
                maxStars += 3f;
            }
        }
        return currStars / maxStars;
    }
}
