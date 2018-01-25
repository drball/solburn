using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

	private float speed = 500f;
	private Vector3 explodePos; //--the position it explodes (if hit something other than trigger)
	public Rigidbody2D rb;
	public GameObject Explosion;
	public GameObject ExplosionSelf;

	// Use this for initialization
	void Start () {
		Destroy(gameObject,3);

		rb.AddForce(transform.right * speed);
	}
	
	// Update is called once per frame
	void Update () {
		// transform.Translate(transform.x * speed * Time.smoothDeltaTime);
		// transform.Translate(Vector3.right * speed * Time.deltaTime);
	}

	void OnTriggerEnter2D(Collider2D other) {

		// Debug.Log("other = "+other.name);

        if (other.tag == "PlayerVehicle" || other.tag == "Player"){

        	// Debug.Log("dire = "+transform.rotation);

        	if(other.GetComponent<ShipController>()){
        		other.GetComponent<ShipController>().HitByBullet(rb.velocity);

        		Instantiate(Explosion, transform.position, transform.rotation);

            	Destroy(gameObject);
    		} else if (other.GetComponent<PlayerCharacterMovement>()) {
    		
    			HitPlayer();
    		}

    		
        } else if (other.tag == "Crate" || other.tag == "NPC"){
			if(other.GetComponent<AddImpulseForce>()){
				other.GetComponent<AddImpulseForce>().AddForce(rb.velocity*15f);
				DestroySelf();
			}
        } 
    }

    void OnCollisionEnter2D(Collision2D coll) {
        DestroySelf();
    }

    void DestroySelf(){
    	
		GameObject exp = Instantiate(ExplosionSelf, transform.position, transform.rotation);

		exp.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
		
    	Destroy(gameObject);
    }

    public void HitPlayer(){

    	//--player looks for triggeEnters too, and sometimes it gets hit by bullet before bullet detects it
    	DestroySelf();
    }
}
