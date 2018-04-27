using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupSaveData : MonoBehaviour {

	// Use this for initialization
	void Start () {
		PlayerPrefs.SetInt ("CURRENT_LEVEL_NUMBER", -1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
