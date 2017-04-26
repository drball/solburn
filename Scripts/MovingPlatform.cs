using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

	public Transform[] Waypoints;
	public float moveSpeed = 3;
	public float rotateSpeed = 0.5f;
	public float scaleSpeed = 0.5f;
	public float delayTimer = 3;
	public int CurrentPoint = 0;
	public GameObject Platform;
	float delay;

	void Update () {
		if (Platform.transform.position == Waypoints [CurrentPoint].transform.position) 
		{
			delay = Time.time + delayTimer;
			CurrentPoint += 1;
		}

		else if (Platform.transform.position != Waypoints [CurrentPoint].transform.position && delay < Time.time) 
		{
			Platform.transform.position = Vector3.MoveTowards (Platform.transform.position, Waypoints [CurrentPoint].transform.position, moveSpeed * Time.deltaTime);
			// Platform.transform.rotation = Quaternion.Lerp (Platform.transform.rotation, Waypoints [CurrentPoint].transform.rotation, rotateSpeed * Time.deltaTime);
            // Platform.transform.localScale = Vector3.Lerp (Platform.transform.localScale, Waypoints [CurrentPoint].transform.localScale, scaleSpeed * Time.deltaTime);
		}

		if( CurrentPoint >= Waypoints.Length)
		{
			CurrentPoint = 0;
		}
	}


}
