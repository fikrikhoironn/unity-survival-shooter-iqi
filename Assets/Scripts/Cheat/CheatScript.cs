using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheatScript : MonoBehaviour
{
    public InputField cheatInput;
    public Wallet wallet;
    public GameObject Player;
  
    void Update()
    {
        if (cheatInput.text == "suicide")
        {
            Player.SendMessage("Suicide");
        }

    }
}
