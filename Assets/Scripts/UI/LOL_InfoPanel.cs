using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LOL_InfoPanel : InfoPanel
{
    public override void OnTTSClicked()
    {
        base.OnTTSClicked();
        if (infoTextHelper.localizationId != "") 
        {
#if UNITY_EDITOR
            GameManager.instance.ttsManager.TextToSpeech(infoTextHelper.ProTextTarget.text);
            Debug.Log("TTS received: " + infoTextHelper.ProTextTarget.text);
#else
            GameManager.instance.ttsManager.TextToSpeech(infoTextHelper.localizationId);
            Debug.Log("TTS received: " + infoTextHelper.localizationId);
#endif

        }
    }
}
