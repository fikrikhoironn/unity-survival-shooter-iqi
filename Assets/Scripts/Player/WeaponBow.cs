using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponBow : MonoBehaviour
{
    public int initialDamagePerShot = 100;

    public float maxChargePower;
    public float maxChargeTime;

    public Slider slider;

    public int damagePerShot;

    public Arrow arrowPrefab;

    public Arrow currentArrow;

    int shootableMask;

    private Transform transform;

    private Transform playerTransform;

    private float firepowerPerSecond;
    private float firepower;

    float currentChargeTime = 0f;
    bool isCharging = false;

    public GameObject chargeBarPrefab;



    void Awake()
    {
        //GetMask
        shootableMask = LayerMask.GetMask("Shootable");

        damagePerShot = initialDamagePerShot;

        transform = GetComponent<Transform>();

        playerTransform = transform.parent;



        firepowerPerSecond = maxChargePower / maxChargeTime;

        Debug.Log("firepowerPerSecond: " + firepowerPerSecond);

        GameObject WeaponUI = GameObject.Find("Weapon");
        GameObject chargeBar = Instantiate(chargeBarPrefab, WeaponUI.transform);
        slider = chargeBar.GetComponent<Slider>();
        Debug.Log("WeaponUI: " + WeaponUI);
        Debug.Log("chargeBar: " + chargeBar);
        Debug.Log("slider: " + slider);

    }

    void Start()
    {
        Debug.Log("maxChargePower: " + maxChargePower);
        slider.maxValue = maxChargePower;

        // find child component
        currentArrow = transform.GetChild(0).GetComponent<Arrow>();

        // set position to zero
        currentArrow.transform.localPosition = Vector3.zero;

        // slider = GameObject.Find("ChargeBar").GetComponent<Slider>();
        // instansiate chargeBar



    }

    void Update()
    {
        if (Input.GetButtonUp("Fire1") && isCharging)
        {
            isCharging = false;
            Fire(firepower);
            currentChargeTime = 0f;
            if (firepower > maxChargePower)
            {
                firepower = maxChargePower;
            }
            slider.value = maxChargePower;

            Debug.Log("charge power: " + firepower);
        }
        if (Input.GetButtonDown("Fire1"))
        {
            isCharging = true;
            currentChargeTime = 0f;
            slider.value = maxChargePower;

        }
        
        if (isCharging)
        {
            currentChargeTime += Time.deltaTime;
            firepower = currentChargeTime * firepowerPerSecond;
            slider.value = maxChargePower - firepower;
        }
    }

    void FixedUpdate()
    {


    }

    public void Fire(float firePower)
    {
        var force = playerTransform.TransformDirection(Vector3.forward);
        // elevate y axis
        // normalize force
        force.Normalize();

        // multiply force with firepower
        force *= firePower;

        this.currentArrow.Fly(force);
        this.currentArrow.setEnemyTag("Enemy");
        Debug.Log("force: " + force);

        // print force magnitude
        Debug.Log("force magnitude: " + force.magnitude);

        // destroy arrow after 3 seconds
        Destroy(this.currentArrow.gameObject, 3f);

        // instantiate new arrow
        this.currentArrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity, transform);
        // set local position to zero
        this.currentArrow.transform.localPosition = Vector3.zero;
        // set local rotation to zero
        this.currentArrow.transform.localRotation = Quaternion.Euler(-90, 0, 0);


    }

    // on destroy
    void OnDestroy()
    {
        Destroy(this.currentArrow.gameObject);

        // destroy slider
        Destroy(slider.gameObject);
    }



}
