using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothing = 5f;
    Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        // mendapatkan offset antara target dan camera
        offset = transform.position - target.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 tempTarget = target.position;
        // If right mouse key is pressed
        if (Input.GetMouseButton(1))
        {
            // Get the raycast hit
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                // Set tempTarget between target and hit point
                tempTarget = (target.position + hit.point) / 2;
            }
        }
        // mendapat posisi camera
        Vector3 targetCamPos = tempTarget + offset;

        // set posisi camera dengan smoothing
        transform.position = Vector3.Lerp(
            transform.position,
            targetCamPos,
            smoothing * Time.deltaTime
        );
    }
}
