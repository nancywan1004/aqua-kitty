using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "InventoryItem", menuName = "AquaKitty/ScriptableObjects/InventoryItem")]
public class InventoryItem : ScriptableObject
{
    public ItemType itemType;
    public Sprite sprite;
    public int ammunition;
    public enum ItemType
    {
        BubbleGun,
        GrappleGun,
        Garbage
    }
}
