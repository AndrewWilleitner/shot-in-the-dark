using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserProjectorController : MonoBehaviour {

    public Power[] powerSwitches;
    public bool laserPowered;

    LineRenderer laserRenderer;
    Transform currentReceiver;

    // Use this for initialization
    void Start()
    {
        laserRenderer = transform.GetComponent<LineRenderer>();
        currentReceiver = null;

        if (powerSwitches.Length == 0)
            laserPowered = true;
    }
    
    void FixedUpdate()
    {
        if (powerSwitches.Length > 0)
        {
            laserPowered = true;
            for (int i = 0; i < powerSwitches.Length; i++)
            {
                if (!powerSwitches[i].powered)
                {
                    laserPowered = false;
                }
            }
        }

        LaserLine();
    }


    private void LaserLine()
    {
        // check if laser is powered
        if (laserPowered)
        {
            // Raycast the laser beam
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                laserRenderer.SetPositions(new Vector3[] { new Vector3(0,0,0), transform.InverseTransformPoint(hit.point) });
                if (hit.transform.tag == "LaserReceiver")
                {
                    currentReceiver = hit.transform;
                    currentReceiver.GetComponent<Power>().powered = true;
                }
                else
                {
                    // if laser hit something new, power off the previous Laser Receiver
                    if (currentReceiver != null)
                    {
                        currentReceiver.GetComponent<Power>().powered = false;
                        currentReceiver = null;
                    }
                }
            }
            else
            {
                laserRenderer.SetPositions(new Vector3[] { new Vector3(0, 0, 0), new Vector3(0, 0, 10000) });
                // if laser hit something new, power off the previous Laser Receiver
                if (currentReceiver != null)
                {
                    currentReceiver.GetComponent<Power>().powered = false;
                    currentReceiver = null;
                }
            }
        }
        else
        {
            laserRenderer.SetPositions(new Vector3[] { new Vector3(0, 0, 0), new Vector3(0, 0, 1) });
        }
    }
}
