using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

	// public GameObject TurretHead;
	public GameObject target;
	private Vector3 initialScale;
	private Vector3 reverseScale;
	public bool isActive = false;
	private float distance;
	public float fireRange = 3;

	// Use this for initialization
	void Start () {
		initialScale = transform.localScale;
		reverseScale = new Vector3(initialScale.x, -initialScale.y, initialScale.z);
	}
	
	// Update is called once per frame
	void Update () {

		if(isActive){

			var angle = Mathf.Atan2(target.transform.position.y - transform.position.y, target.transform.position.x - transform.position.x) * Mathf.Rad2Deg;

			Debug.Log("Angle = "+angle);
	 		transform.rotation = Quaternion.Euler(0f, 0f, angle);

	 		if(angle > 90 || angle < -90){
				transform.localScale = reverseScale;
	 		} else {
	 			transform.localScale = initialScale;
	 		}

	 		distance = Vector2.Distance(transform.position, target.transform.position);

	 		if (distance > fireRange){
	 			//--target is too far
	 			target = null;
	 			isActive = false;
	 		}
	 	}

	}

	void OnTriggerEnter2D(Collider2D other) {

		if(isActive == false){
			if (other.tag == "Player" || other.tag == "PlayerVehicle"){
				target = other.gameObject;
				isActive = true;
			}
		}
	}

}
