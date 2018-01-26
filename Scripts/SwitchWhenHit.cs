using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//--when hit by a player, disable the initial obj & enable the new obj

public class SwitchWhenHit : MonoBehaviour {

	public GameObject InitialObj;
	public GameObject NewObj;

	// Use this for initialization
	void Start () {
		InitialObj.SetActive(true);
		NewObj.SetActive(false);		
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		//--is the other trigger a vehicle? 

		if (other.tag == "Player" || other.tag == "PlayerVehicle"){
			
			//--check the speed 
			InitialObj.SetActive(false);
			NewObj.SetActive(true);
		} 

	}
}
