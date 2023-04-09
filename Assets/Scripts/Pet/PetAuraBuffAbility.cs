using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetAuraBuffAbility : MonoBehaviour
{
    GameObject gunBarrelEnd;
    GameObject pet;

    PlayerShooting playerShooting;
    PetHealth petHealth;
    AudioSource petAudio;

    int buff = 80;

    // Start is called before the first frame update
    void Start()
    {
        petAudio = GetComponent<AudioSource>();

        gunBarrelEnd = GameObject.FindGameObjectWithTag("Gun");
        pet = GameObject.FindGameObjectWithTag("Pet");

        if (gunBarrelEnd != null)
        {
            playerShooting = gunBarrelEnd.GetComponent<PlayerShooting>();
            playerShooting.BuffDamageShot(buff);
        }
        else
        {
            // todo: make general for all weapon
            playerShooting = null;
        }

        petHealth = pet.GetComponent<PetHealth>();

        petAudio.Play();


    }

    // Update is called once per frame
    void Update()
    {
        // todo: make general for all weapon
        if (petHealth.IsDead && playerShooting != null)
        {
            playerShooting.ResetDamageShot();
        }
    }
}
