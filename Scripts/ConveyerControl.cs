using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyerControl : MonoBehaviour {
    public float speed;
    public Power[] powerSwitches;
    private bool beltPowered;

    private MeshRenderer rend;
    private Vector2 matOffset;
    private List<Rigidbody> rigidbodiesTouching = new List<Rigidbody>();

    // Use this for initialization
    void Start () {
        rend = transform.GetComponent<MeshRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        beltPowered = true;
        for (int i = 0; i < powerSwitches.Length; i++)
        {
            if (!powerSwitches[i].powered)
            {
                beltPowered = false;
            }
        }

        if (beltPowered)
        {
            matOffset += new Vector2(0, speed / 120 * Time.deltaTime);
            rend.materials[0].SetTextureOffset("_MainTex", matOffset);
        }

    }

    void FixedUpdate()
    {
        if (beltPowered)
        {
            float conveyorVelocity = speed * Time.deltaTime;
            foreach (Rigidbody rig in rigidbodiesTouching) {
                rig.velocity = conveyorVelocity * transform.forward;
            }
        }
    }

    void OnCollisionEnter(Collision col)
    {
        rigidbodiesTouching.Add(col.gameObject.GetComponent<Rigidbody>());
    }

    void OnCollisionExit(Collision col)
    {
        rigidbodiesTouching.Remove(col.gameObject.GetComponent<Rigidbody>());
    }
}
