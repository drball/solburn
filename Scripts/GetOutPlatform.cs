using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//--specifically for the platform which allows players to get out of their vehicle

public class GetOutPlatform : MonoBehaviour {

	public GameObject ExitButton;

	// Use this for initialization
	void Start () {
		
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		//--is the other trigger a vehicle? 

		if (other.tag == "PlayerVehicle"){
			
			ExitButton.SetActive(true);
		} 

	}

	void OnTriggerExit2D(Collider2D other) {
		//--is the other trigger a vehicle? 
		Debug.Log("van left platform");

		if (other.tag == "PlayerVehicle"){
			
			ExitButton.SetActive(false);
		} 

	}
}
