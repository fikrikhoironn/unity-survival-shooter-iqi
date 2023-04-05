using UnityEngine;
using System;

public class StateManager : MonoBehaviour
{
    private int _level;
    public int level
    {
        get { return _level; }
    }
    private TimeSpan _time;
    public TimeSpan time
    {
        get { return _time; }
    }

    public bool paused = false;

    void Start()
    {
        if (DataManager.instance.currentSaveData != null)
        {
            _level = DataManager.instance.currentSaveData.level;
            _time = TimeSpan.Parse(DataManager.instance.currentSaveData.time);
        }
        else
        {
            _level = 1;
            _time = new TimeSpan(0, 0, 0, 0, 0);
        }
    }

    void Update()
    {
        if (!paused)
        {
            _time += TimeSpan.FromSeconds(Time.deltaTime);
        }
    }

    public void increaseLevel()
    {
        _level++;
    }

    public void switchPause()
    {
        paused = !paused;
    }
}
