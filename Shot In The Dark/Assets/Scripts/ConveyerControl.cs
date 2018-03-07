using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyerControl : MonoBehaviour {
	public float speed = 200f;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionStay(Collision obj){
		float conveyorVelocity = speed * Time.deltaTime;
		Rigidbody other = obj.gameObject.GetComponent<Rigidbody>();
		other.velocity = conveyorVelocity * transform.forward;
	}
}
