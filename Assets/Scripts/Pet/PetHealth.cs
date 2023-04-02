using UnityEngine;
using System.Collections;

public class PetHealth : MonoBehaviour
{
    public int startingHealth = 50;
    public int currentHealth;
    public float sinkSpeed = 2.5f;

    PetMovement petMovement;

    bool isSinking;
    public bool IsDead;

    // -- CHEAT --
    bool isFullHPPetCheat = false;

    // Use this for initialization
    void Awake()
    {
        currentHealth = startingHealth;
        petMovement = GetComponent<PetMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        //Check jika sinking
        if (isSinking)
        {
            //memindahkan object kebawah
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }

    public void TakeDamage(int amount)
    {
        if (!isFullHPPetCheat)
        {
            currentHealth -= amount;
            if (currentHealth <= 0 && !IsDead)
            {
                Death();
            }
        }
    }

    void Death()
    {
        IsDead = true;
        petMovement.enabled = false;
        StartSinking();
    }

    // ------------------- CHEAT -------------------
    public void SetToZero()
    {
        currentHealth = 0;
        Death();
    }

    public void FullHpPet()
    {
        isFullHPPetCheat = true;
    }

    public void StartSinking()
    {
        //disable Navmesh Component
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        //Set rigisbody ke kinematic
        GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;
        Destroy(gameObject, 2f);
    }
}
