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

    private int selectedWeapon;


    // bow
    public Sprite bowSprite;
    public GameObject bowChargeBarPrefab;
    public GameObject bowPrefab;



    // objects




    // Start is called before the first frame update
    void Awake()
    {

        WeaponUI = GameObject.Find("Weapon");
        weponIcon = WeaponUI.transform.Find("WeaponIcon").GetComponent<Image>();
        WeaponExtraAttributes = WeaponUI.transform.Find("WeaponExtraAttributes").gameObject;
        WeaponSpawnPoint = GameObject.Find("WeaponSpawnPoint");

        selectedWeapon = 4;
    }
    void Start()
    {

        ChangeWeapon(selectedWeapon);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = 1;
            ChangeWeapon(selectedWeapon);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedWeapon = 2;
            ChangeWeapon(selectedWeapon);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectedWeapon = 3;
            ChangeWeapon(selectedWeapon);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            selectedWeapon = 4;
            ChangeWeapon(selectedWeapon);
        }
    }

    void ChangeWeapon(int weaponIdx)
    {
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
                GameObject WeaponUI = GameObject.Find("Weapon");
                GameObject WeaponExtraAttributes = WeaponUI.transform.Find("WeaponExtraAttributes").gameObject;
                GameObject chargeBar = Instantiate(bowChargeBarPrefab, WeaponExtraAttributes.transform);

                // spawn bow
                Debug.Log("Bow Prefab = " + bowPrefab);
                Debug.Log("WeaponSpawnPoint = " + WeaponSpawnPoint);
                // instansiate bow with parent WeaponSpawnPoint
                if (bowPrefab == null)
                {
                    Debug.Log("Bow Prefab is null");
                }
                currentWeapon = Instantiate(bowPrefab, WeaponSpawnPoint.transform.position, Quaternion.identity, WeaponSpawnPoint.transform);


                break;
            case 1:
                weponIcon.sprite = gunSprite;
                break;
            default:
                break;
        }

    }
}
