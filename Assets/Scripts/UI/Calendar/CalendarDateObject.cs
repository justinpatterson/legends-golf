using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CalendarDateObject : MonoBehaviour
{
    public Button goButton;
    public TextMeshProUGUI goButtonText;
    public LevelSelectPanel levelSelectPanel;
    public int levelIndexRef = 0;
    public TextMeshProUGUI TMPLabel;
    public string strLabel = "";

    public Image[] stars;
    public Sprite star_empty, star_full;

    private void Awake()
    {
        if (goButton)
            goButton.onClick.AddListener(() => { OnButtonClick(); });

        if (TMPLabel) 
        {
            TMPLabel.text = strLabel; 
        }
    }
    private void OnEnable()
    {
        RefreshStars();
        RefreshPlayButtonVisibility(); //probably not great, as we can't guarantee the order these happen ON START. 

#if UNITY_EDITOR
        goButton.interactable = true;
        Debug.Log("Should enable...");
#endif
    }

    void RefreshStars() 
    {
        bool success = false;
        GameDataManager.GameData.LevelData myLevel = GameDataManager.instance.GetLevelData(levelIndexRef, out success);
        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].sprite = i < myLevel.starCount ? star_full : star_empty;
        }
    }
    void RefreshPlayButtonVisibility() 
    {

        bool currentSuccess = false;
        bool locked = false;
        GameDataManager.GameData.LevelData currentLevel = GameDataManager.instance.GetLevelData((levelIndexRef), out currentSuccess);

        if (levelIndexRef > 0)
        {
            bool previousSuccess = false;
            GameDataManager.GameData.LevelData previousLevel = GameDataManager.instance.GetLevelData((levelIndexRef-1), out previousSuccess);
            locked = (previousLevel.starCount <= 0);
            goButton.interactable = !locked; 
        }
        else 
        {
            goButton.interactable = true;
        }
        goButtonText.text = (currentLevel.starCount > 0) ? "REPLAY" : (locked ? "LOCKED":"PLAY");
    }
    void OnButtonClick()  
    {
        if (levelSelectPanel)
            levelSelectPanel.ReportLevelLaunchPressed(levelIndexRef);
    }
}
