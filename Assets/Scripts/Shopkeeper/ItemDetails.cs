using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemDetails : MonoBehaviour
{
    // Name, image, type, cost
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDescription;
    public TextMeshProUGUI itemType;
    public TextMeshProUGUI itemCost;
    public Image itemImage;
    private ItemObject selectedItem;

    public void SetItem(ItemObject item)
    {
        selectedItem = item;
        itemName.text = item.name;
        itemDescription.text = item.description;
        itemType.text = "Type: " + (item.selectedItemType == ItemTypes.Pet ? "Pet" : "Weapon");
        itemCost.text = "Cost: " + item.cost.ToString();
        itemImage.sprite = item.icon;
    }

    public void buyItem()
    {
        ItemDisplay itemDisplay = GetComponentInParent<ItemDisplay>();
        if (itemDisplay.GetWallet().gold >= selectedItem.cost)
        {
            itemDisplay.GetWallet().gold -= selectedItem.cost;
            itemDisplay.UpdateGold();
            print("Bought " + selectedItem.name);
            // Play Ka-Ching!
            if (selectedItem.selectedItemType == ItemTypes.Pet)
            {
                // Add pet to inventory
            }
            else
            {
                // Add weapon to inventory
            }
        }
        else
        {
            Debug.Log("Not enough gold");
        }
    }
}
