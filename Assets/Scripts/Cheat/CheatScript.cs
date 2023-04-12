using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheatScript : MonoBehaviour
{
    public GameObject player;
    private bool isCheatInputVisible = false;
    private InputField cheatInput;
    


    void Start()
    {
        // Get the InputField component
        cheatInput = GetComponent<InputField>();
        // Hide the InputField initially
        // cheatInput.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Q pressed");
            isCheatInputVisible = !isCheatInputVisible;
            cheatInput.gameObject.SetActive(isCheatInputVisible);
            if (isCheatInputVisible)
            {
                cheatInput.Select();
                cheatInput.text = "";
            }
        }

        if (cheatInput.gameObject.activeSelf && Input.GetKeyDown(KeyCode.Return))
        {
            if (cheatInput.text == "suicide")
            {
                PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
                playerHealth.TakeDamage(playerHealth.currentHealth);
            }
            else if (cheatInput.text == "money")
            {
                Wallet wallet = player.GetComponent<Wallet>();
            }
            else if (cheatInput.text == "motherlode")
            {
                Wallet wallet = player.GetComponent<Wallet>();
                wallet.AddMoney(999999);
            }
            else if (cheatInput.text == "onehitkill")
            {
                GameObject gunBarrelEnd = GameObject.FindGameObjectWithTag("Gun");
                PlayerShooting playerShooting;
                if (gunBarrelEnd != null)
                {
                    playerShooting = gunBarrelEnd.GetComponent<PlayerShooting>();
                    playerShooting.BuffDamageShot(99999999);
                }
            }
            cheatInput.text = "";
        }
    }

}