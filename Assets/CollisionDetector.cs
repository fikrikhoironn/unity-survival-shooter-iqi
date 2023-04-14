using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    List<GameObject> enemies = new List<GameObject>();
    public int swingDamage = 50;
    public int idlingDamage = 25;
    public SwordController swordController;


    void OnTriggerEnter(Collider other)
    {
        // if (other.tag == "Enemy" && swordController.IsAttacking )
        // {
        //     EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
        //     enemyHealth.TakeDamage(swingDamage, other.transform.position);
        // }
        // else if (other.tag == "Enemy" && !swordController.IsAttacking)
        // {
        //     EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
        //     enemyHealth.TakeDamage(idlingDamage, other.transform.position);
        // }

        if (!other.isTrigger && other.tag == "Enemy")
        {
            enemies.Add(other.gameObject);
        }
    }

    void OnTriggerLeave(Collider other)
    {
        if (!other.isTrigger && other.tag == "Enemy")
        {
            // checkkk if it's in the list
            if (enemies.Contains(other.gameObject))
            {
                enemies.Remove(other.gameObject);
            }
        }

    }

    public void damageAll()
    {
        foreach (GameObject enemy in enemies)
        {
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(swingDamage, enemy.transform.position);
        }
    }

}
