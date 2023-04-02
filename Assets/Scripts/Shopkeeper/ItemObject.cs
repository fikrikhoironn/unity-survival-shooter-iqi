using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item")]
public class ItemObject : ScriptableObject
{
    public new string name;
    public string description;
    public Sprite icon;
    public int cost;

    public ItemTypes selectedItemType = new ItemTypes();

    public enum ItemTypes
    {
        Sword,
        Axe,
        Ranged,
        Resource,
        Pet,
        Usable
    }
}
