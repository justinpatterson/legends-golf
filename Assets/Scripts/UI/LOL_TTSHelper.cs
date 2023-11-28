using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LOL_TTSHelper : MonoBehaviour
{
    public void OnTTSButtonPressed(string id) 
    {
        GameManager.instance.ttsManager.TextToSpeech(id);

    }
}
