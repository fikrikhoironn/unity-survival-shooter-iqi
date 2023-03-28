using System;
using UnityEngine;

public class EnemyFactory : MonoBehaviour, IFactory
{

    [SerializeField]
    public GameObject[] enemyPrefab;

    public GameObject FactoryMethod(int tag, Vector3 pos, Quaternion rot)
    {
        GameObject enemy = Instantiate(enemyPrefab[tag], pos, rot);
        return enemy;
    }
}