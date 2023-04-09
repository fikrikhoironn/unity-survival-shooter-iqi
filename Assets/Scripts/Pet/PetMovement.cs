using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetMovement : MonoBehaviour
{
    Transform player;
    PlayerHealth playerHealth;
    UnityEngine.AI.NavMeshAgent nav;

    Animator anim;

    private void Awake()
    {
        // find game object with tag player
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // get reference component
        playerHealth = player.GetComponent<PlayerHealth>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (playerHealth.currentHealth > 0)
        {
            nav.SetDestination(player.position);
            // look at nav destination
            transform.LookAt(nav.destination);
            // fix rotation for pet attacker
            if (GetComponent<PetAttackerAttack>())
            {
                transform.Rotate(new Vector3(0, 90, 0));
            }

            if (nav.velocity.magnitude > 0)
            {
                anim.SetBool("walking", true);
            }
            else
            {
                anim.SetBool("walking", false);
            }
        }
        else
        {
            nav.enabled = false;
        }
    }
}
