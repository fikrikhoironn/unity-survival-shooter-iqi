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
        // not colliding trigger
        if (collider.isTrigger)
            return;

        if (didHit)
            return;

        // if hit not shootable layer and not floor, (not 6, not 3)
        if (collider.gameObject.layer != 6 && collider.gameObject.layer != 3)
        {
            return;
        }

        didHit = true;

        if (collider.CompareTag(enemyTag) && !collider.isTrigger && rb.velocity.magnitude > 0.1f)
        {
            enemyHealth = collider.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(damage, transform.position);
        }

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;

        // deactivate all collider
        Collider[] colliders = GetComponentsInChildren<Collider>();
        foreach (Collider c in colliders)
        {
            c.enabled = false;
        }

        // deactivate light in child component
        Light[] lights = GetComponentsInChildren<Light>();
        foreach (Light l in lights)
        {
            l.enabled = false;
        }
    }
}
