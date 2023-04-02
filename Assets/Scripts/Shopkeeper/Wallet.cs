using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Wallet : MonoBehaviour
{
    public int gold;
    public TextMeshProUGUI goldBalance;
    
    public void Update()
    {
        goldBalance.SetText(gold.ToString());
    }
    
    // cheat code to add gold
    public void AddGold()
    {
        gold += 999;
    }
}
