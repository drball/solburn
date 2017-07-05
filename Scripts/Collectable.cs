using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//--delete this object when player touches it
//--requires a box collider set to trigger

public class Collectable : MonoBehaviour {

	// public TestScript script;
	public bool isCollectable = true;
	public bool isMoving = false;
	private GameObject target;
	private float speed = 3f;
	private float beamWidth = 0.02f;
	private float beamWidthMax = 0.2f;
	private LineRenderer lineRenderer;
	public Material beamMaterial;

	void Start(){
		//--use linerenderer for the beam that shoots the gatecube
		lineRenderer = gameObject.AddComponent<LineRenderer>();
		lineRenderer.material = beamMaterial;
	}
	
	void OnTriggerEnter2D(Collider2D other) {

		if(isCollectable){
			if (other.name == "HassleTrigger"){
				// gameObject.SendMessage("Collected");
				// Destroy(gameObject);
				isMoving = true;
				target = other.transform.parent.gameObject;
			}
		}
	}

	void Update(){

		if(isMoving == true){

			//--move to target
			transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
			speed += 0.5f;
			Debug.Log("distance = "+Vector2.Distance(transform.position, target.transform.position));

			lineRenderer.SetPosition(0, target.transform.position);
			lineRenderer.SetPosition(1, transform.position);	
			lineRenderer.SetWidth(beamWidth, beamWidth);

			if(beamWidth < beamWidthMax) {
				beamWidth += 0.005f;
			}


			//--destroy when hit
			if (Vector2.Distance(transform.position, target.transform.position) < 0.02f){
				Destroy(gameObject);
			}


		}
		
	}
}
