using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TutorialObject", order = 1)]
public class TutorialElement : ScriptableObject
{
    [SerializeField]
    public Sprite tutImage;

    [SerializeField]
    public string tutText;


    [SerializeField]
    public bool autoProgress;
    public TutorialElement(Sprite s, string t, bool p)
    {
        tutImage = s;
        tutText = t;
        autoProgress = p;
    }
}
