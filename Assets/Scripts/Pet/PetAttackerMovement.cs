using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetAttackerMovement : MonoBehaviour
{
    Transform player;
    PlayerHealth playerHealth;
    UnityEngine.AI.NavMeshAgent nav;

    Animator anim;

    // get this object transform
    Transform thisTransform;


    private void Awake()
    {
        // find game object with tag player
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // get reference component
        playerHealth = player.GetComponent<PlayerHealth>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = GetComponent<Animator>();

        // get this object transform
        thisTransform = GetComponent<Transform>();
    }

    void Update()
    {
        // lookk at player, then rotate 90 degree
        thisTransform.LookAt(player.position);
        thisTransform.Rotate(0, 90, 0);
        // set anim state, isWalking = true
        anim.SetBool("isWalking", true);
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
