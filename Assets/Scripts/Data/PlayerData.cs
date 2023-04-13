using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerData
{
    public int money = 0;
    public int health = 50;
    public WeaponType currentWeapon = WeaponType.DEFAULT;
    public bool[] unlockedWeapons = new bool[Enum.GetNames(typeof(WeaponType)).Length];
    public PetType currentPet = PetType.NONE;
    public int petHealth = 0;
}
