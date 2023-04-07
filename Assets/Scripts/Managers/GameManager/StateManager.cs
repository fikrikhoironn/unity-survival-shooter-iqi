using UnityEngine;
using System;

public class StateManager : MonoBehaviour
{
    private TimeSpan _time;
    public TimeSpan time
    {
        get { return _time; }
    }

    public float delayBetweenLevels = 5;

    public bool paused = false;

    void Start()
    {
        if (DataManager.instance.currentSaveData != null)
        {
            _time = TimeSpan.Parse(DataManager.instance.currentSaveData.time);
        }
        else
        {
            _time = new TimeSpan(0, 0, 0, 0, 0);
        }
    }

    void Update()
    {
        if (!paused && QuestManager.instance.isQuestComplete)
        {
            switchPause();
        }
        else if (!paused)
        {
            _time += TimeSpan.FromSeconds(Time.deltaTime);
        }
        else if (paused && QuestManager.instance.isQuestComplete)
        {
            if (delayBetweenLevels > 0)
            {
                delayBetweenLevels -= Time.deltaTime;
            }
            else
            {
                switchPause();
                increaseLevel();
                delayBetweenLevels = 5;
            }
        }
    }

    public void increaseLevel()
    {
        DataManager.instance.currentSaveData.level++;
        QuestManager.instance.startLevel();
    }

    public void switchPause()
    {
        paused = !paused;
    }
}
