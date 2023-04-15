using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    int damageMultiplierPercentage = 100;

    public void BuffDamageShot(int damageBuffPercentage)
    {
        damageMultiplierPercentage += damageBuffPercentage;
        Debug.Log("Damage Multiplier: " + damageMultiplierPercentage);
    }

    public void ResetDamageShot()
    {
        damageMultiplierPercentage = 100;
    }

    public int getMultiplierPercentage()
    {
        return damageMultiplierPercentage;
    }





}