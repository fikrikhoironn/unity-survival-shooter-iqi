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
    public bool[] unlockedWeapons = new bool[4] { true, false, false, false };
    public PetType currentPet = PetType.NONE;
    public int petHealth = 0;

    public PlayerData copy()
    {
        PlayerData copy = new PlayerData();
        copy.money = money;
        copy.health = health;
        copy.currentWeapon = currentWeapon;
        copy.unlockedWeapons = unlockedWeapons;
        copy.currentPet = currentPet;
        copy.petHealth = petHealth;
        return copy;
    }
}
