﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;

    public Slider healthSlider;
    public Image damageImage;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;

    PlayerAttack playerAttack;

    bool isDead;
    bool damaged;
    bool isNoDamageCheat = false;

    void Awake()
    {
        //Mendapatkan refernce komponen
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
        playerAttack = GetComponent<PlayerAttack>();

        if (currentHealth == 0)
        {
            currentHealth = startingHealth;
        }
        healthSlider.value = currentHealth;
    }

    void Update()
    {
        //Jika terkena damaage
        if (damaged)
        {
            //Merubah warna gambar menjadi value dari flashColour
            damageImage.color = flashColour;
        }
        else
        {
            //Fade out damage image
            damageImage.color = Color.Lerp(
                damageImage.color,
                Color.clear,
                flashSpeed * Time.deltaTime
            );
        }

        //Set damage to false
        damaged = false;
    }

    //fungsi untuk mendapatkan damage
    public void TakeDamage(int amount)
    {
        damaged = true;

        //mengurangi health
        if (!isNoDamageCheat)
        {
            currentHealth -= amount;
        }

        //Merubah tampilan dari health slider
        healthSlider.value = currentHealth;
        updateHealth();

        //Memainkan suara ketika terkena damage
        playerAudio.Play();

        //Memanggil method Death() jika darahnya kurang dari sama dengan 10 dan belu mati
        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;

        // playerShooting.DisableEffects();

        //mentrigger animasi Die
        anim.SetTrigger("Die");

        //Memainkan suara ketika mati
        playerAudio.clip = deathClip;
        playerAudio.Play();

        //mematikan script player movement
        playerMovement.enabled = false;
        playerAttack.enabled = false;

        // playerShooting.enabled = false;
    }

    public void RestartLevel()
    {
        //meload ulang scene dengan index 0 pada build setting
        SceneManager.LoadScene(0);
    }

    public void UseHeal(int amount)
    {
        currentHealth = Mathf.Min(startingHealth, currentHealth + amount);
        healthSlider.value = currentHealth;
        updateHealth();
    }

    private void updateHealth()
    {
        DataManager.instance.currentSaveData.playerData.health = currentHealth;
    }

    public void NoDamageCheat()
    {
        isNoDamageCheat = true;
    }
}
