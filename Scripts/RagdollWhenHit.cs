using UnityEngine;
using System.Collections;

//-- removes kinematic from all subobjects when hit 
//-- requires a box collider on the parent object set to trigger

public class RagdollWhenHit : MonoBehaviour {

	public Animator animator;
	public GameObject body;
	// private Rigidbody2D rb;
	private BoxCollider2D collider;
	// public Renderer rend;
	public bool alive = true;
	public Vector2 initialPosition;
	// public Rigidbody2D[] childRbs;
	public Renderer[] childRends;
	public bool doesReset = false;
    public GameObject RagdollRef; //--obj to clone when turning to ragdoll
    private GameObject newRagdoll;

	// Use this for initialization
	void Start () {
		// rb = body.GetComponent<Rigidbody2D>();
		collider = GetComponent<BoxCollider2D>();
		// childRbs = GetComponentsInChildren<Rigidbody2D>( ) as Rigidbody2D[];
		childRends = GetComponentsInChildren<Renderer>( ) as Renderer[];
		initialPosition = transform.localPosition;
		// rend = GetComponent<Renderer>();
	}
	

	void OnTriggerEnter2D(Collider2D other) {

		// Debug.Log("other = "+other.name);

        if (other.tag == "DynamicLand" || (other.tag == "PlayerVehicle" && gameObject.tag != "Player")){

            Vector2 otherVelocity = other.GetComponent<Rigidbody2D>().velocity;

            Debug.Log("collided with "+other.name+" mag = "+otherVelocity.magnitude);

            if(alive && otherVelocity.magnitude > 1){

            	alive = false;

                gameObject.SetActive(false);

                SpawnRagdoll();

		        Vector2 force = otherVelocity * 10;
		        Debug.Log("hit force = "+force);

                AddImpulseForce newRagdollScript = newRagdoll.GetComponent<AddImpulseForce>();
                newRagdollScript.AddForce(force);

                Invoke("Reset",5);
            }
        }
    }

    void Reset(){
    	gameObject.SetActive(true);
        alive = true;
    }


    void SpawnRagdoll(){
        newRagdoll = Instantiate(RagdollRef, transform.position, transform.rotation);
        // newRagdoll.transform.parent = transform;
    }
 

}
