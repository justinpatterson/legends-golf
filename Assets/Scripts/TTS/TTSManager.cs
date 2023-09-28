using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using LoLSDK;
using System;
using UnityEngine.SceneManagement;

public class TTSManager : MonoBehaviour
{
    protected AudioSource audioSpeaker;
    public void Start()
    {
        audioSpeaker = GetComponent<AudioSource>();
    }
    public virtual void TextToSpeech(string Text)
    {
        Debug.Log("TTS Is Not Implemented.");
    /*
#if UNITY_EDITOR
        LOLSDK.Instance.SpeakText("hot sauce is very spicy to me.");
        ((ILOLSDK_EDITOR)LOLSDK.Instance.PostMessage).SpeakText(Text,
        clip => { audioSpeaker.clip = clip; audioSpeaker.Play(); },
        this);
#else
        Debug.Log("Received instruction for TTS: " + Text);
		LOLSDK.Instance.SpeakText(Text);
#endif
    */
    }
}
