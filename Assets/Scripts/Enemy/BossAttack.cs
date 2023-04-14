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

    AnimatorStateInfo stateInfo;
    public float normalAttackCooldown;
    public float ultimateAttackCooldown;
    public int normalAttackDamage;
    public int ultimateAttackDamage;

    EnemyHealth EnemyHealth;

    float normalAttackTimer;
    float ultimateAttackTimer;

    float betweenAttackTimer;

    public float betweenAttackTime;

    public GameObject ultimateEffectPrefab;

    bool isAttacking = false;

    int idleId;
    int attackId;
    int jumpAttackId;
    int walkingId;

    AudioSource ultimateAttackSound;

    AnimatorClipInfo[] clipInfo;
    void Awake()
    {
        // find collider on child object (NormalAttack and UltimateAttack)
        normalAttack = transform.Find("NormalAttack").GetComponent<BossAttackCollider>();
        ultimateAttack = transform.Find("UltimateAttack").GetComponent<BossAttackCollider>();
        anim = GetComponent<Animator>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        EnemyHealth = GetComponent<EnemyHealth>();
        ultimateAttackSound = ultimateAttack.transform.Find("UltimateSound").GetComponent<AudioSource>();

        // idleId = Animator.StringToHash("Idle");
        // attackId = Animator.StringToHash("Attack");
        // jumpAttackId = Animator.StringToHash("Jump Attack");
        // walkingId = Animator.StringToHash("Walking");
    }
    void Start()
    {
        normalAttackTimer = 0f;
        ultimateAttackTimer = 0f;
        betweenAttackTimer = 0f;

    }

    // Update is called once per frame
    void Update()
    {

        // if (stateHash == idleId)
        // {
        //     Debug.Log("Idle");
        // }
        // else if (stateHash == attackId)
        // {
        //     Debug.Log("Attack");
        // }
        // else if (stateHash == jumpAttackId)
        // {
        //     Debug.Log("Jump Attack");
        // }
        // else if (stateHash == walkingId)
        // {
        //     Debug.Log("Walking");
        // }
        // else
        // {
        //     Debug.Log("Unknown State");
        // }
        // Debug.Log("Time: " + time);


    }

    void FixedUpdate()
    {

        if (EnemyHealth.currentHealth <= 0)
        {
            return;
        }
        float time = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;

        normalAttackTimer += Time.deltaTime;
        ultimateAttackTimer += Time.deltaTime;
        betweenAttackTimer += Time.deltaTime;

        if (ultimateAttackTimer >= ultimateAttackCooldown && betweenAttackTimer >= betweenAttackTime)
        {
            // entering ultimate state

            nav.stoppingDistance = 4.5f;
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

                    // stop nav
                    nav.isStopped = true;

                    // look at player
                    transform.LookAt(playerTransform);

                    // set souund time to 0
                    ultimateAttackSound.time = 0;
                    ultimateAttackSound.Play();

                    if (ultimateAttackSound != null)
                    {
                        Debug.Log("Ultimate Attack Sound is not null");
                    }
                    else
                    {
                        Debug.Log("Ultimate Attack Sound is null");
                    }
                }
            }

            if (isAttacking && isAnimaatingJumpAttack() && time >= 0.4f)
            {
                UltimateAttack();
            }
        }
        else if (normalAttackTimer >= normalAttackCooldown && betweenAttackTimer >= betweenAttackTime)
        {
            // entering attack state

            // set nav stopping distance to 3
            nav.stoppingDistance = 3f;

            if (normalAttack.playerOrPetInRange())
            {
                anim.SetBool("attack", true);

                // enter normal attack state
                if (!isAttacking)
                {
                    isAttacking = true;
                    anim.SetBool("attack", true);
                    Debug.Log("Player in range, Normal Attack");

                    // stop nav
                    nav.isStopped = true;

                    // look at player
                    transform.LookAt(playerTransform);
                }

            }
            if (isAttacking && isAnimatingAttack() && time >= 0.4f)
            {
                NormalAttack();
            }

        }
        else
        {
            nav.stoppingDistance = 3f;
        }
    }

    bool isAnimatingAttack()
    {
        return anim.GetCurrentAnimatorStateInfo(0).IsName("Attack");
    }

    bool isAnimaatingJumpAttack()
    {
        return anim.GetCurrentAnimatorStateInfo(0).IsName("Jump Attack");
    }

    void NormalAttack()
    {
        // set animation to ultimate attack
        Debug.Log("Damaging player, normal attack");

        // damaging player or pet
        normalAttack.Damage(normalAttackDamage);
        anim.SetBool("attack", false);
        isAttacking = false;

        // reset timer
        normalAttackTimer = 0f;
        betweenAttackTimer = 0f;

        // resume nav
        nav.isStopped = false;
    }

    void UltimateAttack()
    {

        // set animation to ultimate attack
        Debug.Log("Damaging player, ultimate attack");
        // instansiate effect at the position of the ultimate attack
        GameObject effect = Instantiate(ultimateEffectPrefab, ultimateAttack.transform.position, Quaternion.identity);
        ParticleSystem particle = effect.GetComponent<ParticleSystem>();
        // set parent of effect to null
        effect.transform.parent = null;
        particle.Play();
        Destroy(effect, particle.main.duration);





        // damaging player or pet
        ultimateAttack.Damage(ultimateAttackDamage);
        anim.SetBool("jumpAttack", false);
        isAttacking = false;

        // reset timer
        ultimateAttackTimer = 0f;
        betweenAttackTimer = 0f;
        // resume nav
        nav.isStopped = false;
    }


    // }
}
