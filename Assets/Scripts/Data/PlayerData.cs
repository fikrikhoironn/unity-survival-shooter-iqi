using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerData
{
    public int money;
    public int health;
    public WeaponType currentWeapon;
    public PetType currentPet;
    public List<int> weaponsLevel;
}
