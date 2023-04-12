using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PetAttackerAttack : MonoBehaviour
{


    Animator anim;

    // get this object transform
    Transform thisTransform;

    // shockwave prefab and object
    public GameObject ShockWavePrefab;


    // enemy array
    public List<GameObject> enemies;

    // timer
    float timer = 0.0f;

    float timeBetweenShockWave = 5.0f;


    GameObject closestEnemy = null;

    // pet navmesh agent
    UnityEngine.AI.NavMeshAgent nav;

    // audio source
    AudioSource audioSource;





    private void Awake()
    {
        // get reference component
        anim = GetComponent<Animator>();

        // get this object transform
        thisTransform = GetComponent<Transform>();

        // get pet navmesh agent
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();

        // get audio source
        audioSource = GetComponent<AudioSource>();





    }

    void Update()
    {

    }
    void FixedUpdate()
    {

        // add time to timer
        timer += Time.deltaTime;

        if (timer >= timeBetweenShockWave)
        {



            // 1 second delay


            FindClosestEnemy();
            // print message

            // if there is an enemy
            if (closestEnemy != null)
            {
                timer = 0.0f;
                // // instantiate shockwave
                // ShockWaveObject = Instantiate(ShockWavePrefab, closestEnemy.transform.position, Quaternion.identity);

                // set anim trigger isJumping to true
                anim.SetBool("attacking", true);

                // set nav
                if (nav.enabled == true)
                {
                    nav.velocity = Vector3.zero;
                    nav.isStopped = true;
                }
           




                StartCoroutine(Attack());

                Debug.Log("Attacking, time now = " + timer + "");

            }




        }
    }

    private IEnumerator Attack()
    {
        // play audio clip
        audioSource.Play();

        yield return new WaitForSeconds(1.0f);

        // instantiate shockwave
        GameObject shockwave = Instantiate(ShockWavePrefab, closestEnemy.transform.position, Quaternion.identity);

        try
        {
            Destroy(shockwave, 2.0f);
        }
        catch (System.Exception)
        {
            Debug.Log("Already destroyed");
        }








        anim.SetBool("attacking", false);

        yield return new WaitForSeconds(0.75f);
        // resume nav
        if (nav.enabled == true)
        {
            nav.isStopped = false;
        }
    }

    // on trigger enter
    private void OnTriggerEnter(Collider other)
    {
        // if other object is player
        if (other.tag == "Enemy")
        {
            // append other object to enemies array
            GameObject enemy = other.gameObject;
            enemies.Add(enemy);



        }
    }

    // on trigger exit
    private void OnTriggerExit(Collider other)
    {
        // if other object is player
        if (other.tag == "Enemy")
        {
            // remove other object from enemies array
            GameObject enemy = other.gameObject;
            enemies.Remove(enemy);
        }
    }

    private void FindClosestEnemy()
    {
        // find closest enemy
        float distance = Mathf.Infinity;
        Vector3 position = thisTransform.position;
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                Vector3 diff = enemy.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closestEnemy = enemy;
                    distance = curDistance;
                }
            }
        }
    }
}
