using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetMovement : MonoBehaviour
{
    Transform player;
    PlayerHealth playerHealth;
    UnityEngine.AI.NavMeshAgent nav;

    Animator anim;
    Vector3 enemyPosition;

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
            transform.LookAt(player.position);

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
            enemyPosition = other.transform.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy") && !other.isTrigger)
        {
            enemyPosition = new Vector3(0, 0, 0);
        }
    }
}
