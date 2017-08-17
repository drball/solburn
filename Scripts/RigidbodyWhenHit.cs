using System.Collections;
using UnityEngine;

public class RigidbodyWhenHit : MonoBehaviour {

//-- removes kinematic from all subobjects when hit 
//-- requires a box collider on the parent object set to trigger

	private Rigidbody2D rb;
	private BoxCollider2D collider;
	// public bool alive = true;
	public Vector2 initialPosition;
	public Rigidbody2D[] childRbs;
	// public Renderer[] childRends;
	public bool doesReset = false;
    private Vector2 force;
    public bool dynamic = false;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		collider = GetComponent<BoxCollider2D>();
		childRbs = GetComponentsInChildren<Rigidbody2D>( ) as Rigidbody2D[];
		// childRends = GetComponentsInChildren<Renderer>( ) as Renderer[];
		// initialPosition = transform.localPosition;
		// rend = GetComponent<Renderer>();
		rb.isKinematic = true;
	}


	void OnTriggerEnter2D(Collider2D other) {

        if (other.tag == "DynamicLand" || other.tag == "Pickuppable" || (other.tag == "PlayerVehicle" && gameObject.tag != "Player" || other.tag == "Bullet")){

            Vector2 otherVelocity = other.GetComponent<Rigidbody2D>().velocity;

            Debug.Log("building collided with "+other.name+" mag = "+otherVelocity.magnitude);

            if(!dynamic){
            	force = otherVelocity * 5;
				MakeRigidbody();
            }

        }
    }

    // void Reset(){
    // 	gameObject.SetActive(true);
    //     alive = true;
    // }


    void MakeRigidbody(){
        
        dynamic = true;
        rb.isKinematic = false;
        gameObject.tag = "DynamicLand";

        // Invoke("Reset",10);

        if(force.magnitude > 2){            
            rb.AddForce(force, ForceMode2D.Impulse);
            force = new Vector2(0,0);
        }

    }
 
}
