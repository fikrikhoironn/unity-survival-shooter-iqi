using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheatScript : MonoBehaviour
{
    public GameObject player;
    private bool isCheatInputVisible = false;
    private InputField cheatInput;
    private Graphic originalGraphic;

    


    void Start()
    {
        // Get the InputField component
        cheatInput = GetComponent<InputField>();
        // Hide the InputField 
        originalGraphic = cheatInput.targetGraphic;
        cheatInput.interactable = false;
        cheatInput.transform.localScale = Vector3.zero;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            Debug.Log("Q pressed");
            isCheatInputVisible = !isCheatInputVisible;
            cheatInput.interactable = isCheatInputVisible;
            if (isCheatInputVisible)
            {
                cheatInput.transform.localScale = Vector3.one;
                cheatInput.Select();
            }
            else
            {
                cheatInput.transform.localScale = Vector3.zero;
            }
            cheatInput.text = "";
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
            else if (cheatInput.text == "doublespeed")
            {
                PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
                playerMovement.DoubleSpeed();
            }
            cheatInput.text = "";
            cheatInput.transform.localScale = Vector3.zero;
        }
    }

}