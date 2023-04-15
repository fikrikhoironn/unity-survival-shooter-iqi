using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    Animator anim;
    public int initialDamagePerShot = 20;
    public int damagePerShot;

    public float timeBetweenBullets = 0.15f * 7;
    public float range = 10f;

    float timer;
    Ray shootRay = new Ray();
    RaycastHit shootHit;

    // RaycastHit shootHit2;
    int shootableMask;
    ParticleSystem gunParticles;
    LineRenderer gunLine1;
    LineRenderer gunLine2;
    LineRenderer gunLine3;
    AudioSource gunAudio;
    Light gunLight;
    float effectsDisplayTime = 0.2f / 7;
    LineRenderer[] gunLines = new LineRenderer[3];

    void Awake()
    {
        //GetMask
        shootableMask = LayerMask.GetMask("Shootable");

        //Mendapatkan Reference component
        gunParticles = GetComponent<ParticleSystem>();
        gunLines = new LineRenderer[]
        {
            GameObject.Find("Bullet1").GetComponent<LineRenderer>(),
            GameObject.Find("Bullet2").GetComponent<LineRenderer>(),
            GameObject.Find("Bullet3").GetComponent<LineRenderer>(),
            GameObject.Find("Bullet4").GetComponent<LineRenderer>(),
            GameObject.Find("Bullet5").GetComponent<LineRenderer>(),
            GameObject.Find("Bullet6").GetComponent<LineRenderer>(),
            GameObject.Find("Bullet7").GetComponent<LineRenderer>(),
            GameObject.Find("Bullet8").GetComponent<LineRenderer>(),
            GameObject.Find("Bullet9").GetComponent<LineRenderer>()
        };
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
        for (int i = 0; i < gunLines.Length; i++)
        {
            gunLines[i].enabled = false;
        }

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
        timer = 0f;
        
        anim.SetTrigger("Attack");

        //Play audio
        gunAudio.Play();

        //enable Light
        gunLight.enabled = true;

        //Play gun particle
        gunParticles.Stop();
        gunParticles.Play();

        //Set posisi ray shoot dan direction
        shootRay.origin = transform.position;
        

        //enable Line renderer dan set first position
        for (int i = 0; i < gunLines.Length; i++)
        {
            gunLines[i].enabled = true;
            gunLines[i].SetPosition(0, transform.position);

            Quaternion rotation = Quaternion.AngleAxis(-32f + i * 8f, transform.up);
            shootRay.direction = rotation * transform.forward;

            //Lakukan raycast jika mendeteksi id nemy hit apapun
            if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
            {
                //Lakukan raycast hit hace component Enemyhealth
                EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();

                if (enemyHealth != null)
                {
                    // calculate damage
                    float distance = Vector3.Distance(shootHit.point, shootRay.origin);

                    int damage = Convert.ToInt32(((1 - (distance / range)) * damagePerShot));

                    //Jika ada, maka enemy health take damage
                    enemyHealth.TakeDamage(damage, shootHit.point);
                }

                //Set line end position ke hit position
                gunLines[i].SetPosition(1, shootHit.point);
            }
            else
            {
                //set line end position ke range freom barrel
                gunLines[i].SetPosition(1, shootRay.origin + shootRay.direction * range);
            }
        }
    }
}
