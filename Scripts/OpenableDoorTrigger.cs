using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenableDoorTrigger : MonoBehaviour {

	public OpenableDoor DoorScript;
	public bool doorSwitch = false;
	public Animator animator;
	public PlayerCharacterMovement PlayerScript;
	// Use this for initialization
	void Start () {
		// Debug.Log("the door is "+DoorScript.open);

		doorSwitch = DoorScript.open;

		if(doorSwitch){
			Debug.Log("switch is on");
		} else{
			Debug.Log("switch is off");
		}
		// ToggleDoor();
	}
	
	void ToggleDoor(){
		Debug.Log("toggling switch");

		PlayerScript.DoAnimation();

		if(doorSwitch){
			doorSwitch = false;
			DoorScript.Close();
		} else{
			doorSwitch = true;
			DoorScript.Open();
		}
		animator.SetBool("DoorSwitch", doorSwitch);

	}

	void OnTriggerEnter2D(Collider2D other) {

		if (other.tag == "Player"){
			ToggleDoor();
		} 

		//--show enter dialog
	}
}
