using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetMovement : MonoBehaviour
{
    Transform player;
    PlayerHealth playerHealth;
    UnityEngine.AI.NavMeshAgent nav;

    private void Awake()
    {
        // find game object with tag player
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // get reference component
        playerHealth = player.GetComponent<PlayerHealth>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

        void Update()
    {
        if (playerHealth.currentHealth > 0)
        {
            nav.SetDestination(player.position);
        }
        else
        {
            nav.enabled = false;
        }
    }
}
