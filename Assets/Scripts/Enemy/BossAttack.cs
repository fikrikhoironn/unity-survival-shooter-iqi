using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    BossAttackCollider normalAttack;
    BossAttackCollider ultimateAttack;

    UnityEngine.AI.NavMeshAgent nav;

    Transform playerTransform;

    public Animator anim;
    AnimationState animState;
    public int normalAttackCooldown;
    public int ultimateAttackCooldown;
    public int normalAttackDamage;
    public int ultimateAttackDamage;

    EnemyHealth EnemyHealth;

    float normalAttackTimer;
    float ultimateAttackTimer;

    bool isAttacking = false;
    void Awake()
    {
        // find collider on child object (NormalAttack and UltimateAttack)
        normalAttack = transform.Find("NormalAttack").GetComponent<BossAttackCollider>();
        ultimateAttack = transform.Find("UltimateAttack").GetComponent<BossAttackCollider>();
        anim = GetComponent<Animator>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        EnemyHealth = GetComponent<EnemyHealth>();
    }
    void Start()
    {
        normalAttackTimer = 0f;
        ultimateAttackTimer = 0f;

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (EnemyHealth.currentHealth <= 0) {
            return;
        }
        normalAttackTimer += Time.deltaTime;
        ultimateAttackTimer += Time.deltaTime;

        if (ultimateAttackTimer >= ultimateAttackCooldown && normalAttackTimer >= normalAttackCooldown)
        {
            // entering ultimate state

            nav.stoppingDistance = 5f;
            if (ultimateAttack.playerOrPetInRange())
            {
                anim.SetBool("jumpAttack", true);
                anim.SetBool("attack", false);

                // enter ultimate attack state
                if (!isAttacking)
                {
                    isAttacking = true;
                    anim.SetBool("jumpAttack", true);
                    Debug.Log("Player in range, Ultimate Attack");

                    StartCoroutine
                    (
                        UltimateAttack()
                    );
                }


            }
        }
        else if (normalAttackTimer >= normalAttackCooldown)
        {
            // entering attack state

            // set nav stopping distance to 3
            nav.stoppingDistance = 3f;

            if (normalAttack.playerOrPetInRange())
            {
                anim.SetBool("attack", true);

                // enter ultimate attack state
                if (!isAttacking)
                {
                    isAttacking = true;
                    anim.SetBool("attack", true);
                    Debug.Log("Player in range, Normal Attack");

                    StartCoroutine
                    (
                        NormalAttack()
                    );
                }


            }

        }
        else
        {
            nav.stoppingDistance = 3f;
        }
    }

    IEnumerator NormalAttack()
    {
        // set animation to ultimate attack
        Debug.Log("Damaging player, normal attack");

        // stop nav
        nav.isStopped = true;

        // look at player
        transform.LookAt(playerTransform);

        // wait until animation is done using animation state
        yield return new WaitForSeconds(2f);

        // damaging player or pet
        normalAttack.Damage(normalAttackDamage);
        anim.SetBool("attack", false);
        isAttacking = false;

        // reset timer
        normalAttackTimer = 0f;

        // resume nav
        nav.isStopped = false;
    }

    IEnumerator UltimateAttack()
    {
        // set animation to ultimate attack
        Debug.Log("Damaging player, ultimate attack");

        // stop nav
        nav.isStopped = true;

        // look at player
        transform.LookAt(playerTransform);

        // wait until animation is done using animation state
        yield return new WaitForSeconds(2f);

        // damaging player or pet
        ultimateAttack.Damage(ultimateAttackDamage);
        anim.SetBool("jumpAttack", false);
        isAttacking = false;

        // reset timer
        ultimateAttackTimer = 0f;
        normalAttackTimer = 0f;

        // resume nav
        nav.isStopped = false;
    }


    // }
}
