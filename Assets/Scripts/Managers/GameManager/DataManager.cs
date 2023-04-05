using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    public GameData[] saves = new GameData[3];
    public int currentSaveIndex = -1;
    public GameData currentSaveData
    {
        get
        {
            if (currentSaveIndex < 0 || currentSaveIndex >= saves.Length)
            {
                Debug.LogError("Invalid save index");
                return null;
            }
            return saves[currentSaveIndex];
        }
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        loadAllGameData();
        if (instance == null)
        {
            instance = this;
        }
    }

    private void loadAllGameData()
    {
        for (int i = 0; i < saves.Length; i++)
        {
            saves[i] = loadGameData(i);
        }
    }

    private GameData loadGameData(int saveIndex)
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

    public void save()
    {
        if (currentSaveIndex < 0 || currentSaveIndex >= saves.Length)
        {
            Debug.LogError("Invalid save index");
            return;
        }
        // TODO: Get current game data from other object
        saveGameData(currentSaveData, currentSaveIndex);
    }

    private void saveGameData(GameData gameData, int index)
    {
        if (gameData != null)
        {
            saves[index] = gameData;
            string json = JsonUtility.ToJson(gameData);
            File.WriteAllText(Application.persistentDataPath + "/save" + index + ".json", json);
            return;
        }
        else
        {
            // Delete save file
            if (File.Exists(Application.persistentDataPath + "/save" + index + ".json"))
            {
                File.Delete(Application.persistentDataPath + "/save" + index + ".json");
            }
            saves[index] = null;
        }
    }
}
