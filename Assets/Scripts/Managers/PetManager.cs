using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PetManager : MonoBehaviour
{
    public static PetManager instance;

    public Transform petTransform = null;

    [SerializeField]
    public PetPrefabs[] petPrefabs = null;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        PetType currentPet = DataManager.instance.currentSaveData.playerData.currentPet;
        if (currentPet != PetType.NONE)
        {
            SpawnPet(currentPet);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (petTransform)
            DataManager.instance.currentSaveData.playerData.petHealth = (int)
                petTransform.GetComponent<PetHealth>().currentHealth;
    }

    public void SpawnPet(PetType petType)
    {
        print("Pet Type: " + petType);
        GameObject petPrefabsData = null;

        for (int i = 0; i < petPrefabs.Length; i++)
        {
            if (petPrefabs[i].petType == petType)
            {
                petPrefabsData = petPrefabs[i].petPrefab;
                break;
            }
        }
        print("Pet Prefabs Data: " + petPrefabsData);

        if (petPrefabsData != null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            GameObject pet = Instantiate(
                petPrefabsData,
                player.transform.position + new Vector3(0, 0.5f, 0),
                player.transform.rotation,
                transform
            );
            petTransform = pet.transform;
            DataManager.instance.currentSaveData.playerData.currentPet = petType;
        }
    }
}

[Serializable]
public class PetPrefabs
{
    public PetType petType;
    public GameObject petPrefab;
}
