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
    public Button buyButton;

    private ItemObject selectedItem;

    public void SetItem(ItemObject item)
    {
        selectedItem = item;
        itemName.text = item.name;
        itemDescription.text = item.description;
        itemType.text = "Type: " + (item.selectedItemType == ItemTypes.Pet ? "Pet" : "Weapon");
        itemCost.text = "Cost: " + item.cost.ToString();
        itemImage.sprite = item.icon;
        if (
            (item.selectedItemType == ItemTypes.Pet && PetManager.instance.petTransform != null) // Got pet already
            || (false) // TODO: Or Weapon is already bought
        )
        {
            buyButton.interactable = false;
        }
        else
        {
            buyButton.interactable = true;
        }
    }

    public void buyItem()
    {
        ItemDisplay itemDisplay = GetComponentInParent<ItemDisplay>();
        if (itemDisplay.GetWallet().gold >= selectedItem.cost)
        {
            itemDisplay.GetWallet().gold -= selectedItem.cost;
            itemDisplay.UpdateGold();
            print("Bought " + selectedItem.name + " type " + selectedItem.selectedItemType);
            // Play Ka-Ching!
            if (selectedItem.selectedItemType == ItemTypes.Pet)
            {
                PetType petType = PetType.NONE;
                if (selectedItem.name == "Pet Attacker")
                {
                    petType = PetType.ATTACKER;
                }
                else if (selectedItem.name == "Pet Healer")
                {
                    petType = PetType.HEALER;
                }
                else if (selectedItem.name == "Pet Aura Buff")
                {
                    petType = PetType.AURA_BUFF;
                }

                PetManager.instance.SpawnPet(petType);
                buyButton.interactable = false;
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
