using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public RectTransform QuestTracker;
    private RectTransform content;
    public QuestData[] quests;
    public Dictionary<string, int> enemyKills = new Dictionary<string, int>();
    public QuestData activeQuest
    {
        get
        {
            if (DataManager.instance.currentSaveData.level <= quests.Length)
            {
                return quests[DataManager.instance.currentSaveData.level - 1];
            }
            else
            {
                var quest = new QuestData();
                quest.objectives = new ObjectiveData[0];
                return quest;
            }
        }
    }
    public static QuestManager instance;
    public bool isQuestComplete
    {
        get
        {
            foreach (ObjectiveData objective in activeQuest.objectives)
            {
                if (enemyKills.ContainsKey(objective.enemy.name))
                {
                    if (enemyKills[objective.enemy.name] < objective.enemyCount)
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
    }
    public bool autoSpawn = true;

    void Start()
    {
        content = QuestTracker.GetChild(0).GetChild(0).GetComponent<RectTransform>();
        EnemyHealth.onEnemyKilled += addKillCount;
        startLevel();
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void startLevel()
    {
        if (DataManager.instance.currentSaveData.level > quests.Length)
        {
            return;
        }
        for (int i = 0; i < activeQuest.objectives.Length; i++)
        {
            enemyKills.Add(activeQuest.objectives[i].enemy.name, 0);
            // Spawn objective
            if (autoSpawn)
            {
                for (int j = 0; j < activeQuest.objectives[i].enemyCount; j++)
                {
                    // Get random position on navmesh with min distance of 10 and max distance of 30
                    Vector3 vec = Random.insideUnitSphere * 20;
                    NavMeshHit hit;
                    NavMesh.SamplePosition(
                        vec + vec.normalized * 10,
                        out hit,
                        30,
                        NavMesh.AllAreas
                    );
                    // Spawn enemy
                    GameObject enemy = Instantiate(
                        activeQuest.objectives[i].enemy,
                        hit.position,
                        Quaternion.identity
                    );
                }
            }
        }
    }

    void Update()
    {
        // Update content for each objective in active quest
        for (int i = 0; i < activeQuest.objectives.Length; i++)
        {
            if (i < content.childCount)
            {
                RectTransform objective = content.GetChild(i).GetComponent<RectTransform>();
                objective.GetComponent<TextMeshProUGUI>().text =
                    activeQuest.objectives[i].enemy.name
                    + ": "
                    + enemyKills[activeQuest.objectives[i].enemy.name]
                    + "/"
                    + activeQuest.objectives[i].enemyCount;
            }
            else
            {
                GameObject objective = Instantiate(
                    Resources.Load("Objective") as GameObject,
                    content
                );
                objective.GetComponent<TMPro.TextMeshProUGUI>().text =
                    activeQuest.objectives[i].enemy.name
                    + ": "
                    + enemyKills[activeQuest.objectives[i].enemy.name]
                    + "/"
                    + activeQuest.objectives[i].enemyCount;
            }
        }
        for (int i = activeQuest.objectives.Length; i < content.childCount; i++)
        {
            Destroy(content.GetChild(i).gameObject);
        }
    }

    private void addKillCount(string enemyName)
    {
        if (enemyKills.ContainsKey(enemyName))
        {
            enemyKills[enemyName]++;
        }
        else
        {
            enemyKills.Add(enemyName, 1);
        }
        print(enemyName);
        print(enemyKills[enemyName]);
    }
}
