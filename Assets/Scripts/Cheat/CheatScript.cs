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




    private void Awake()
    {
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();

    }
    void Start()
    {
        cheatInput = GetComponent<InputField>();
        originalGraphic = cheatInput.targetGraphic;
        cheatInput.interactable = false;
        cheatInput.transform.localScale = Vector3.zero;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            isCheatInputVisible = !isCheatInputVisible;
            cheatInput.interactable = isCheatInputVisible;
            if (isCheatInputVisible)
            {
                cheatInput.transform.localScale = Vector3.one;
				Time.timeScale = 0f;
                cheatInput.Select();
            }
            else
            {	
				Time.timeScale = 1f;
                cheatInput.transform.localScale = Vector3.zero;
            }
            cheatInput.text = "";
        }

        if (cheatInput.gameObject.activeSelf && Input.GetKeyDown(KeyCode.Return))
        {
            Wallet wallet = player.GetComponent<Wallet>();
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
            switch (cheatInput.text)
            {
                case "suicide":
                    playerHealth.TakeDamage(playerHealth.currentHealth);
                    break;
                case "money":
                    wallet.AddMoney(999999);
                    break;
                case "motherlode":
                    wallet.AddMoney(999999);
                    break;
                case "onehitkill":
                    GameObject gunBarrelEnd = GameObject.FindGameObjectWithTag("Gun");
                    PlayerShooting playerShooting;
                    if (gunBarrelEnd != null)
                    {
                        playerShooting = gunBarrelEnd.GetComponent<PlayerShooting>();
                        playerShooting.BuffDamageShot(99999999);
                    }
                    break;
                case "doublespeed":
                    playerMovement.DoubleSpeed();
                    break;
            }
            cheatInput.text = "";
			Time.timeScale = 1f;
            cheatInput.transform.localScale = Vector3.zero;
        }
    }

}