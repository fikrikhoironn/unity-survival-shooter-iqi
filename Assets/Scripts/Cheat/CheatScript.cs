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
            Debug.Log("BackQuote pressed");
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
            cheatInput.text = "";
        }
    }
}
