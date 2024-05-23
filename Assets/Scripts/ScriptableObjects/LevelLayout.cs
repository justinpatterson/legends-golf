using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/LevelLayout", order = 1)]
public class LevelLayout : ScriptableObject
{
    [SerializeField]
    public struct LevelComponent 
    {
        public string prefabId;
        public Vector3 position;
        public Vector3 rotation;
        public Vector3 scale;
    }

    public LevelComponent[] levelComponents;
}
