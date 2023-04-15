using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    float time = 0f;
    Animator anim;

    Transform player;

    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    UnityEngine.AI.NavMeshAgent nav;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();

        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    void Start() { }

    private void FixedUpdate()
    {
        if (enemyHealth.currentHealth <= 0)
        {
            // set all velocity to 0
            nav.velocity = Vector3.zero;
            nav.angularSpeed = 0;
            nav.acceleration = 0;
            nav.enabled = false;

            // set object velocity to 0
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            GetComponent<Rigidbody>().isKinematic = true;
            return;
        }
        if (playerHealth.currentHealth > 0)
        {
            nav.SetDestination(player.position);
        }
        else
        {
            nav.enabled = false;
        }

        // walking or not
        if (nav.velocity.magnitude > 1f)
        {
            anim.SetBool("walking", true);
        }
        else
        {
            anim.SetBool("walking", false);
        }
    }

    private void LateUpdate() { }
}
