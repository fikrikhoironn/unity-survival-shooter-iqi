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

    public Sprite swordSprite;
    public GameObject swordPrefab;

    public GameObject shotgunSprite;
    public GameObject shotgunPrefab;


    public Transform playerTransform;



    // objects




    // Start is called before the first frame update
    void Awake()
    {

        WeaponUI = GameObject.Find("WeaponUI");
        weponIcon = WeaponUI.transform.Find("WeaponIcon").GetComponent<Image>();
        WeaponExtraAttributes = WeaponUI.transform.Find("WeaponExtraAttributes").gameObject;
        WeaponSpawnPoint = GameObject.Find("WeaponSpawnPoint");
        playerTransform = GameObject.Find("Player").transform;

        selectedWeapon = 4;
    }
    void Start()
    {

        // ChangeWeapon(selectedWeapon);
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
                GameObject WeaponUI = GameObject.Find("WeaponUI");
                GameObject WeaponExtraAttributes = WeaponUI.transform.Find("WeaponExtraAttributes").gameObject;
                GameObject chargeBar = Instantiate(bowChargeBarPrefab, WeaponExtraAttributes.transform);

                // instansiate bow with parent WeaponSpawnPoint
                if (bowPrefab == null)
                {
                    Debug.Log("Bow Prefab is null");
                }
                currentWeapon = Instantiate(bowPrefab, WeaponSpawnPoint.transform.position, Quaternion.identity, WeaponSpawnPoint.transform);

                // look at rotation player forward
                Quaternion rotation = Quaternion.LookRotation(playerTransform.forward);



                break;
            case 1:
                weponIcon.sprite = gunSprite;
                break;
            case 3:
                weponIcon.sprite = swordSprite;

                currentWeapon = Instantiate(swordPrefab, WeaponSpawnPoint.transform.position, Quaternion.identity, WeaponSpawnPoint.transform);
                break;
            default:
                break;
        }

    }
}
