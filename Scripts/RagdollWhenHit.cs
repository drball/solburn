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
    public bool canFall; //--can this guy turn to ragdoll if there's no collider underneath? 
    public bool onGround;
    public Transform RaycastBottom;
    private Vector2 force;

	// Use this for initialization
	void Start () {
		// rb = body.GetComponent<Rigidbody2D>();
		collider = GetComponent<BoxCollider2D>();
		// childRbs = GetComponentsInChildren<Rigidbody2D>( ) as Rigidbody2D[];
		childRends = GetComponentsInChildren<Renderer>( ) as Renderer[];
		initialPosition = transform.localPosition;
		// rend = GetComponent<Renderer>();
	}

	void Update(){

		//--check if there's ground underneath
		onGround = Physics2D.Linecast(transform.position, RaycastBottom.position, 1 << LayerMask.NameToLayer("Ground"));

		if(!onGround && canFall){
			MakeRagdoll();
		}
			
	}
	

	void OnTriggerEnter2D(Collider2D other) {

		// Debug.Log("other = "+other.name);

        if (other.tag == "DynamicLand" || (other.tag == "PlayerVehicle" && gameObject.tag != "Player")){

            Vector2 otherVelocity = other.GetComponent<Rigidbody2D>().velocity;

            Debug.Log("collided with "+other.name+" mag = "+otherVelocity.magnitude);

            if(alive && otherVelocity.magnitude > 1){
            	force = otherVelocity * 10;
				MakeRagdoll();
            }
        }
    }

    void Reset(){
    	gameObject.SetActive(true);
        alive = true;
    }


    void MakeRagdoll(){
        
        alive = false;

        gameObject.SetActive(false);

        newRagdoll = Instantiate(RagdollRef, transform.position, transform.rotation);

        Invoke("Reset",5);

        if(force.magnitude > 0){
        	AddImpulseForce newRagdollScript = newRagdoll.GetComponent<AddImpulseForce>();
        	newRagdollScript.AddForce(force);
        }

    }
 
}
