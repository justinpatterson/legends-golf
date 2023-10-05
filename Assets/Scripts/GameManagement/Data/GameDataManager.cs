using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager instance;
    [SerializeField]
    public GameData gameData = new GameData();

    public InventoryObject[] masterInventoryList;
    private void Update()
    {
#if UNITY_EDITOR
        //just testing that we can equip an item
        if (Input.GetKeyDown(KeyCode.E)) 
        {
            InventoryObject io = masterInventoryList[0];
            gameData.playerData.EquipItem(io);
        }
#endif
    }
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
        for (int i = 0; i < gameData.levelDataMap.Count; i++)
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

    public bool AttemptPurchase(int itemIndex) 
    {   
        if( masterInventoryList.Length > itemIndex) 
        {
            int cost = masterInventoryList[itemIndex].itemCost;
            if( gameData.playerData.currencyAmt >= cost) 
            {
                gameData.playerData.currencyAmt -= cost;
                gameData.purchaseData.SetPurchasedItem(itemIndex, true);
                return true;
            }
        }
        return false;
    }

    public InventoryObject GetInventoryItem(int id) 
    {
        //for now, index and array location should match. But let's assume that's not always the case.
            //if size exceeds 20, let's shift this to a dictionary.
        for(int i = 0; i < masterInventoryList.Length; i++) 
        {
            if(masterInventoryList[i].itemIndex == id) { return masterInventoryList[i]; }
        }
        return null;
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

            public bool HasPurchasedItem(int index)
            {
                bool hasKey = GameDataManager.instance.gameData.purchaseData.purchaseMap.ContainsKey(index);
                if (hasKey) 
                {
                    return GameDataManager.instance.gameData.purchaseData.purchaseMap[index];
                }
                else 
                {
                    GameDataManager.instance.gameData.purchaseData.purchaseMap.Add(index, false);
                    return false;
                }
            }
            public void SetPurchasedItem(int index, bool purchased) 
            {
                bool hasKey = GameDataManager.instance.gameData.purchaseData.purchaseMap.ContainsKey(index);
                if (hasKey)
                {
                    GameDataManager.instance.gameData.purchaseData.purchaseMap[index] = purchased;
                }
                else
                {
                    GameDataManager.instance.gameData.purchaseData.purchaseMap.Add(index, purchased);
                }
            }
        }

        [System.Serializable]
        public class PlayerData
        {
            //maybe current equipment indexes for golf club, etc.
            public int currencyAmt = 0;
            public int customBallId = -1;
            public int customCharId = -1;
            public int customPetId = -1;

            public void EquipItem(InventoryObject io) 
            {
                if (io.itemType == InventoryObject.ItemTypes.BallColor) 
                {
                    customBallId = io.itemIndex;
                }
                else if (io.itemType == InventoryObject.ItemTypes.Character)
                {
                    customCharId = io.itemIndex;
                }
                else if (io.itemType == InventoryObject.ItemTypes.Pet)
                {
                    customPetId = io.itemIndex;
                }
            }
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
