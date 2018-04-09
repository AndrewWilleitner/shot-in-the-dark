using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatformController : MonoBehaviour {

    public Power[] powerSwitches;
    public bool platformPowered;

    private Vector3 unpoweredRotation;
    public Vector3 poweredRotation;

    // Use this for initialization
    void Start()
    {
        unpoweredRotation = transform.parent.rotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        platformPowered = true;
        for (int i = 0; i < powerSwitches.Length; i++)
        {
            if (!powerSwitches[i].powered)
            {
                platformPowered = false;
            }
        }

        RotatePlatform();
    }


    private void RotatePlatform()
    {
        if (platformPowered)
        {
            transform.parent.localEulerAngles = Vector3.MoveTowards(transform.parent.rotation.eulerAngles, poweredRotation, 30 * Time.deltaTime);
        }
        else
        {
            transform.parent.localEulerAngles = Vector3.MoveTowards(transform.parent.rotation.eulerAngles, unpoweredRotation, 30 * Time.deltaTime);
        }
    }
}
