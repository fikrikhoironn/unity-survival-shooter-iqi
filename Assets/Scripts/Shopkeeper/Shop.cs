using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    Light[] lights;
    public bool active = false;
    bool inRange = false;
    public Canvas shopUI;

    void Start()
    {
        lights = GetComponentsInChildren<Light>(true);
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
            if (inRange && Input.GetKeyDown(KeyCode.E))
            {
                Time.timeScale = 0;
                shopUI.enabled = true;
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
