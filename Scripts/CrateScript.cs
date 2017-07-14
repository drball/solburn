using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateScript : MonoBehaviour {

	public bool isCollectable = true;
	public bool isMoving = false;
	public GameObject target;
	private float speed = 3f;

	// Use this for initialization
	void Start () {
		isMoving = true;
	}
	
	// Update is called once per frame
	void Update(){


		if(Input.GetKey("c")){
	        //activate spring joint with target
	    }


		if(isMoving == true){

			//--move to target
			// transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.transform.position.x, target.transform.position.y - 0.8f), speed * Time.deltaTime);
			// Debug.Log("distance = "+Vector2.Distance(transform.position, target.transform.position));



		}
		
	}
}



