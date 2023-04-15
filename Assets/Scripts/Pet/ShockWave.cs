using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWave : MonoBehaviour
{
    List<GameObject> enemies;

    public int damage;

    void Awake()
    {
        enemies = new List<GameObject>();

        // start coroutine
        StartCoroutine(DamageAllEnemies());
    }

    // Start is called before the first frame update


    // Update is called once per frame
    void Update() { }

    private void OnTriggerEnter(Collider other)
    {
        // if other object is player
        if (other.tag == "Enemy")
        {
            // append other object to enemies array
            GameObject enemy = other.gameObject;
            // add if not exist
            if (!enemies.Contains(enemy))
            {
                enemies.Add(enemy);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            GameObject enemy = other.gameObject;
            enemies.Remove(enemy);
        }
    }

    private IEnumerator DamageAllEnemies()
    {
        yield return new WaitForSeconds(1.0f);

        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
                enemyHealth.TakeDamage(damage, enemy.transform.position);
            }
        }
    }
}
