using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGun : MonoBehaviour
{
    public int initialDamagePerShot = 20;
    public int damagePerShot;

    public float timeBetweenBullets = 0.15f;
    public float range = 100f;

    float timer;
    Ray shootRay = new Ray();
    RaycastHit shootHit;
    int shootableMask;
    ParticleSystem gunParticles;
    LineRenderer gunLine;
    AudioSource gunAudio;
    Light gunLight;
    float effectsDisplayTime = 0.2f;

    Animator anim;
    

    void Awake()
    {
        //GetMask
        shootableMask = LayerMask.GetMask("Shootable");

        //Mendapatkan Reference component
        gunParticles = GetComponent<ParticleSystem>();
        gunLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        gunLight = GetComponent<Light>();
        damagePerShot = initialDamagePerShot;

        anim = transform.parent.GetComponent<Animator>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (
            Input.GetButton("Fire1")
            && timer >= timeBetweenBullets
            && Time.timeScale != 0
            && !StateManager.instance.isBreak
        )
        {
            Shoot();
        }

        if (timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects();
        }
    }

    public void DisableEffects()
    {
        //disable line renderer
        gunLine.enabled = false;

        //disable light
        gunLight.enabled = false;
    }

    public void BuffDamageShot(int amount)
    {
        damagePerShot = amount + initialDamagePerShot;
    }

    public void ResetDamageShot()
    {
        damagePerShot = initialDamagePerShot;
    }

    void Shoot()
    {
        anim.SetTrigger("shoot");
        timer = 0f;

        //Play audio
        gunAudio.Play();

        //enable Light
        gunLight.enabled = true;

        //Play gun particle
        gunParticles.Stop();
        gunParticles.Play();

        //enable Line renderer dan set first position
        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);

        //Set posisi ray shoot dan direction
        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        //Lakukan raycast jika mendeteksi id nemy hit apapun
        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            //Lakukan raycast hit hace component Enemyhealth
            EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();

            if (enemyHealth != null)
            {
                //Jika ada, maka enemy health take damage
                enemyHealth.TakeDamage(damagePerShot, shootHit.point);
            }

            //Set line end position ke hit position
            gunLine.SetPosition(1, shootHit.point);
        }
        else
        {
            //set line end position ke range freom barrel
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
    }
}
