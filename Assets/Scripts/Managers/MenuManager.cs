using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject scoreboardMenu;
    public GameObject loadMenu;

    AudioSource buttonClick;

    void Start()
    {
        changeMenu("main");

        buttonClick = GetComponents<AudioSource>()[0];

        BGMManager.instance.changeBGM("menu");
    }

    public void changeMenu(string menu)
    {
        switch (menu)
        {
            case "main":
                mainMenu.SetActive(true);
                settingsMenu.SetActive(false);
                scoreboardMenu.SetActive(false);
                loadMenu.SetActive(false);
                break;
            case "settings":
                mainMenu.SetActive(false);
                settingsMenu.SetActive(true);
                scoreboardMenu.SetActive(false);
                loadMenu.SetActive(false);
                break;
            case "scoreboard":
                mainMenu.SetActive(false);
                settingsMenu.SetActive(false);
                scoreboardMenu.SetActive(true);
                loadMenu.SetActive(false);
                break;
            case "load":
                mainMenu.SetActive(false);
                settingsMenu.SetActive(false);
                scoreboardMenu.SetActive(false);
                loadMenu.SetActive(true);
                break;
        }
    }

    public void playButtonClick()
    {
        buttonClick.Play();
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
