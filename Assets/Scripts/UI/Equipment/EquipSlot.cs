using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipSlot : MonoBehaviour
{
    public EquipElement currentItem;
    public bool SlotFilled => currentItem;
    public virtual void AddItemToSlot(EquipElement item) 
    {
        currentItem = item;
    }
    public virtual void RemoveItemFromSlot(EquipElement item)
    {
        currentItem = null;
    }
    public virtual bool IsValidSlotForItem(EquipElement item) 
    {
        return true;
    }
}
