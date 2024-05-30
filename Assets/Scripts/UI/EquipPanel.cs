using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipPanel : UIPanel
{
    public Transform inventorySection;
    public ES_ToEquip[] equipmentSlots;
    public GameObject equipmentElementPrefab;
    public override void OpenPanel()
    {
        base.OpenPanel();
        InitializeEquipment();
        InitializeInventory();
    }
    public override void ClosePanel()
    {
        base.ClosePanel();
        ClearAll();
    }
    public void ReportBackButtonPressed()
    {
        GamePhase gamePhase = GameManager.instance.GetCurrentGamePhase();
        GP_Customization gpCast = (GP_Customization)gamePhase;
        if (gpCast!= null)
        {
            Debug.Log("SHOULD GO BACK");
            gpCast.TriggerBackButton();
        }
        //GameManager.instance.DoPhaseTransition(GameManager.GamePhases.LevelSelect);
    }
    void ClearAll()
    {
        foreach (ES_ToEquip slot in equipmentSlots)
        {
            if (slot.currentItem)
            {
                Destroy(slot.currentItem.gameObject);
                slot.currentItem = null;
            }
        }
        foreach(Transform t in inventorySection.transform) 
        {
            ES_Grid slot = t.GetComponent<ES_Grid>();
            if(slot != null) 
            {
                if (slot.currentItem) 
                {
                    Destroy(slot.currentItem.gameObject);
                    slot.currentItem = null;
                }
            }
        }
    }
    void InitializeEquipment()
    {
        if (GameDataManager.instance.gameData.playerData.customBallId!=-1)
        {
            int ballIndex = GameDataManager.instance.gameData.playerData.customBallId;
            InventoryObject io = GameDataManager.instance.GetInventoryItem(ballIndex);
            foreach (ES_ToEquip slot in equipmentSlots)
            {
                if (slot.targetType == InventoryObject.ItemTypes.BallColor)
                {
                    GameObject elementInst = Instantiate(equipmentElementPrefab);
                    EquipElement element = elementInst.GetComponent<EquipElement>();
                    element.InitializeElement(io);
                    element.currentSlot = slot;
                    slot.AddItemToSlot(element);
                    element.transform.SetParent(slot.transform, false);
                    element.transform.localPosition = Vector3.zero;
                    elementInst.name = "INSTANCE";
                }
            }
        }
        if (GameDataManager.instance.gameData.playerData.customCharId!=-1)
        {
            int charIndex = GameDataManager.instance.gameData.playerData.customCharId;
            InventoryObject io = GameDataManager.instance.GetInventoryItem(charIndex);
            foreach (ES_ToEquip slot in equipmentSlots)
            {
                if (slot.targetType == InventoryObject.ItemTypes.Character)
                {
                    GameObject elementInst = Instantiate(equipmentElementPrefab);
                    EquipElement element = elementInst.GetComponent<EquipElement>();
                    element.InitializeElement(io);
                    element.currentSlot = slot;
                    slot.AddItemToSlot(element);
                    element.transform.SetParent(slot.transform, false);
                    element.transform.localPosition = Vector3.zero;
                    elementInst.name = "INSTANCE";
                }
            }
        }
        if (GameDataManager.instance.gameData.playerData.customPetId!=-1)
        {
            int petIndex= GameDataManager.instance.gameData.playerData.customPetId;
            InventoryObject io = GameDataManager.instance.GetInventoryItem(petIndex);
            foreach (ES_ToEquip slot in equipmentSlots)
            {
                if (slot.targetType == InventoryObject.ItemTypes.Pet)
                {
                    GameObject elementInst = Instantiate(equipmentElementPrefab);
                    EquipElement element = elementInst.GetComponent<EquipElement>();
                    element.InitializeElement(io);
                    element.currentSlot = slot;
                    slot.AddItemToSlot(element);
                    element.transform.SetParent(slot.transform, false);
                    element.transform.localPosition = Vector3.zero;
                    elementInst.name = "INSTANCE";
                }
            }
        }
    }
    void InitializeInventory() 
    {
        int inventoryIndex = 0;
        foreach(int key in GameDataManager.instance.gameData.purchaseData.purchaseMap.Keys)
        {
            if (key == GameDataManager.instance.gameData.playerData.customBallId || key == GameDataManager.instance.gameData.playerData.customCharId || key == GameDataManager.instance.gameData.playerData.customPetId) { } //these will go in equip section, not grid
            else 
            {
                if(GameDataManager.instance.gameData.purchaseData.HasPurchasedItem(key))
                {
                    InventoryObject io = GameDataManager.instance.masterInventoryList[key]; //right now, key is the index of this array. could make it nicer
                    GameObject elementInst = Instantiate(equipmentElementPrefab);
                    EquipElement element = elementInst.GetComponent<EquipElement>();
                    element.InitializeElement(io);
                    ES_Grid slot = inventorySection.GetChild(inventoryIndex).GetComponent<ES_Grid>();
                    element.currentSlot = slot;
                    slot.AddItemToSlot(element);
                    element.transform.SetParent(slot.transform, false);
                    element.transform.localPosition = Vector3.zero;
                    elementInst.name = "INSTANCE";
                    
                    inventoryIndex++;
                    if (inventoryIndex >= inventorySection.childCount)
                        return;
                }
            }
        }    
    }
}
