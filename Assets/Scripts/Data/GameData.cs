using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GameData
{
    public string saveName = "My Game";
    public int level = 1;
    public string time = TimeSpan.FromSeconds(0).ToString(); // String representation of the timeSpan
    public PlayerData playerData = new PlayerData();

    public GameData copy()
    {
        GameData copy = new GameData();
        copy.saveName = saveName;
        copy.level = level;
        copy.time = time;
        copy.playerData = playerData.copy();
        return copy;
    }
}
