using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    Light[] lights;
    public bool active = false;
    bool inRange = false;
    public Canvas shopUI;
    public Camera viewCam;
    private ItemDisplay itemDisplay;

    public float camDelay = 2f;
    float camTimer = 0f;

    void Start()
    {
        lights = GetComponentsInChildren<Light>(true);
        itemDisplay = GetComponentInChildren<ItemDisplay>();
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            foreach (Light light in lights)
            {
                // lerp intensity to 1
                light.intensity = Mathf.Lerp(light.intensity, 2, Time.deltaTime);
            }

            // Set viewCam.enable to true for camDelay seconds
            if (camTimer < camDelay)
            {
                camTimer += Time.deltaTime;
                viewCam.enabled = true;
            }
            else
            {
                viewCam.enabled = false;
            }

            if (inRange && Input.GetKeyDown(KeyCode.E) && Time.timeScale > 0)
            {
                Time.timeScale = 0;
                shopUI.enabled = true;
                itemDisplay.Init();
            }
            if (shopUI.enabled && Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 1;
                shopUI.enabled = false;
            }
        }
        else
        {
            foreach (Light light in lights)
            {
                light.intensity = Mathf.Lerp(light.intensity, 0, Time.deltaTime);
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = true;
            GetComponentInChildren<ItemDisplay>().SetWallet(other.GetComponent<Wallet>());
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
        }
    }
}
