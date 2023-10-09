using System;
using System.Collections.Generic;

public class Inventory<T> where T : InventoryItem
{
    protected List<T> _itemList;
    public int CurrentIndex { get; private set; } = 0;
    public T SelectedItem { get; protected set; }
    public int ItemCount => _itemList.Count;
    public event Action<int> OnSwitchInventoryItem;

    public Inventory(List<T> itemList)
    {
        _itemList = itemList;
        SelectedItem = _itemList[CurrentIndex];
    }

    public void AddItem(T item)
    {
        _itemList.Add(item);
    }

    public List<T> GetItemList()
    {
        return _itemList;
    }

    // add input action to trigger switch inventory item
    public void SwitchInventoryItem()
    {
        if (CurrentIndex == _itemList.Count - 1)
        {
            CurrentIndex = 0;
        }
        else if (CurrentIndex < _itemList.Count - 1)
        {
            CurrentIndex += 1;
        }
        SelectedItem = _itemList[CurrentIndex];
        OnSwitchInventoryItem?.Invoke(CurrentIndex);
    }
}
