using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class PetManager : MonoBehaviour
{
    public static PetManager instance;

    private Transform _petTransform = null;
    public Transform petTransform
    {
        get { return _petTransform; }
        set
        {
            _petTransform = value;
            if (value != null)
            {
                petHealthSlider.gameObject.SetActive(true);
            }
            else
            {
                petHealthSlider.gameObject.SetActive(false);
            }
        }
    }
    public Slider petHealthSlider;

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
            petHealthSlider.gameObject.SetActive(true);
        }
        else
        {
            petHealthSlider.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (petTransform)
        {
            DataManager.instance.currentSaveData.playerData.petHealth = (int)
                petTransform.GetComponent<PetHealth>().currentHealth;
            petHealthSlider.value = DataManager.instance.currentSaveData.playerData.petHealth;
        }
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
