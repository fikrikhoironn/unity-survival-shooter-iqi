using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundCamera : MonoBehaviour
{
    public Camera topCamera;
    public Camera frontCamera;

    public float topTime = 3.0f;
    public float frontTime = 8.0f;

    float curAnimationTime = 0.0f;

    // Animate the camera between the top and front views
    // Top camera is active for topTime seconds, then front camera is active for frontTime seconds
    // While active, top camera rotate from y_rot = 1.525 to y_rot = -27.134
    // While active, front camera translate from z = -29.3 to z = 10.0
    // Repeat


    // Start is called before the first frame update
    void Start()
    {
        topCamera.enabled = true;
        frontCamera.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        curAnimationTime += Time.deltaTime;

        if (curAnimationTime < topTime)
        {
            if (!topCamera.enabled)
            {
                topCamera.enabled = true;
                frontCamera.enabled = false;
            }
            topCamera.transform.rotation = Quaternion.Euler(
                41.79f,
                1.525f + (curAnimationTime / topTime) * (-28.659f),
                0.0f
            );
        }
        else if (curAnimationTime < topTime + frontTime)
        {
            if (!frontCamera.enabled)
            {
                topCamera.enabled = false;
                frontCamera.enabled = true;
            }
            frontCamera.transform.position = new Vector3(
                0.0f,
                1.9f,
                -29.3f + (curAnimationTime - topTime) / frontTime * 39.3f
            );
        }
        else
        {
            curAnimationTime = 0.0f;
        }
    }
}
