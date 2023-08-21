using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private List<InventoryItem> _itemList;
    public int CurrentIndex { get; private set; } = 0;
    public InventoryItem SelectedItem { get; private set; }
    public event Action<int> OnSwitchInventoryItem;

    public Inventory(List<InventoryItem> itemList)
    {
        _itemList = itemList;
        SelectedItem = _itemList[CurrentIndex];
    }

    public void AddItem(InventoryItem item)
    {
        _itemList.Add(item);
    }

    public List<InventoryItem> GetItemList()
    {
        return _itemList;
    }

    // add input action to trigger switch inventory item
    public void SwitchInventoryItem()
    {
        while (CurrentIndex <= _itemList.Count - 1)
        {
            if (CurrentIndex == _itemList.Count - 1)
            {
                CurrentIndex = 0;
            }
            else
            {
                CurrentIndex += 1;
            }
            SelectedItem = _itemList[CurrentIndex];
            OnSwitchInventoryItem?.Invoke(CurrentIndex);
        }
    }
}
