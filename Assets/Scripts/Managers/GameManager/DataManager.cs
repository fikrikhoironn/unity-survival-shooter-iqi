using UnityEngine;
using System.IO;
using System;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    public GameData[] saves = new GameData[3];
    public GameData currentSaveData;

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

    public void InstantiateGame()
    {
        currentSaveData = new GameData();
        // Change scene to level1
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scenes/Level 1");
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
