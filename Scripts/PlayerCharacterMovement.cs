using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterMovement : MonoBehaviour {

	public bool active = true;
	public float speed = 1f;
	private Rigidbody2D rb;
	private Vector3 initialScale;
	public TouchControls TouchControls;
	private Vector3 reverseScale;
	public Animator animator;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		initialScale = transform.localScale;
		reverseScale = new Vector3(-initialScale.x, initialScale.y, initialScale.z);
	}
	
	// Update is called once per frame
	void Update () {

		if(active){

			if(TouchControls.RightPressed) {

				rb.velocity = new Vector2(speed, rb.velocity.y);
				transform.localScale = initialScale;
				// animator.SetTrigger("MakeWalk");

			} else if (TouchControls.LeftPressed){
				rb.velocity = new Vector2(-speed, rb.velocity.y);
				transform.localScale = reverseScale;
				// animator.SetTrigger("MakeWalk");
			} else {
				// animator.SetTrigger("MakeIdle");
			}

			animator.SetFloat("Speed", Mathf.Abs(rb.velocity.magnitude));


		
		}
	

	}

}
