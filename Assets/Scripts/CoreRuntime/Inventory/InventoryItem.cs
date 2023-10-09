using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "InventoryItem", menuName = "AquaKitty/ScriptableObjects/InventoryItem")]
public class InventoryItem : ScriptableObject
{
    public string name;
    public ItemType type;
    public Sprite sprite;
    public int ammunition;
    public GameObject ammunitionPrefab;
}

public enum ItemType
{
    BubbleGun,
    GrappleGun
}
