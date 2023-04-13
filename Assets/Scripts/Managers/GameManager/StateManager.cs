using UnityEngine;
using System;

public class StateManager : MonoBehaviour
{
    private TimeSpan _time;
    public TimeSpan time
    {
        get { return _time; }
    }

    public Transform shopTransform;
    public Transform saveTransform;
    Shop shop;
    Save save;

    public float delayBetweenLevels = 5;
    float delay;

    public bool isBreak = false;
    public static StateManager instance;

    void Start()
    {
        shop = shopTransform.GetComponent<Shop>();
        save = saveTransform.GetComponent<Save>();
        if (DataManager.instance.currentSaveData != null)
        {
            _time = TimeSpan.Parse(DataManager.instance.currentSaveData.time);
        }
        else
        {
            _time = new TimeSpan(0, 0, 0, 0, 0);
        }
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
        if (!isBreak && QuestManager.instance.isQuestComplete)
        {
            switchBreak();
        }
        else if (!isBreak)
        {
            _time += TimeSpan.FromSeconds(Time.deltaTime);
        }
        else if (isBreak && QuestManager.instance.isQuestComplete)
        {
            if (delay > 0)
            {
                delay -= Time.deltaTime;
            }
            else
            {
                switchBreak();
                increaseLevel();
            }
        }
    }

    public void increaseLevel()
    {
        DataManager.instance.currentSaveData.level++;
        QuestManager.instance.startLevel();
    }

    public void switchBreak()
    {
        delay = delayBetweenLevels;
        isBreak = !isBreak;
        shop.active = !shop.active;
        save.active = !save.active;
    }
}
