using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{
    private Inventory _inventory;
    [SerializeField] private Transform _contentContainer;
    [SerializeField] private Transform _itemSlotContainer;
    [SerializeField] private Sprite _borderSelectedSprite;
    private const float ITEM_SLOT_CELL_SIZE = 70f;

    public void SetInventory(Inventory inventory)
    {
        _inventory = inventory;
        _inventory.OnSwitchInventoryItem += SetSelectedUI;
        RefreshInventoryItems();
    }

    private void OnDisable()
    {
        _inventory.OnSwitchInventoryItem -= SetSelectedUI; 
    }

    private void SetSelectedUI(int currentIndex)
    {
        var selectedItemSlot = _contentContainer.GetChild(currentIndex);
        Image image = selectedItemSlot.Find("Border").GetComponent<Image>();
        image.sprite = _borderSelectedSprite;
    }

    private void RefreshInventoryItems()
    {
        foreach (InventoryItem item in _inventory.GetItemList())
        {
            var itemSlot = Instantiate(_itemSlotContainer, _contentContainer);
            Image image = itemSlot.Find("Icon").GetComponent<Image>();
            image.sprite = item.sprite;
        }
        
        SetSelectedUI(_inventory.CurrentIndex);

        var contentRectTransform = _contentContainer.GetComponent<RectTransform>();
        var desiredWidth = _contentContainer.childCount * ITEM_SLOT_CELL_SIZE;
        contentRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, desiredWidth);
    }
}
