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

		//--set the switch to the status of the door
		doorSwitch = DoorScript.open;

		if(doorSwitch){
			Debug.Log("switch is on");
		} else{
			Debug.Log("switch is off");
		}
		animator.SetBool("DoorSwitch", doorSwitch);
		// ToggleDoor();
	}
	
	void ToggleSwitch(){
		Debug.Log("toggling switch");

		//--show player pulling switch
		

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
			PlayerScript.PullSwitch();

			//--pull switch soon, 
			Invoke("ToggleSwitch",0.5f);
		} 

		//--show enter dialog
	}
}
