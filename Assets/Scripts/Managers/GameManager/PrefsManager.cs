using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PrefsManager : MonoBehaviour
{
    public AudioMixer mixer;
    public static PrefsManager instance;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        // Load the settings from the player prefs
        var sfxVolume = PlayerPrefs.GetInt("sfxVolume", 100);
        var musicVolume = PlayerPrefs.GetInt("musicVolume", 100);
        setSfxVolume(sfxVolume);
        setMusicVolume(musicVolume);

        // Set the instance
        if (instance == null)
        {
            instance = this;
        }
    }

    public void setSfxVolume(float sfxVolume)
    {
        // Normalize amount,
        sfxVolume = sfxVolume / 100f * 30f - 30f;
        mixer.SetFloat("sfxVol", sfxVolume);
    }

    public void setMusicVolume(float musicVolume)
    {
        // Normalize amount,
        musicVolume = musicVolume / 100f * 30f - 30f;
        mixer.SetFloat("musicVol", musicVolume);
    }
}
