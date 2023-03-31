using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetAuraBuffAbility : MonoBehaviour
{
    GameObject gunBarrelEnd;
    GameObject pet;

    PlayerShooting playerShooting;
    PetHealth petHealth;
    int buff = 80;

    // Start is called before the first frame update
    void Start()
    {
        gunBarrelEnd = GameObject.FindGameObjectWithTag("Gun");
        pet = GameObject.FindGameObjectWithTag("Pet");

        playerShooting = gunBarrelEnd.GetComponent<PlayerShooting>();
        petHealth = pet.GetComponent<PetHealth>();

        playerShooting.BuffDamageShot(buff);
    }

    // Update is called once per frame
    void Update()
    {
        if (petHealth.IsDead)
        {
            playerShooting.ResetDamageShot();
        }
    }
}
