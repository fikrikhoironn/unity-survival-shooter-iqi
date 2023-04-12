using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour
{
    public Canvas saveCanvas;
    public Transform arrowSignTransform;
    private GameObject arrowSign;
    private bool _active = false;
    public bool active
    {
        get { return _active; }
        set
        {
            _active = value;
            arrowSign.SetActive(value);
        }
    }

    void Start()
    {
        arrowSign = arrowSignTransform.gameObject;
        arrowSign.SetActive(false);
        saveCanvas.enabled = false;
    }

    private void openCanvas()
    {
        Time.timeScale = 0;
        saveCanvas.enabled = true;
    }

    public void closeCanvas()
    {
        Time.timeScale = 1;
        saveCanvas.enabled = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            if (active)
                openCanvas();
        }
    }
}
