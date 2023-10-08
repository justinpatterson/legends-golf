using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ShopUIElement : MonoBehaviour
{
    public int masterInventoryIndex = 0;
    [SerializeField]
    ShopPanel _shopPanelRef;
    public TextMeshProUGUI title;
    public TextMeshProUGUI costText;
    public Image icon;

    public GameObject purchaseButton, purchasedContainer;

    private void Start()
    {
        RefreshView();
    }
    void RefreshView() 
    {
        if (GameDataManager.instance.masterInventoryList.Length>masterInventoryIndex) 
        {
            InventoryObject io = GameDataManager.instance.masterInventoryList[masterInventoryIndex];
            icon.sprite = io.iconSprite != null ? io.iconSprite : io.sprites[0];
            title.text = io.itemName;
            costText.text = io.itemCost.ToString("00");
            bool hasPurchased = GameDataManager.instance.gameData.purchaseData.HasPurchasedItem(masterInventoryIndex);
            purchaseButton.SetActive(!hasPurchased);
            purchasedContainer.SetActive(hasPurchased);
        }
    }

    public void OnPurchaseClicked() 
    {
        _shopPanelRef.ReportPurchaseClicked(masterInventoryIndex);
        RefreshView();
    }
}
