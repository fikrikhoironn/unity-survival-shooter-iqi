using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;

    Animator anim;

    GameObject player;
    GameObject pet = null;

    PlayerHealth playerHealth;
    PetHealth petHealth = null;
    EnemyHealth enemyHealth;

    bool playerInRange;
    bool petInRange;

    float timer;

    void Awake()
    {
        //Mencari game object dengan tag "Player"
        player = GameObject.FindGameObjectWithTag("Player");
        if (GameObject.FindGameObjectWithTag("Pet")){
            pet = GameObject.FindGameObjectWithTag("Pet");
            petHealth = pet.GetComponent<PetHealth>();
        }

        //mendapatkan komponen health
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();

        //mendapatkan komponen Animator
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        //Set player in range
        if (other.gameObject == player && other.isTrigger == false)
        {
            playerInRange = true;
        }

        if (pet != null && other.gameObject == pet && other.isTrigger == false)
        {
            petInRange = true;
        }
    }


    //Callback jika ada object yang keluar dari trigger
    void OnTriggerExit(Collider other)
    {
        //Set player jika tidak dalam range
        if (other.gameObject == player)
        {
            playerInRange = false;
        }

        if (pet != null && other.gameObject == pet)
        {
            petInRange = false;
        }
    }


    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
        {
            AttackPlayer();
        }

        if (pet != null && timer >= timeBetweenAttacks && petInRange && enemyHealth.currentHealth > 0)
        {
            AttackPet();
        }

        //mentrigger animasi PlayerDead jika darah player kurang dari sama dengan 0
        if (playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger("PlayerDead");
        }
    }


    void AttackPlayer()
    {
        //Reset timer
        timer = 0f;

        //Taking Damage
        if (playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }


    void AttackPet()
    {
        if (pet != null)
        {
            //Reset timer
            timer = 0f;

            //Taking Damage
            if (petHealth.currentHealth > 0)
            {
                petHealth.TakeDamage(attackDamage);
            }
        }
    }
}