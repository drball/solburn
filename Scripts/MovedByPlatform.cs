using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovedByPlatform : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D other){

		Debug.Log("player collision");

		if(other.transform.tag == "MovingPlatform"){
			Debug.Log("player collision with platform");
			transform.parent = other.transform;
		}
	}


	void OnCollisionExit2D(Collision2D other){

		if(other.transform.tag == "MovingPlatform"){
			Debug.Log("player separated  from platform");
			transform.parent = null;
		}
	}

}
