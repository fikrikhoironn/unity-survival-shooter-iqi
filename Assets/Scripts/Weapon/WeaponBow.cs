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

    private Transform playerTransform;

    private float firepowerPerSecond;
    private float firepower;

    float currentChargeTime = 0f;
    bool isCharging = false;

    public GameObject chargeBarPrefab;

    AudioSource clipArched;
    AudioSource clipReleased;

    private int floorMask;

    bool reloading = false;

    public float delay;

    void Awake()
    {
        //GetMask
        shootableMask = LayerMask.GetMask("Shootable");

        damagePerShot = initialDamagePerShot;

        playerTransform = transform.parent;

        firepowerPerSecond = maxChargePower / maxChargeTime;

        Debug.Log("firepowerPerSecond: " + firepowerPerSecond);

        GameObject WeaponExtraAttributes = GameObject.Find("WeaponExtraAttributes");

        GameObject chargeBar = WeaponExtraAttributes.transform.GetChild(0).gameObject;

        slider = chargeBar.GetComponent<Slider>();

        // audio from object archedAudio
        clipArched = transform.Find("archedAudio").GetComponent<AudioSource>();
        clipReleased = transform.Find("releasedAudio").GetComponent<AudioSource>();

        // floorMask
        floorMask = LayerMask.GetMask("Floor");
    }

    void Start()
    {
        Debug.Log("maxChargePower: " + maxChargePower);
        slider.maxValue = maxChargePower;

        // instantiate new arrow
        ReloadArrow();
    }

    void Update()
    {
        // Check if raycast hit floor
        if (Input.GetButtonUp("Fire1") && isCharging /* && StateManager.instance.isBreak == false */)
        {
            // play released
            clipReleased.time = 0.5f;
            clipReleased.Play();
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
        if (Input.GetButtonDown("Fire1") /* && StateManager.instance.isBreak == false */)
        {
            if (currentArrow != null)
            {
                // play arched, skip 0.1s using time
                clipArched.time = 0.1f;
                clipArched.Play();
                isCharging = true;
                currentChargeTime = 0f;
                slider.value = maxChargePower;
            }
        }

        if (isCharging)
        {
            currentChargeTime += Time.deltaTime;
            firepower = currentChargeTime * firepowerPerSecond;
            slider.value = maxChargePower - firepower;
        }
    }

    void FixedUpdate() { }

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
        this.currentArrow = null;

        // instantiate new arrow
        StartCoroutine(ReloadWithDelay(delay));
    }

    IEnumerator ReloadWithDelay(float delay)
    {
        if (!reloading)
        {
            reloading = true;
            yield return new WaitForSeconds(delay);
            ReloadArrow();
            reloading = false;
        }
    }

    void ReloadArrow()
    {
        this.currentArrow = Instantiate(
            arrowPrefab,
            transform.position,
            Quaternion.identity,
            transform
        );
        // set local position to zero
        this.currentArrow.transform.localPosition = Vector3.zero;
        Quaternion newRotation = Quaternion.LookRotation(playerTransform.forward);

        // set rotation
        this.currentArrow.transform.rotation = newRotation;
    }

    // on destroy
    void OnDestroy()
    {
        if (this.currentArrow != null)
        {
            Destroy(this.currentArrow.gameObject);
        }

        // destroy slider
        if (slider != null)
        {
            Destroy(slider.gameObject);
        }
    }
}
