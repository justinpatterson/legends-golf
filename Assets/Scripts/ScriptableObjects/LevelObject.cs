using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/LevelObject", order = 1)]
public class LevelObject : ScriptableObject
{
    public string levelName = "Golfy Gulch";
    public int levelIndex = 0;
    public int parCount = 4;
    public int defaultDifficulty = 0; //0 is easy, 3 is hard <-- probably just modifies the length of the trajectory preview
    public GameObject levelPrefab;
    public TutorialElement[] tutorialElements;
}
