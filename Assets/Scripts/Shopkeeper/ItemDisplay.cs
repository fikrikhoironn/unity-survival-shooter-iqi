using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemDisplay : MonoBehaviour
{
    public ItemTypes selectedItemType = ItemTypes.Weapon;
    public ItemDetails itemDetails;
    public ItemList itemList;
    public ItemObject[] petItems;
    public ItemObject[] weaponItems;
    private Wallet playerWallet;
    private WeaponManager weaponManager;
    public TextMeshProUGUI goldText;

    public void Init()
    {
        UpdateGold();
        changeType(1);
        itemDetails.SetItem(petItems[0]);
    }

    public void SetWallet(Wallet wallet)
    {
        playerWallet = wallet;
        weaponManager = wallet.GetComponentInChildren<WeaponManager>();
    }

    public Wallet GetWallet()
    {
        return playerWallet;
    }

    public WeaponManager GetWeaponManager()
    {
        return weaponManager;
    }

    public void UpdateGold()
    {
        goldText.text = "Your Gold: " + playerWallet.gold.ToString();
    }

    public void changeType(int type)
    {
        selectedItemType = (ItemTypes)type;
        if (selectedItemType == ItemTypes.Pet)
        {
            itemList.setItemList(petItems);
        }
        else
        {
            itemList.setItemList(weaponItems);
        }
    }
}
