using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private int damage = 100;
    private float torque;

    private Rigidbody rb;

    private string enemyTag = "Enemy";

    private bool didHit;

    private bool fly = false;

    private EnemyHealth enemyHealth;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        didHit = false;
        rb.isKinematic = true;
    }

    public void setEnemyTag(string enemyTag)
    {
        this.enemyTag = enemyTag;
    }

    public void Fly(Vector3 force)
    {
        rb.isKinematic = false;
        rb.AddForce(force, ForceMode.Impulse);
        // rb.AddTorque(transform.right * torque);
        transform.SetParent(null);

    }

    void OnTriggerEnter(Collider collider)
    {
        if (didHit) return;
        didHit = true;
        // if colliding with player
        if (collider.CompareTag("Player") || collider.CompareTag("Pet"))
        {
            return;
        }



        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;

        if (collider.CompareTag(enemyTag))
        {
            enemyHealth = collider.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(damage, transform.position);

        }


        // deactivate all collider
        Collider[] colliders = GetComponentsInChildren<Collider>();
        foreach (Collider c in colliders)
        {
            c.enabled = false;
        }

    }
}
