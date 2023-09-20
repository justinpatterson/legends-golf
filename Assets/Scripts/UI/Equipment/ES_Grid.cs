using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ES_Grid : EquipSlot
{
    public override void AddItemToSlot(EquipElement item)
    {
        base.AddItemToSlot(item);
        //if there's a grid, that means the slot is the overall inventory group and not the equipment section. 
        //item.transform.SetSiblingIndex(item.inventoryReference.itemIndex);
    }
}
