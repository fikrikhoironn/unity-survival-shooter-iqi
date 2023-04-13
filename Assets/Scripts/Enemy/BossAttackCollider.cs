using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackCollider : MonoBehaviour
{
    PlayerHealth playerHealth;
    PetHealth petHealth;

    bool playerInRange = false;
    bool petInRange = false;


    void Awake()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        if (GameObject.FindGameObjectWithTag("Pet"))
        {
            petHealth = GameObject.FindGameObjectWithTag("Pet").GetComponent<PetHealth>();
        }

    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger)
        {
            if (other.gameObject.tag == "Player")
            {
                playerInRange = true;
            }

            if (other.gameObject.tag == "Pet")
            {
                petInRange = true;
            }
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (!other.isTrigger)
        {
            if (other.gameObject.tag == "Player")
            {
                playerInRange = false;
            }

            if (other.gameObject.tag == "Pet")
            {
                petInRange = false;
            }
        }
    }

    public void Damage(int damage)
    {
        if (playerInRange)
        {
            Debug.Log("Player damaged");
            playerHealth.TakeDamage(damage);
        }

        if (petInRange)
        {
            Debug.Log("Pet damaged");
            petHealth.TakeDamage(damage);
        }

        if (!playerInRange && !petInRange)
        {
            Debug.Log("No one damaged");
        }
    }

    public bool playerOrPetInRange()
    {
        return playerInRange || petInRange;
    }
}
