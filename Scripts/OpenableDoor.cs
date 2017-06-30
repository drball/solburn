using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenableDoor : MonoBehaviour {

	public bool open = false;
	public GameObject DoorObj;
	public Animator animator;
	public float delay = 1f; 
	public bool persistent = false;

	void Awake () {

		//--load from playerprefs

		if(persistent){
			open = (PlayerPrefs.GetInt("Door") == 1) ? true : false;
		}
		

		ChangeStatus();
	}

	void ChangeStatus(){

		Debug.Log("open is now "+open);
		
		animator.SetBool("isOpen",open);

		if(persistent){
			PlayerPrefs.SetInt("Door", (open == true) ? 1 : 0);
		}
		
	}

	public void Open(){

		open = true;
		Invoke("ChangeStatus",delay);

	}

	public void Close(){

		open = false;
		Invoke("ChangeStatus",delay);
		
	}

}
