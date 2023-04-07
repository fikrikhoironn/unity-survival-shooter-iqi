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
    public PetType currentPet = PetType.NONE;
}
