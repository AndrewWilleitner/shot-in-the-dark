using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiStoneSwitchTrigger : MonoBehaviour {

    private Power power;
    private LineRenderer lineRender; 

    public Material powerOnMaterial;
    public Material powerOffMaterial;
    public int stoneLimit;
    public TextMesh countTextMesh;

    private int stoneCount;

    // Use this for initialization
    void Start()
    {
        power = transform.GetComponent<Power>();
        lineRender = transform.GetComponent<LineRenderer>();

        if (power.powered)
            lineRender.material = powerOnMaterial;
        else
            lineRender.material = powerOffMaterial;

        countTextMesh.text = "" + Mathf.Clamp(stoneLimit - stoneCount, 0, Mathf.Infinity);
    }


    private void Update()
    {
        if (stoneCount >= stoneLimit)
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

    void OnTriggerEnter(Collider coll)
    {
        if (coll.transform.tag == "GlowStone")
        {
            stoneCount++;
            countTextMesh.text = "" + Mathf.Clamp(stoneLimit - stoneCount, 0, Mathf.Infinity);
        }
    }

    void OnTriggerExit(Collider coll)
    {
        if (coll.transform.tag == "GlowStone")
        {
            stoneCount--;
            countTextMesh.text = "" + Mathf.Clamp(stoneLimit - stoneCount, 0, Mathf.Infinity);
        }
    }
}
