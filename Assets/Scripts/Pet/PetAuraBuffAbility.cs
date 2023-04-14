using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetAuraBuffAbility : MonoBehaviour
{
    GameObject gunBarrelEnd;
    GameObject pet;

    GameObject player;

    PlayerAttack playerAttack;
    PetHealth petHealth;
    AudioSource petAudio;

    int buff = 50;

    // Start is called before the first frame update
    void Start()
    {
        petAudio = GetComponent<AudioSource>();

        gunBarrelEnd = GameObject.FindGameObjectWithTag("Gun");
        pet = GameObject.FindGameObjectWithTag("Pet");

        player = GameObject.FindGameObjectWithTag("Player");

        playerAttack = player.GetComponent<PlayerAttack>();
        playerAttack.BuffDamageShot(buff);

        petHealth = pet.GetComponent<PetHealth>();

        petAudio.Play();


    }

    // Update is called once per frame
    void Update()
    {
        // todo: make general for all weapon
        if (petHealth.IsDead)
        {
            playerAttack.ResetDamageShot(); 
        }
    }
}
