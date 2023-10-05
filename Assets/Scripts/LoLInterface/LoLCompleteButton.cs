using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LoLCompleteButton : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        Button b = GetComponent<Button>();
        if (b!=null) 
        {
            b.onClick.AddListener(TriggerCompleteGame);
        }
    }

    private void TriggerCompleteGame()
    {
        LoLSDK.LOLSDK.Instance.CompleteGame();
    }
}
