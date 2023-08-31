using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CalendarDateObject : MonoBehaviour
{
    public Button goButton;
    public LevelSelectPanel levelSelectPanel;
    public int levelIndexRef = 0;
    public TextMeshProUGUI TMPLabel;
    public string strLabel = "";
    private void Awake()
    {
        if (goButton)
            goButton.onClick.AddListener(() => { OnButtonClick(); });

        if (TMPLabel) 
        {
            TMPLabel.text = strLabel; 
        }
    }
    void OnButtonClick()  
    {
        if (levelSelectPanel)
            levelSelectPanel.ReportLevelLaunchPressed(levelIndexRef);
    }
}
