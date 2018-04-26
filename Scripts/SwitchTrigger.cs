using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchTrigger : MonoBehaviour {

    private Power power;
    private LineRenderer lineRender;
    private AudioSource source;

    public Material powerOnMaterial;
    public Material powerOffMaterial;
    public AudioClip switchOnSound;
    public AudioClip switchOffSound;

    // Use this for initialization
    void Start()
    {
        power = transform.GetComponent<Power>();
        lineRender = transform.GetComponent<LineRenderer>();
        source = GetComponent<AudioSource>();

        if (power.powered)
            lineRender.material = powerOnMaterial;
        else
            lineRender.material = powerOffMaterial;
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.transform.tag == "GlowStone")
        {
            source.PlayOneShot(switchOnSound);
            power.powered = true;
            lineRender.material = powerOnMaterial;
        }
    }

    void OnTriggerExit(Collider coll)
    {
        if (coll.transform.tag == "GlowStone")
        {
            source.PlayOneShot(switchOffSound);
            power.powered = false;
            lineRender.material = powerOffMaterial;
        }
    }
}
