using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BGMManager : MonoBehaviour
{
    public static BGMManager instance;

    [SerializeField]
    public BGMSource[] bgms;
    public float fadeTime = 1f;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0;
        audioSource.Play();
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public IEnumerator FadeIn()
    {
        float timer = 0f;
        while (timer < fadeTime)
        {
            timer += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(0, 1, timer / fadeTime);
            yield return null;
        }
    }

    public IEnumerator FadeOut()
    {
        float timer = 0f;
        while (timer < fadeTime)
        {
            timer += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(1, 0, timer / fadeTime);
            yield return null;
        }
    }

    public IEnumerator changeFadeBGM(string name)
    {
        yield return StartCoroutine(FadeOut());
        audioSource.Stop();
        audioSource.clip = bgms[Array.FindIndex(bgms, bgm => bgm.name == name)].clip;
        audioSource.Play();
        yield return StartCoroutine(FadeIn());
    }

    public void changeBGM(string name)
    {
        audioSource.Stop();
        audioSource.clip = bgms[Array.FindIndex(bgms, bgm => bgm.name == name)].clip;
        audioSource.volume = 1;
        audioSource.Play();
    }
}

[Serializable]
public class BGMSource
{
    public string name;
    public AudioClip clip;
}
