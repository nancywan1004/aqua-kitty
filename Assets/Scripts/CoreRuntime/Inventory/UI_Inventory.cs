using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory<T> : MonoBehaviour where T : InventoryItem
{
    private Inventory<T> _inventory;
    [SerializeField] private Transform _contentContainer;
    [SerializeField] private Transform _itemSlotContainer;
    [SerializeField] private Sprite _borderSelectedSprite;
    [SerializeField] private Sprite _borderDeselectedSprite;
    private const float ITEM_SLOT_CELL_SIZE = 70f;

    public void InitInventory(Inventory<T> inventory)
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
        if (currentIndex == 0)
        {
            SetDeselectedUI(_inventory.ItemCount - 1);
        }
        else
        {
            SetDeselectedUI(currentIndex - 1);
        }
        var selectedItemSlot = _contentContainer.GetChild(currentIndex);
        Image image = selectedItemSlot.Find("Border").GetComponent<Image>();
        image.sprite = _borderSelectedSprite;
    }

    private void SetDeselectedUI(int previousIndex)
    {
        var deselectedItemSlot = _contentContainer.GetChild(previousIndex);
        Image image = deselectedItemSlot.Find("Border").GetComponent<Image>();
        image.sprite = _borderDeselectedSprite;
    }

    private void RefreshInventoryItems()
    {
        foreach (var item in _inventory.GetItemList())
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
