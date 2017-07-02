using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballChain : MonoBehaviour {

	public Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		rb.isKinematic = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void PulledSwitch (){
		rb.isKinematic = false;
	}
}
