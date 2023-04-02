using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetMovement : MonoBehaviour
{
    Transform player;
    PlayerHealth playerHealth;
    UnityEngine.AI.NavMeshAgent nav;

    Transform thisTransform;
    private void Awake()
    {
        // find game object with tag player
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // get reference component
        playerHealth = player.GetComponent<PlayerHealth>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();

        // get this object transform
        thisTransform = GetComponent<Transform>();
    }

    void Update()
    {

        thisTransform.LookAt(player.position);
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
