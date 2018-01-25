using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//--for when a vehicle touches land, create some rubble particles

public class CollideWithLand : MonoBehaviour {

	public GameObject rubbleSpark;

	// Use this for initialization
	void Start () {
		
	}
	
	void OnCollisionEnter2D(Collision2D collision) {


		ContactPoint2D contact = collision.contacts[0];
		GameObject other = collision.gameObject;
		Debug.Log("a spark collision has happened between "+gameObject.name +" and "+other.name);
		
        if (other.layer == 9){

            Debug.Log("collided with side ");

            Vector3 collidePos = contact.point;

            GameObject sparkInstance = Instantiate(rubbleSpark,
				collidePos, 
				Quaternion.identity
			);

			Destroy(sparkInstance,3);
        }
        
    }
}
