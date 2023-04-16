using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class SceneManagerObject : MonoBehaviour
{
    string mainScene = "Scenes/Level 1";
    string[] cutScenes = new string[3]
    {
        "Scenes/Cut Scenes/Cut Scene 1",
        "Scenes/Cut Scenes/Cut Scene 2",
        "Scenes/Cut Scenes/Cut Scene 3"
    };
    public int currentCutSceneIndex;
    string mainMenu = "Scenes/Main Menu";

    public PlayableDirector playableDirector
    {
        get
        {
            GameObject cutscene = GameObject.FindGameObjectWithTag("Cutscene");
            if (cutscene != null)
            {
                return cutscene.GetComponent<PlayableDirector>();
            }
            else
            {
                return null;
            }
        }
    }
    public static SceneManagerObject instance;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (playableDirector != null)
        {
            if (playableDirector.state == PlayState.Playing)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    playableDirector.time = playableDirector.duration;
                }
            }
            else if (currentCutSceneIndex < 2)
            {
                SceneManager.LoadScene(mainScene, LoadSceneMode.Single);
            }
            else
            {
                SceneManager.LoadScene(mainMenu, LoadSceneMode.Single);
            }
        }
    }

    public void PlayCutscenes(int index)
    {
        currentCutSceneIndex = index;
        SceneManager.LoadScene(cutScenes[currentCutSceneIndex], LoadSceneMode.Single);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(mainMenu, LoadSceneMode.Single);
    }
}
