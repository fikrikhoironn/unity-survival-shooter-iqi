using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    Transform player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    UnityEngine.AI.NavMeshAgent nav;

    EnemyType enemyType;

    private void Awake()
    {
        // find game object with tag player
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // get reference component
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        enemyType = enemyHealth.enemyType;
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    void Update()
    {
        if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {
            if (enemyType == EnemyType.Zombunny)
            {
                // random speed around 1f to 10f
                nav.speed = Random.Range(1f, 10f);
            }
            nav.SetDestination(player.position);
        }
        else
        {
            nav.enabled = false;
        }
    }
}
