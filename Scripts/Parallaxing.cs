// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Parallaxing : MonoBehaviour {

// 	public Transform[] backgrounds;		// Array of all backgrounds to be parallaxed
// 	private float[] parallaxScales;		// the proportion of the camera's movement to move the backgrounds by
// 	public float smoothing = 1f;		// How smooth the parallax is going to be. Make sure to set this above 0

// 	private Transform cam;
// 	private Vector3 previousCamPos;		// the camera pos in the previous frame
// 	private int david;

// 	void Awake(){
// 		cam = Camera.main.transform;
// 	}

// 	// Use this for initialization
// 	void Start () {
// 		previousCamPos = cam.position;
// 		parallaxScales = new float[backgrounds.Length];

// 		for(int i = 0; i < backgrounds.Length; i++){
// 			parallaxScales[i] = backgrounds[i].position.z*-1;
// 			Debug.Log("parallaxScales= "+parallaxScales[i]);
// 		}


// 	}
	
// 	// Update is called once per frame
// 	void Update () {


// 		//--for each bg
// 		for (int i = 0; i < backgrounds.Length; i++){
// 			// the parallax is the oppositie of the camera movement because the previous frame multiplied by the scale
// 			float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];

// 			// set  a target x position which is the current position plus the parallax 
// 			float backgroundTargetPosX = backgrounds[i].position.x + parallax;

// 			// create a target position which is the bg's current position with its target x position 
// 			Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

// 			// fade between current position and the target position using lerp
// 			backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);

// 			previousCamPos = cam.position;

// 			Debug.Log(david+"update "+i+" parallax "+parallax);

// 			david++;
// 		}
// 	}
// }
