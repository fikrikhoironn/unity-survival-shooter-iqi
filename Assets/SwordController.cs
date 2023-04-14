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

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(CanAttack)
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
        StartCoroutine(ResetAttackCooldown());
    }
    
    IEnumerator ResetAttackCooldown()
    {
        StartCoroutine(ResetAttackBool());
        yield return new WaitForSeconds(AttackCooldown);
        CanAttack = true;
    }
    
    IEnumerator ResetAttackBool()
    {
        yield return new WaitForSeconds(0.5f);
        IsAttacking = false;
    }
    
}
