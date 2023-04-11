using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatManager : MonoBehaviour
{
    // ublic GameObject cheatInput;
    GameObject cheatInput;
    public bool UIactive;
    void Awake()
    {
        // find child object 0, component canvas
        cheatInput = transform.GetChild(0).gameObject;
        // make invisible
        cheatInput.SetActive(false);
        
        // find child of cheatInput
        GameObject cheatConsole = cheatInput.transform.GetChild(0).gameObject;
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            Debug.Log("Cheat Input Opened");
            PauseGame();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Cheat Input Closed");
            ResumeGame();
        }
    }
    
    public void PauseGame()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        
        cheatInput.SetActive(true);
        
        Time.timeScale = 0f;
        UIactive = true;
    }
    
    public void ResumeGame()
    {
        Cursor.visible = false;
        // Cursor.lockState = CursorLockMode.Locked;
        
        cheatInput.SetActive(false);
        Time.timeScale = 1f;
        UIactive = false;
    }
}
