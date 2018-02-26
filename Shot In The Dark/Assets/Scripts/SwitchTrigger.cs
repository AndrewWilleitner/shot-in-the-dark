using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchTrigger : MonoBehaviour {

    private Power power;

    // Use this for initialization
    void Start()
    {
        power = transform.GetComponent<Power>();
    }

    void OnTriggerStay(Collider coll)
    {
        if (coll.transform.tag == "GlowStone")
        {
            power.powered = true;
        }
        else
        {
            power.powered = false;
        }
    }

    void OnTriggerExit(Collider coll)
    {
        if (coll.transform.tag == "GlowStone")
        {
            power.powered = false;
        }
    }
}
