using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using System.Linq;

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
    public Canvas bossHealthCanvas;
    public bool isQuestComplete
    {
        get
        {
            var objectiveSum = new Dictionary<string, int>();
            foreach (ObjectiveData objective in activeQuest.objectives)
            {
                if (objectiveSum.ContainsKey(objective.enemy.name))
                {
                    objectiveSum[objective.enemy.name] += objective.enemyCount;
                }
                else
                {
                    objectiveSum.Add(objective.enemy.name, objective.enemyCount);
                }
            }
            foreach (string objective in objectiveSum.Keys)
            {
                if (enemyKills.ContainsKey(objective))
                {
                    if (enemyKills[objective] < objectiveSum[objective])
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
    public float spawnDelay = 2f;
    public float spawnDelayTimer = 0f;
    private int i,
        j = 0;

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
        i = 0;
        j = 0;
        enemyKills.Clear();
        for (int i = 0; i < activeQuest.objectives.Length; i++)
        {
            if (!enemyKills.ContainsKey(activeQuest.objectives[i].enemy.name))
            {
                enemyKills.Add(activeQuest.objectives[i].enemy.name, 0);
            }
            if (
                activeQuest.objectives[i].enemy.GetComponent<EnemyHealth>().enemyType
                == EnemyType.Boss
            )
            {
                spawnDelay = 4f;
                bossHealthCanvas.gameObject.SetActive(true);
            }
            else
            {
                spawnDelay = 2f;
                bossHealthCanvas.gameObject.SetActive(false);
            }
        }
    }

    void Update()
    {
        // Sum all objective based on enemy name
        var objectiveSum = new Dictionary<string, int>();
        foreach (ObjectiveData objective in activeQuest.objectives)
        {
            if (objectiveSum.ContainsKey(objective.enemy.name))
            {
                objectiveSum[objective.enemy.name] += objective.enemyCount;
            }
            else
            {
                objectiveSum.Add(objective.enemy.name, objective.enemyCount);
            }
        }
        // Update content for each objective in active quest
        for (int i = 0; i < objectiveSum.Keys.Count; i++)
        {
            if (i < content.childCount)
            {
                RectTransform objective = content.GetChild(i).GetComponent<RectTransform>();
                objective.GetComponent<TextMeshProUGUI>().text =
                    objectiveSum.Keys.ElementAt(i)
                    + ": "
                    + (
                        enemyKills.TryGetValue(objectiveSum.Keys.ElementAt(i), out int value)
                            ? value
                            : 0
                    )
                    + "/"
                    + objectiveSum[objectiveSum.Keys.ElementAt(i)];
            }
            else
            {
                GameObject objective = Instantiate(
                    Resources.Load("Objective") as GameObject,
                    content
                );
                objective.GetComponent<TMPro.TextMeshProUGUI>().text =
                    objectiveSum.Keys.ElementAt(i)
                    + ": "
                    + enemyKills[objectiveSum.Keys.ElementAt(i)]
                    + "/"
                    + objectiveSum[objectiveSum.Keys.ElementAt(i)];
            }
        }
        for (int i = activeQuest.objectives.Length; i < content.childCount; i++)
        {
            Destroy(content.GetChild(i).gameObject);
        }
        spawnDelayTimer += Time.deltaTime;
        if (spawnDelayTimer >= spawnDelay)
        {
            spawnDelayTimer = 0f;
            spawnEnemy();
        }
    }

    private void spawnEnemy()
    {
        if (i < activeQuest.objectives.Length)
        {
            if (j < activeQuest.objectives[i].enemyCount)
            {
                // Get random position on navmesh with min distance of 10 and max distance of 30
                Vector3 vec = Random.insideUnitSphere * 20;
                NavMeshHit hit;
                NavMesh.SamplePosition(vec + vec.normalized * 10, out hit, 30, NavMesh.AllAreas);
                // Spawn enemy
                GameObject enemy = Instantiate(
                    activeQuest.objectives[i].enemy,
                    hit.position,
                    Quaternion.identity
                );
                j++;
            }
            else
            {
                i++;
                j = 0;
            }
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
