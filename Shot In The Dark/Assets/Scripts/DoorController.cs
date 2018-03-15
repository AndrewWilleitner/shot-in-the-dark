﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {

    public float doorHeight;
    public Power[] powerSwitches;
    public bool doorPowered;

    private Vector3 openPos;
    private Vector3 closePos;

    // Use this for initialization
    void Start ()
    {
        closePos = transform.position;
        openPos = transform.position + new Vector3(0,-doorHeight + 0.2f, 0);
    }
	
	// Update is called once per frame
	void Update ()
    {
        doorPowered = true;
		for (int i = 0; i < powerSwitches.Length; i++)
        {
            if (!powerSwitches[i].powered)
            {
                doorPowered = false;
            }
        }

        DoorOpen();

    }


    private void DoorOpen()
    {
        if (doorPowered)
        {
            transform.position = Vector3.MoveTowards(transform.position, openPos, 5 * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, closePos, 5 * Time.deltaTime);
        }
    }
}