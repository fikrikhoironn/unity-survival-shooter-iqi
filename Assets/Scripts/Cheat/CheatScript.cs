using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheatScript : MonoBehaviour
{
    public bool isCheatInputVisible = false;
    private InputField cheatInput;
    private Graphic originalGraphic;
    PetHealth petHealth;
    PlayerAttack playerAttack;
    PlayerHealth playerHealth;
    PlayerMovement playerMovement;
    Wallet wallet;

    private void Awake()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
        playerHealth = player.GetComponent<PlayerHealth>();
        playerAttack = player.GetComponent<PlayerAttack>();
        wallet = player.GetComponent<Wallet>();
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
            if (!isCheatInputVisible && Time.timeScale > 0)
            {
                isCheatInputVisible = true;
                cheatInput.interactable = true;
                cheatInput.transform.localScale = Vector3.one;
                Time.timeScale = 0f;
                cheatInput.Select();
            }
            else if (isCheatInputVisible)
            {
                isCheatInputVisible = false;
                cheatInput.interactable = false;
                Time.timeScale = 1f;
                cheatInput.transform.localScale = Vector3.zero;
            }
            cheatInput.text = "";
        }

        if (cheatInput.gameObject.activeSelf && Input.GetKeyDown(KeyCode.Return))
        {
            if (PetManager.instance.petTransform != null)
            {
                petHealth = PetManager.instance.petTransform.GetComponent<PetHealth>();
            }
            switch (cheatInput.text)
            {
                case "suicide":
                    playerHealth.TakeDamage(playerHealth.currentHealth);
                    break;
                case "nodamage":
                    playerHealth.NoDamageCheat();
                    break;
                case "money":
                    wallet.AddMoney(999999);
                    break;
                case "motherlode":
                    wallet.AddMoney(999999);
                    break;
                case "onehitkill":
                    playerAttack.BuffDamageShot(999999);
                    break;
                case "doublespeed":
                    playerMovement.DoubleSpeed();
                    break;
                case "fullhppet":
                    petHealth.FullHpPet();
                    break;
                case "killpet":
                    petHealth.SetToZero();
                    break;
            }
            cheatInput.text = "";
            Time.timeScale = 1f;
            cheatInput.transform.localScale = Vector3.zero;
        }
    }
}
