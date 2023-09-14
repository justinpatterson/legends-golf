using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ResultsPanel : UIPanel
{
    public Button confirmButton;
    public Image[] resultStars;
    public TMPro.TextMeshProUGUI descriptionText;
    public AnimationCurve starFillCurve;
    public override void OpenPanel()
    {
        ResetStars();
        base.OpenPanel();
        confirmButton.onClick.RemoveAllListeners();
        confirmButton.onClick.AddListener(() => OnResultsClicked());
        //probably populate some Angry Birds Stars or something 
        ShowStars();
    }

    void ResetStars() 
    {
        Debug.Log("Reset stars...");
        foreach(Image rs in resultStars) { rs.enabled = false; }
    }
    void ShowStars() 
    {
        Debug.Log("Show Stars...");
        bool success = false;
        GameDataManager.GameData.LevelData levelData = GameDataManager.instance.GetLevelData(GP_Gameplay.levelIndexSelected, out success);
        if (success) 
        {
            Debug.Log("Found level data... " + levelData.levelIndex);
            for (int i = 0; i < resultStars.Length; i++) 
            {
                resultStars[i].enabled = (i < levelData.starCount);
                StartCoroutine(ShowStarRoutine(i, (i < levelData.starCount)));
                //resultStars[i].sprite = (i < levelData.starCount) ? star_full : star_empty;            
            }
            descriptionText.text = (levelData.starCount == 3) ? "Hole in One!" :
                 (levelData.starCount == 2) ? "Eagle!" :
                 (levelData.starCount == 1) ? "Par" : "ERROR"; //zero stars shouldn't be possible. 
        }
        else { Debug.Log("No level data found for " + GP_Gameplay.levelIndexSelected); }
    }

    IEnumerator ShowStarRoutine(int index, bool isFilled)
    {
        resultStars[index].transform.localScale = Vector3.zero;
        if (isFilled)
        {
            yield return new WaitForSeconds((float)index * 0.2f);
            float percent = 0f;
            while (percent <= 1f) 
            {
                percent += Time.deltaTime * 2f;
                resultStars[index].transform.localScale = Vector3.one * starFillCurve.Evaluate(percent);
                yield return new WaitForEndOfFrame();
            }
            resultStars[index].transform.localScale = Vector3.one;
        }
        else 
        {
        }
        yield return null;
    }

    void OnResultsClicked() 
    {
        GameManager.instance.DoPhaseTransition(GameManager.GamePhases.LevelSelect);
    }

    public override void ClosePanel()
    {
        base.ClosePanel();
    }
}
