using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager instance;
    [SerializeField]
    public GameData gameData = new GameData();


    public GameData.LevelData GetLevelData(int levelIndex, out bool success)
    {
        for (int i = 0; i < gameData.levelDataMap.Count; i++)
        {
            if (gameData.levelDataMap[i].levelIndex == levelIndex)
            {
                success = true;
                return gameData.levelDataMap[i];
            }
        }

        success = false;
        GameData.LevelData ld = new GameData.LevelData();
        ld.levelIndex = levelIndex;
        ld.starCount = 0;
        gameData.levelDataMap.Add(ld);

        return ld;
    }
    public bool UpdateStarData(int levelIndex, int starCount) 
    {
        for(int i = 0; i < gameData.levelDataMap.Count; i++) 
        {
            if (gameData.levelDataMap[i].levelIndex == levelIndex) 
            {
                gameData.levelDataMap[i].starCount = starCount;
                return true;
            }
        }

        //if we reach here, no star data was found
        GameData.LevelData ld = new GameData.LevelData();
        ld.levelIndex = levelIndex;
        ld.starCount = starCount;
        gameData.levelDataMap.Add(ld);
        return false;
    }
    

    [System.Serializable]
    public class GameData
    {
        public List<LevelData> levelDataMap = new List<LevelData>();
        [SerializeField]
        public SettingsData settingsData = new SettingsData();
        [SerializeField]
        public PurchaseData purchaseData = new PurchaseData();
        [SerializeField]
        public PlayerData playerData = new PlayerData();    

        [System.Serializable]
        public class LevelData
        {
            public int levelIndex = 0;
            public int starCount = -1;
            //public bool isLocked = false; //isLocked really should just be based on settings on the button itself (expecting certain levels to have certain star counts, etc)
        }

        [System.Serializable]
        public class SettingsData
        {
            
        }

        [System.Serializable]
        public class PurchaseData
        {
            public Dictionary<int, bool> purchaseMap = new Dictionary<int, bool>();

        }

        [System.Serializable]
        public class PlayerData
        {
            //maybe current equipment indexes for golf club, etc.
            public int currencyAmt = 0;
        }
    }

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            gameData = new GameData();
        }
        else
            Destroy(gameObject);
    }
}
