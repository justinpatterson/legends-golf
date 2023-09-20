using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/InventoryObject", order = 1)]
public class InventoryObject : ScriptableObject
{
    public string itemName = "Golfy Gulch";
    public int itemIndex = 0;
    public int itemCost = 0;
    public enum ItemTypes { BallColor, Pet, BallFX, Character };
    public ItemTypes itemType = ItemTypes.BallColor;
    public Color colorData = Color.white;
    public GameObject fxPrefab;
    public Sprite[] sprites;


}
