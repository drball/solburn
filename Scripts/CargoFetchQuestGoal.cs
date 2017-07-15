using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoFetchQuestGoal : MonoBehaviour {

	public GameObject target; //--the item we need to fetch
	public float distance;
	public bool goalComplete = false;
	private CargoScript CargoScript;
	public GameController GameController;

	// Use this for initialization
	void Start () {
		CargoScript = target.GetComponent<CargoScript>();
	}
	
	// Update is called once per frame
	void Update () {
		distance = Vector2.Distance(target.transform.position,transform.position);

		if(distance < 0.5f && CargoScript.isCollectable){
			Debug.Log("goal complete!!!!");

			if(GameController.CurrentVehicle){
				GameController.VehicleDrop();
			}
		
			CargoScript.isCollectable = false;
			CargoScript.Remove();
			SendMessage("Complete");
		}
	}
}
