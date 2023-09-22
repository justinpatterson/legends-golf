using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TimerUI : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public Image timerCircle;
    public Transform timerCircleNode;
    public void SetTimerValue(float val) 
    {
        timerCircle.fillAmount = (val/30f);
        timerText.text = val.ToString("00");
        float rot = (val/30f) * 360f * -1f;
        rot+=180f;
        timerCircleNode.localRotation = Quaternion.Euler(0, 0, rot);
    }
}
