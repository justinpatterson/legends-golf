using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipElement : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public InventoryObject inventoryReference;
    bool _isDragging;
    // Reference to current item slot.
    public EquipSlot currentSlot;
    // Reference to the canvas.
    [SerializeField] private Canvas canvas;
    // Reference to UI raycaster.
    [SerializeField] private GraphicRaycaster graphicRaycaster;

    public Image iconImg;
    Canvas mainCanvas;
    /// <summary>
    /// IBeginDragHandler
    /// Method called on drag begin.
    /// </summary>
    /// <param name="eventData">Event data.</param>
    private void Awake()
    {
        mainCanvas = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<Canvas>();
    }

    public void InitializeElement(InventoryObject item) 
    {
        inventoryReference = item;
        if (iconImg != null)
        {
            if (inventoryReference.iconSprite != null)
                iconImg.sprite = inventoryReference.iconSprite;
            else
                iconImg.sprite = inventoryReference.sprites[0];
        }
    }

    Vector3 CalculateOffsetFromPointerDelta(Vector2 delta) 
    {
        return new Vector3(delta.x, delta.y, 0) / mainCanvas.scaleFactor;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        // Start moving object from the beginning!
        transform.localPosition += CalculateOffsetFromPointerDelta(eventData.delta);// (transform.lossyScale.x); // Thanks to the canvas scaler we need to devide pointer delta by canvas scale to match pointer movement.
        // We need a few references from UI.
        if (!canvas)
        {
            canvas = GetComponentInParent<Canvas>();
            graphicRaycaster = canvas.GetComponent<GraphicRaycaster>();
        }
        // Change parent of our item to the canvas.
        transform.SetParent(canvas.transform, true);
        // And set it as last child to be rendered on top of UI.
        transform.SetAsLastSibling();
    }
    public void OnDrag(PointerEventData eventData)
    {
        // Continue moving object around screen.
        transform.localPosition += CalculateOffsetFromPointerDelta(eventData.delta);// (transform.lossyScale.x); // Thanks to the canvas scaler we need to devide pointer delta by canvas scale to match pointer movement.
        //transform.position = eventData.position;

        //transform.position = Input.mousePosition;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        // On end we need to test if we can drop item into new slot.
        var results = new List<RaycastResult>();
        graphicRaycaster.Raycast(eventData, results);
        // Check all hits.
        foreach (var hit in results)
        {
            // If we found slot.
            EquipSlot slot = hit.gameObject.GetComponent<EquipSlot>();
            if (slot && slot.IsValidSlotForItem(this))
            {
                Debug.Log("Found slot...");
                // We should check if we can place ourselves​ there.
                if (!slot.SlotFilled) //if you comment out this if statement (but retain AttachToSlot below) it will have a "swap" function, but it's currently buggy
                {
                    AttachToSlot(slot);
                }
                //else 
                {
                    
                }
                // In either cases we should break check loop.
                break;
            }
        }
        // Changing parent back to slot.
        transform.SetParent(currentSlot.transform);
        
        
        // And centering item position.
        transform.localPosition = Vector3.zero;
    }
    void AttachToSlot(EquipSlot slot)
    {
        if (currentSlot!=null)
            currentSlot.RemoveItemFromSlot(this);

        EquipElement other = null;
        if (slot.SlotFilled)
        {
            other = slot.currentItem;
            slot.RemoveItemFromSlot(other);
        }

        if (other != null) 
        {
            currentSlot.AddItemToSlot(other); 
            other.transform.SetParent(currentSlot.transform, false);
            other.transform.localPosition = Vector3.zero;
        }
        
        currentSlot = slot;
        currentSlot.AddItemToSlot(this);
        //currentSlot.currentItem = this;
    }
}
