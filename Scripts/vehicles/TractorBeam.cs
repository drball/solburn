using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TractorBeam : MonoBehaviour {

	public bool isActive = false;
	public SpringJoint2D joint;
	public GameObject target;
	public LineRenderer lineRenderer;
	public Material beamMaterial;
	private float beamWidth = 0.05f;
	public ShipController ShipController;
	private Rigidbody2D targetRb;
	private int cancelTimer;
	// private Vector3 heading;

	// Use this for initialization
	void Start () {
		joint.enabled = false;

		// //--use linerenderer for the beam 
		// lineRenderer = gameObject.AddComponent<LineRenderer>();
		// lineRenderer.material = beamMaterial;

		InvokeRepeating("Timer", 1, 1);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey("c")){
	        //activate spring joint with target

	        if(isActive){
				disableTractorBeam();
        	}else {
        		enableTractorBeam();
        	}

	        enableTractorBeam();
	    }

	    if(isActive){
	    	lineRenderer.SetVertexCount(2);
			lineRenderer.SetPosition(0, target.transform.position);
			lineRenderer.SetPosition(1, transform.position);	
			// lineRenderer.SetWidth(beamWidth, beamWidth);
			// lineRenderer.sortingLayerName = "Player Character";

			//check if target is higher than player
			Debug.Log("dir = "+(target.transform.position.y - transform.position.y));
			if ((target.transform.position.y - transform.position.y) > 0.5f) {
				disableTractorBeam();
			};
			
			

	    } else {
	    	lineRenderer.SetVertexCount(0);
	    }
	}

	void enableTractorBeam(){

		joint.enabled = true;
		isActive = true;
		targetRb = target.GetComponent<Rigidbody2D>();
		joint.connectedBody = targetRb;
	}

	void disableTractorBeam(){

		joint.enabled = false;
		isActive = false;
	}

	void OnTriggerEnter2D(Collider2D other) {

		if(ShipController.active){
			
			if (other.name == "wire crate"){
				// Debug.Log("ufo collided with "+other.name);

				target = other.gameObject;
				enableTractorBeam();
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

			if(cancelTimer > 2){
				// disableTractorBeam();
			}
		}
	}
}
