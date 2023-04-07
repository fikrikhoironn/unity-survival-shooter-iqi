using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct QuestData
{
    public ObjectiveData[] objectives;
}

[Serializable]
public struct ObjectiveData
{
    [SerializeField]
    public GameObject enemy;
    public int enemyCount;
}
