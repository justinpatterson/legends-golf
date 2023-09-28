using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LoLSDK;
using UnityEngine.UI;

public class LoL_TutorialPanel : TutorialPanel
{
    public Button ttsButton;

    public override void TriggerTTS()
    {
        TutorialElement te = GameManager.instance.tutorialManager.GetCurrentTutorialElement();
        if (te!=null)
        {
#if UNITY_EDITOR
            GameManager.instance.ttsManager.TextToSpeech(te.tutText);
            Debug.Log("TTS received: " + te.tutText);
#else
            GameManager.instance.ttsManager.TextToSpeech(te.tutText_locKey);
            Debug.Log("TTS received: " + te.tutText_locKey);
#endif
        }
    }
}
