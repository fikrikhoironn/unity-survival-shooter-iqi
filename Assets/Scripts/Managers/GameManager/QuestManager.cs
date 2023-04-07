using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public RectTransform QuestTracker;
    private RectTransform content;
    public QuestData[] quests;
    public Dictionary<string, int> enemyKills = new Dictionary<string, int>();
    public QuestData activeQuest
    {
        get { return quests[DataManager.instance.currentSaveData.level - 1]; }
    }
    public bool isQuestComplete
    {
        get
        {
            foreach (ObjectiveData objective in activeQuest.objectives)
            {
                if (enemyKills.ContainsKey(objective.enemyName))
                {
                    if (enemyKills[objective.enemyName] < objective.enemyCount)
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

    void Start()
    {
        content = QuestTracker.GetChild(0).GetChild(0).GetComponent<RectTransform>();
        EnemyHealth.onEnemyKilled += addKillCount;
        for (int i = 0; i < activeQuest.objectives.Length; i++)
        {
            enemyKills.Add(activeQuest.objectives[i].enemyName, 0);
        }
    }

    public void nextLevel()
    {
        if (isQuestComplete)
        {
            DataManager.instance.currentSaveData.level++;
            for (int i = 0; i < activeQuest.objectives.Length; i++)
            {
                enemyKills.Add(activeQuest.objectives[i].enemyName, 0);
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
                    activeQuest.objectives[i].enemyName
                    + ": "
                    + enemyKills[activeQuest.objectives[i].enemyName]
                    + "/"
                    + activeQuest.objectives[i].enemyCount;
            }
            else
            {
                GameObject objective = Instantiate(
                    Resources.Load("Objective") as GameObject,
                    content
                );
                objective.GetComponent<TMPro.TextMeshPro>().text =
                    activeQuest.objectives[i].enemyName
                    + ": "
                    + enemyKills[activeQuest.objectives[i].enemyName]
                    + "/"
                    + activeQuest.objectives[i].enemyCount;
                objective.transform.SetParent(content);
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
