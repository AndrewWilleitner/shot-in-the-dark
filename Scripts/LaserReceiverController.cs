using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserReceiverController : MonoBehaviour {

    private Power power;
    private LineRenderer lineRender;

    public Material powerOnMaterial;
    public Material powerOffMaterial;

    // Use this for initialization
    void Start()
    {
        power = transform.GetComponent<Power>();
        lineRender = transform.GetComponent<LineRenderer>();

        if (power.powered)
            lineRender.material = powerOnMaterial;
        else
            lineRender.material = powerOffMaterial;
    }


    void Update()
    {
        if (power.powered)
            lineRender.material = powerOnMaterial;
        else
            lineRender.material = powerOffMaterial;
    }
}
