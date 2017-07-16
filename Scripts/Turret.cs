using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {


	public GameObject target;
	private Vector3 initialScale;
	private Vector3 reverseScale;
	public bool isActive = false;
	private float distance;
	public float fireRange = 5;
	private float turnSpeed = 0.1f;
	public ParticleSystem Flasher;
	public float fireRate = 1f;
	public GameObject Bullet;

	// Use this for initialization
	void Start () {
		initialScale = transform.localScale;
		reverseScale = new Vector3(initialScale.x, -initialScale.y, initialScale.z);
		Flasher.enableEmission = false;
	}
	
	// Update is called once per frame
	void Update () {

		if(isActive){

			var angle = Mathf.Atan2(target.transform.position.y - transform.position.y, target.transform.position.x - transform.position.x) * Mathf.Rad2Deg;

			// Debug.Log("Angle = "+angle);
	 		// transform.rotation = Quaternion.Euler(0f, 0f, angle);
	 		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 0f, angle), Time.time * turnSpeed);

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

	 	} else {
	 		Flasher.enableEmission = false;
	 		CancelInvoke("Shoot");
	 	}

	}

	void OnTriggerEnter2D(Collider2D other) {

		if(isActive == false){
			if (other.tag == "Player" || other.tag == "PlayerVehicle"){
				target = other.gameObject;
				isActive = true;
				Flasher.enableEmission = true;
				InvokeRepeating("Shoot", fireRate, 1);
			}
		}
	}

	void Shoot(){
		Debug.Log("New bullet");
		Instantiate(Bullet, transform.position, transform.rotation);
	}

}
