using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    List<GameObject> enemies;
    public int swingDamage = 50;
    public int idlingDamage = 25;
    public SwordController swordController;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && swordController.IsAttacking )
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(swingDamage, other.transform.position);
        }
        else if (other.tag == "Enemy" && !swordController.IsAttacking)
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(idlingDamage, other.transform.position);
        }
    }
}
