using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    public Slider sfxVolumeSlider;
    public Slider musicVolumeSlider;
    public TMP_InputField playerNameInput;

    void OnEnable()
    {
        sfxVolumeSlider.value = PlayerPrefs.GetInt("sfxVolume", 100);
        musicVolumeSlider.value = PlayerPrefs.GetInt("musicVolume", 100);
        playerNameInput.text = PlayerPrefs.GetString("playerName", "");
    }

    void OnDisable()
    {
        var sfxVolume = PlayerPrefs.GetInt("sfxVolume", 100);
        var musicVolume = PlayerPrefs.GetInt("musicVolume", 100);
        PrefsManager.instance.setSfxVolume(sfxVolume);
        PrefsManager.instance.setMusicVolume(musicVolume);
    }

    public void saveData()
    {
        // Save the settings to the player prefs
        PlayerPrefs.SetInt("sfxVolume", (int)sfxVolumeSlider.value);
        PlayerPrefs.SetInt("musicVolume", (int)musicVolumeSlider.value);
        PlayerPrefs.SetString("playerName", playerNameInput.text);
    }
}
