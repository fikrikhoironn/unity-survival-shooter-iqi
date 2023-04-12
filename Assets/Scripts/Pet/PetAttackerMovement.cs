using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetAttackerMovement : MonoBehaviour
{
    Transform player;
    PlayerHealth playerHealth;
    UnityEngine.AI.NavMeshAgent nav;

    Animator anim;
    Vector3 enemyPosition;
    bool enemyInRange = false;

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
            if (enemyInRange)
            {
                nav.SetDestination(enemyPosition);

            }
            else
            {
                nav.SetDestination(player.position );

            }

            // look at nav destination
            transform.LookAt(nav.destination);

            // fix rotation for pet attacker
            transform.Rotate(new Vector3(0, 90, 0));

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && !other.isTrigger)
        {
            enemyInRange = true;
            enemyPosition = other.transform.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy") && !other.isTrigger)
        {
            enemyInRange = false;
        }
    }
}
