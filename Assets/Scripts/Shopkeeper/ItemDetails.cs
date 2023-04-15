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

    private string[] weaponNameIndex = new string[4] { "Default", "Shotgun", "Sword", "Bow" };

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
            || (
                item.selectedItemType == ItemTypes.Weapon
                && MapWeaponNameToIndex(item.name) != -1
                && DataManager.instance.currentSaveData.playerData.unlockedWeapons[
                    MapWeaponNameToIndex(item.name)
                ]
            ) // Weapon already unlockedwa
        )
        {
            buyButton.interactable = false;
        }
        else
        {
            buyButton.interactable = true;
        }
    }

    private int MapWeaponNameToIndex(string name)
    {
        for (int i = 0; i < weaponNameIndex.Length; i++)
        {
            if (weaponNameIndex[i] == name)
            {
                return i;
            }
        }
        return -1;
    }

    public void buyItem()
    {
        ItemDisplay itemDisplay = GetComponentInParent<ItemDisplay>();
        if (itemDisplay.GetWallet().gold >= selectedItem.cost)
        {
            itemDisplay.GetWallet().gold -= selectedItem.cost;
            itemDisplay.UpdateGold();
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
                int weaponIndex = MapWeaponNameToIndex(selectedItem.name);
                if (weaponIndex != -1)
                {
                    itemDisplay.GetWeaponManager().UnlockWeapon(weaponIndex);
                    buyButton.interactable = false;
                }
            }
        }
    }
}
