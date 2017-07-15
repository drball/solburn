using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TractorBeam : MonoBehaviour {

	public bool isActive = false;
	public SpringJoint2D joint;
	public GameObject target;
	public CargoScript CargoScript;
	public GameObject DropButton;
	public LineRenderer lineRenderer;
	public Material beamMaterial;
	public ShipController ShipController;
	private Rigidbody2D targetRb;
	private int cancelTimer; //--counting an inactive cargo


	// Use this for initialization
	void Start () {
		joint.enabled = false;

		InvokeRepeating("Timer", 1, 1);
	}
	
	// Update is called once per frame
	void Update () {

	    if(isActive){
			lineRenderer.numPositions = 2;
			lineRenderer.SetPosition(0, target.transform.position);
			lineRenderer.SetPosition(1, transform.position);	
			// lineRenderer.SetWidth(beamWidth, beamWidth);
			// lineRenderer.sortingLayerName = "Player Character";

			//check if target is higher than player
			if ((target.transform.position.y - transform.position.y) > 0.5f) {
				disableTractorBeam();
			};
			
			
	    } else {
	    	lineRenderer.numPositions = 0;
	    }
	}

	void enableTractorBeam(){

		joint.enabled = true;
		isActive = true;
		targetRb = target.GetComponent<Rigidbody2D>();
		joint.connectedBody = targetRb;
		DropButton.SetActive(true);
	}

	void disableTractorBeam(){

		joint.enabled = false;
		isActive = false;
		DropButton.SetActive(false);
	}

	void OnTriggerEnter2D(Collider2D other) {

		if(ShipController.active){
			
			if (other.tag == "Pickuppable"){
				// Debug.Log("ufo collided with "+other.name);

				target = other.gameObject;
				CargoScript = target.GetComponent<CargoScript>();

				if(CargoScript.isCollectable){
					enableTractorBeam();
				} else {
					Debug.Log(target.name+" is not collectable");
				}
				
			}
		}
	}

	void Timer(){

		if(isActive){

			if(targetRb.velocity.magnitude < 1){
				// Debug.Log("velocity ="+targetRb.velocity.magnitude); 
				cancelTimer++;
			} else {
				cancelTimer = 0;
			}

			if(cancelTimer > 3){
				disableTractorBeam();
			}
		}
	}

	public void Drop(){
		Debug.Log("tractor beam is dropping cargo");
		disableTractorBeam();
	}
}
