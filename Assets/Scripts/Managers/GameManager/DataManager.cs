using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    public GameData[] saves = new GameData[3];
    public GameData currentSaveData;

    float timer = 0f;
    float cutSceneTime = 25f;
    bool loadLevel1Done = false;
    bool clickedButtonStartGame = false;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        LoadAllGameData();
        if (instance == null)
        {
            instance = this;
        }
    }

    private void LoadAllGameData()
    {
        for (int i = 0; i < saves.Length; i++)
        {
            saves[i] = LoadGameData(i);
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        // Scene 1 can be played if startgame button is clicked, timer already passed the cut scene 1 and cut scene is done
        if (clickedButtonStartGame && timer >= cutSceneTime && !loadLevel1Done)
        {
            SceneManager.LoadScene("Scenes/Level 1", LoadSceneMode.Single);
            loadLevel1Done = true;
        }
    }

    public void InstantiateGame()
    {
        currentSaveData = new GameData();
        SceneManager.LoadScene("Scenes/Cut Scenes/Cut Scene 1", LoadSceneMode.Single);
        timer = 0f;
        clickedButtonStartGame = true;
    }

    private GameData LoadGameData(int saveIndex)
    {
        if (saveIndex < 0 || saveIndex >= saves.Length)
        {
            Debug.LogError("Invalid save index");
            return null;
        }
        if (File.Exists(Application.persistentDataPath + "/save" + saveIndex + ".json"))
        {
            string json = File.ReadAllText(
                Application.persistentDataPath + "/save" + saveIndex + ".json"
            );
            GameData data = JsonUtility.FromJson<GameData>(json);
            return data;
        }
        else
        {
            return null;
        }
    }

    public void LoadGame(int saveIndex)
    {
        if (saveIndex < 0 || saveIndex >= saves.Length)
        {
            Debug.LogError("Invalid save index");
            return;
        }
        currentSaveData = saves[saveIndex];
        if (currentSaveData == null)
        {
            Debug.LogError("Save file does not exist");
            return;
        }

        // Change scene to level1
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scenes/Level 1");

        // Search for the player and set the player's position to the saved position
        GameObject player = GameObject.Find("Player");
        player.transform.position = new Vector3(-22.5f, 0, 0);
    }

    public void SaveGameData(GameData gameData, int saveIndex)
    {
        if (saveIndex < 0 || saveIndex >= saves.Length)
        {
            Debug.LogError("Invalid save index");
            return;
        }
        if (gameData != null)
        {
            gameData.level += 1;
            saves[saveIndex] = gameData;
            string json = JsonUtility.ToJson(gameData);
            File.WriteAllText(Application.persistentDataPath + "/save" + saveIndex + ".json", json);
            return;
        }
        else
        {
            // Delete save file
            if (File.Exists(Application.persistentDataPath + "/save" + saveIndex + ".json"))
            {
                File.Delete(Application.persistentDataPath + "/save" + saveIndex + ".json");
            }
            saves[saveIndex] = null;
        }
    }
}
