using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LoLSDK;
public class LOL_TTSManager : TTSManager
{
    public override void TextToSpeech(string Text)
    {
        Debug.Log("Will attempt to TTS: " + Text);
#if UNITY_EDITOR
        LOLSDK.Instance.SpeakText(Text);
        ((ILOLSDK_EDITOR)LOLSDK.Instance.PostMessage).SpeakText(Text,
        clip => { audioSpeaker.clip = clip; audioSpeaker.Play(); },
        this);
#else
        Debug.Log("Received instruction for TTS: " + Text);
		LOLSDK.Instance.SpeakText(Text);
#endif
    }
}
