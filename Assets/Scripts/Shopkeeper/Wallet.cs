using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Wallet : MonoBehaviour
{
    public int gold;

    public void Update()
    {
        DataManager.instance.currentSaveData.playerData.money = gold;
    }

    // cheat code to add gold
    public void AddGold()
    {
        gold += 999;
    }

    public void AddMoney(int amount)
    {
        gold += amount;
    }
}
