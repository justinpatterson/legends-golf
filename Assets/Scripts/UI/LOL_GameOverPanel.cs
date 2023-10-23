using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LOL_GameOverPanel : GameOverPanel
{
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
}
