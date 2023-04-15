using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    public GameObject Sword;
    public bool CanAttack = true;
    public float AttackCooldown = 1f;
    public AudioClip SwordAttackSound;
    public bool IsAttacking = false;

    CollisionDetector collisionDetector;

    void Awake()
    {
        collisionDetector = GetComponent<CollisionDetector>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (CanAttack && !StateManager.instance.isBreak)
            {
                SwordAttack();
            }
        }
    }

    public void SwordAttack()
    {
        IsAttacking = true;
        CanAttack = false;
        Animator anim = Sword.GetComponent<Animator>();
        anim.SetTrigger("Attack");
        AudioSource.PlayClipAtPoint(SwordAttackSound, transform.position);
        collisionDetector.damageAll();
        StartCoroutine(ResetAttackCooldown());
        StartCoroutine(ResetAttackBool());
    }

    IEnumerator ResetAttackCooldown()
    {
        yield return new WaitForSeconds(AttackCooldown);
        CanAttack = true;
    }

    IEnumerator ResetAttackBool()
    {
        yield return new WaitForSeconds(0.5f);
        IsAttacking = false;
    }
}
