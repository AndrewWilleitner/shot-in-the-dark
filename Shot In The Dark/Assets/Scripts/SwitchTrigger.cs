using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchTrigger : MonoBehaviour {

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

    void OnTriggerStay(Collider coll)
    {
        if (coll.transform.tag == "GlowStone")
        {
            power.powered = true;
            lineRender.material = powerOnMaterial;
        }
        else
        {
            power.powered = false;
            lineRender.material = powerOffMaterial;
        }
    }

    void OnTriggerExit(Collider coll)
    {
        if (coll.transform.tag == "GlowStone")
        {
            power.powered = false;
            lineRender.material = powerOffMaterial;
        }
    }
}
