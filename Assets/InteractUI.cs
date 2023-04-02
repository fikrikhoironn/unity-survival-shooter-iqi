using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractUI : MonoBehaviour
{
    public bool inRange;
    public bool UIactive;
    public GameObject shopUI;
    
    public void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Interact Shop");
            if (UIactive)
            {
                ResumeGame();
            }else
            {
                PauseGame();
            }
        }
    }
    
    public void PauseGame()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        
        shopUI.SetActive(true);
        
        Time.timeScale = 0f;
        UIactive = true;
    }
    
    public void ResumeGame()
    {
        Cursor.visible = false;
        // Cursor.lockState = CursorLockMode.Locked;
        
        shopUI.SetActive(false);
        Time.timeScale = 1f;
        UIactive = false;
    }
    
    private void start()
    {
        shopUI.SetActive(false);
    }
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = true;
        }
    }
    
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
        }
    }
}
