using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetHealAbility : MonoBehaviour
{
    // TODO: pastiin 10f tuh 10s 
    public float timeBetweenHeals = 10f;
    public int healAmount = 10;

    GameObject player;

    PlayerHealth playerHealth;
    PetHealth petHealth;
    EnemyHealth enemyHealth;

    AudioSource playerAudio;


    float timer;

    // Start is called before the first frame update
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();

        player = GameObject.FindGameObjectWithTag("Player");

        playerHealth = player.GetComponent<PlayerHealth>();
        petHealth = GetComponent<PetHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeBetweenHeals && petHealth.currentHealth > 0)
        {
            HealPlayer();
        }
    }

    void HealPlayer()
    {
        //Reset timer
        timer = 0f;

        //Taking Damage
        if (playerHealth.currentHealth < 100)
        {
            playerHealth.UseHeal(healAmount);
            playerAudio.Play();
        }

    }
}
