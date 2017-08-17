using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//--starts walking 

public class WalkingNPC : MonoBehaviour {

	public Animator animator;
	public float walkingSpeed = 1;
	public enum walkingDirections {Right = 0, Left = 1} 
	public walkingDirections walkingDirection= walkingDirections.Right & walkingDirections.Left;
	public Transform RaycastEnd;
	private bool hitEdge; 
	public RagdollWhenHit RagdollScript;
	private Vector3 initialScale;
	private Vector3 reverseScale;
	private Rigidbody2D rb;


	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		RagdollScript = GetComponent<RagdollWhenHit>();
		// animator.SetTrigger("MakeWalk");
		rb = GetComponent<Rigidbody2D>();

		initialScale = transform.localScale;
		reverseScale = new Vector3(-initialScale.x, initialScale.y, initialScale.z);

		// Debug.Log(gameObject.name+"walking dir="+walkingDirection);

		if(walkingDirection == walkingDirections.Left){
			// Debug.Log("start left");
			transform.localScale = reverseScale;
		}
	}
	
	void Update () {


		if(RagdollScript.alive){

			// transform.Translate((Vector2.right * walkingSpeed) * Time.deltaTime);

			//--raycast
			Debug.DrawLine(transform.position, RaycastEnd.position, Color.green);
			hitEdge = Physics2D.Linecast(transform.position, RaycastEnd.position, 1 << LayerMask.NameToLayer("Helper"));

			if(hitEdge){
				Debug.Log("hot edge");
				Turn();
			}
		}
	}

	void FixedUpdate () {
		

		animator.SetFloat("Speed", Mathf.Abs(rb.velocity.magnitude));

		if(walkingDirection == walkingDirections.Left){
			Debug.Log("go left");
			rb.velocity = new Vector2(-walkingSpeed, rb.velocity.y);
		} else if (walkingDirection == walkingDirections.Right) {
			Debug.Log("go right");
			rb.velocity = new Vector2(walkingSpeed, rb.velocity.y);	
		}

	}

	void Turn(){
		Debug.Log(gameObject.name+" change direction");

		if(walkingDirection == walkingDirections.Left){
			Debug.Log("Change to right");
			walkingDirection = walkingDirections.Right;
			transform.localScale = initialScale;
		} else if (walkingDirection == walkingDirections.Right) {
			walkingDirection = walkingDirections.Left;
			transform.localScale = reverseScale;	
			Debug.Log("Change to left");
		}
	}

}
