using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ES_ToEquip : EquipSlot
{
    public InventoryObject.ItemTypes targetType;


    public override void AddItemToSlot(EquipElement item)
    {
        base.AddItemToSlot(item);
        GameDataManager.instance.gameData.playerData.EquipItem(item.inventoryReference);
    }
    public override void RemoveItemFromSlot(EquipElement item)
    {
        base.RemoveItemFromSlot(item);
        InventoryObject io = new InventoryObject();
        io.itemType = item.inventoryReference.itemType;
        io.itemIndex = -1;
        GameDataManager.instance.gameData.playerData.EquipItem(io);
    }
    public override bool IsValidSlotForItem(EquipElement item)
    {
        return item.inventoryReference.itemType == targetType;
    }
}
