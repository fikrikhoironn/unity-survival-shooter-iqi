using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UI;
using System;
using System.Collections;

public class GameOverManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    private bool playerDead;

    Animator anim;

    void Awake()
    {
        playerDead = false;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (playerHealth.currentHealth <= 0 && !playerDead)
        {
            anim.SetTrigger("GameOver");
            playerDead = true;
            StartCoroutine(RestartLevel());
            playerHealth.gameObject.SetActive(false);
        }
    }

    IEnumerator RestartLevel()
    {
        yield return new WaitForSecondsRealtime(5f);
        SceneManagerObject.instance.LoadMainMenu();
    }
}
