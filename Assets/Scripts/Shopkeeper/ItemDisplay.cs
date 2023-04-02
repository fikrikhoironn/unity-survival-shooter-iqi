using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemDisplay : MonoBehaviour
{
    public ItemObject displayedItem;
    
    //Logic
    public string itemName;
    public string itemDescription;
    public Sprite itemIcon;
    public int itemCost;
    public string itemType;
    
    //Graphics
    public TextMeshProUGUI itemNameUI;
    public TextMeshProUGUI itemDescriptionUI;
    public Image itemIconUI;
    public TextMeshProUGUI itemCostUI;
    public TextMeshProUGUI itemTypeUI;

    public void Start()
    {
        itemName = displayedItem.name;
        itemDescription = displayedItem.description;
        itemIcon = displayedItem.icon;
        itemCost = displayedItem.cost;
        itemType = displayedItem.selectedItemType.ToString();
        
        itemNameUI.text = itemName;
        itemDescriptionUI.text = itemDescription;
        itemIconUI.sprite = itemIcon;
        itemCostUI.text = itemCost.ToString();
        itemTypeUI.text = itemType;
        
    }
    
    public void Display()
    {
        itemName = displayedItem.name;
        itemDescription = displayedItem.description;
        itemIcon = displayedItem.icon;
        itemCost = displayedItem.cost;
        itemType = displayedItem.selectedItemType.ToString();
        
        itemNameUI.text = itemName;
        itemDescriptionUI.text = itemDescription;
        itemIconUI.sprite = itemIcon;
        itemCostUI.text = itemCost.ToString();
        itemTypeUI.text = itemType;
    }
}
