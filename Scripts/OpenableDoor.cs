using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenableDoor : MonoBehaviour {

	public bool open = false;
	public string name;
	public GameObject DoorObj;
	public float delay; 

	void Awake () {

		//--load from playerprefs


		open = (PlayerPrefs.GetInt("Door") == 1) ? true : false;

		ChangeStatus(open);
	}

	void ChangeStatus(bool newStatus){
		open = newStatus;

		Debug.Log("open is now "+open);
		
		if(open) {
			DoorObj.SetActive(false);
		} else {
			DoorObj.SetActive(true);
		}

		// PlayerPrefs.SetInt("Door", open);
	}

	public void Open(){

		ChangeStatus(true);

	}

	public void Close(){

		ChangeStatus(false);
		
	}

}
