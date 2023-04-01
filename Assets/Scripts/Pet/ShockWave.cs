using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWave : MonoBehaviour
{

    List<GameObject> enemies;

    int damage = 500;

    void Awake()
    {
        enemies = new List<GameObject>();

        Debug.Log("Start HERE");
        // start coroutine
        StartCoroutine(DamageAllEnemies());
    }

    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        // if other object is player
        if (other.tag == "Enemy")
        {
            Debug.Log("Enemy Detected, " + other.gameObject);
            // append other object to enemies array
            GameObject enemy = other.gameObject;
            enemies.Add(enemy);
            





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
        Debug.Log("Attack");
        yield return new WaitForSeconds(1.0f);

        // show enemies
        Debug.Log("Enemies: " + enemies.Count);

        foreach (GameObject enemy in enemies)
        {
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(damage, enemy.transform.position);
        }
    }
}
