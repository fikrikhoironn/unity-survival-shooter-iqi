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
        if (collider.isTrigger) return;

        Debug.Log("Arrow Hit something");
        // if (didHit) return;
        Debug.Log("Arrow Hit: " + collider.gameObject.name);

        if (didHit) return;
        
        // if hit not shootable layer and not floor, (not 6, not 3)
        if (collider.gameObject.layer != 6 && collider.gameObject.layer != 3)
        {
            Debug.Log("Arrow Hit non shootable layer");
            return;
        }

        didHit = true;

        Debug.Log("HERE");


        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;

        if (collider.CompareTag(enemyTag))
        {
            Debug.Log("Arrow Hit enemy");
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
