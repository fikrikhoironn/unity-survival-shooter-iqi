using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
    GameObject WeaponUI;
    Image weponIcon;
    GameObject WeaponExtraAttributes;
    GameObject WeaponSpawnPoint;

    GameObject currentWeapon;

    public Sprite gunSprite;

    public GameObject gunPrefab;

    private int selectedWeapon;

    // bow
    public Sprite bowSprite;
    public GameObject bowChargeBarPrefab;
    public GameObject bowPrefab;

    public Sprite swordSprite;
    public GameObject swordPrefab;

    public GameObject shotgunSprite;
    public GameObject shotgunPrefab;

    public Transform playerTransform;

    bool[] weaponUnlocked = new bool[4] { true, false, false, false };

    // objects




    // Start is called before the first frame update
    void Awake()
    {
        WeaponUI = GameObject.Find("WeaponUI");
        weponIcon = WeaponUI.transform.Find("WeaponIcon").GetComponent<Image>();
        WeaponExtraAttributes = WeaponUI.transform.Find("WeaponExtraAttributes").gameObject;
        WeaponSpawnPoint = GameObject.Find("WeaponSpawnPoint");
        playerTransform = GameObject.Find("Player").transform;

        weaponUnlocked = DataManager.instance.currentSaveData.playerData.unlockedWeapons;
        selectedWeapon = 1;
    }

    void Start()
    {
        ChangeWeapon(selectedWeapon);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && weaponUnlocked[0])
        {
            selectedWeapon = 1;
            ChangeWeapon(selectedWeapon);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && weaponUnlocked[1])
        {
            selectedWeapon = 2;
            ChangeWeapon(selectedWeapon);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && weaponUnlocked[2])
        {
            selectedWeapon = 3;
            ChangeWeapon(selectedWeapon);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && weaponUnlocked[3])
        {
            selectedWeapon = 4;
            ChangeWeapon(selectedWeapon);
        }
    }

    void ChangeWeapon(int weaponIdx)
    {
        if (!weaponUnlocked[weaponIdx - 1])
        {
            Debug.Log("Weapon " + weaponIdx + " is not unlocked");
            // return;
        }
        Debug.Log("Change to weapon " + weaponIdx);

        // clear current weapon attributes
        foreach (Transform child in WeaponExtraAttributes.transform)
        {
            Destroy(child.gameObject);
        }

        // destroy current weapon
        if (currentWeapon != null)
        {
            Destroy(currentWeapon);
        }

        switch (weaponIdx)
        {
            case 4:
                weponIcon.sprite = bowSprite;
                GameObject WeaponUI = GameObject.Find("WeaponUI");
                GameObject WeaponExtraAttributes = WeaponUI.transform
                    .Find("WeaponExtraAttributes")
                    .gameObject;
                GameObject chargeBar = Instantiate(
                    bowChargeBarPrefab,
                    WeaponExtraAttributes.transform
                );

                // instansiate bow with parent WeaponSpawnPoint
                if (bowPrefab == null)
                {
                    Debug.Log("Bow Prefab is null");
                }
                currentWeapon = Instantiate(
                    bowPrefab,
                    WeaponSpawnPoint.transform.position,
                    Quaternion.identity,
                    WeaponSpawnPoint.transform
                );

                // look at rotation player forward
                Quaternion rotation = Quaternion.LookRotation(playerTransform.forward);

                break;
            case 1:
                weponIcon.sprite = gunSprite;

                // spawn gun with default transform from prefab
                currentWeapon = Instantiate(gunPrefab, WeaponSpawnPoint.transform);
                break;
            case 3:
                weponIcon.sprite = swordSprite;

                currentWeapon = Instantiate(
                    swordPrefab,
                    WeaponSpawnPoint.transform.position,
                    Quaternion.identity,
                    WeaponSpawnPoint.transform
                );
                break;
            default:
                break;
        }
    }

    public void UnlockWeapon(int weaponIdx)
    {
        weaponUnlocked[weaponIdx] = true;
        DataManager.instance.currentSaveData.playerData.unlockedWeapons[weaponIdx] = true;
    }
}
