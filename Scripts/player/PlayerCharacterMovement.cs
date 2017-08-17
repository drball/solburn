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
	public float upForce = 1;
	public Transform RaycastBottom;
	public bool onGround;
	public Bar EnergyBar; //--script on the UI energy bar
	public int EnergyMax = 300;
	public int EnergyCurrent;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		initialScale = transform.localScale;
		reverseScale = new Vector3(-initialScale.x, initialScale.y, initialScale.z);
		EnergyCurrent = EnergyMax;
	}

	void Update(){

		//--check if there's ground underneath
		onGround = Physics2D.Linecast(transform.position, RaycastBottom.position, 1 << LayerMask.NameToLayer("Ground"));
			
	}
	
	void FixedUpdate () {

		if(active){

			if (TouchControls.LeftPressed && TouchControls.RightPressed){

				if(EnergyCurrent > 0){
					rb.AddRelativeForce (Vector2.up * upForce);
					rb.AddRelativeForce (transform.localScale * 8);
					animator.SetBool("Fly", true);
					EnergyCurrent--;
					EnergyBar.UpdateBar(EnergyCurrent, EnergyMax);
				}

			} else {
				if(TouchControls.RightPressed) {

					rb.velocity = new Vector2(speed, rb.velocity.y);
					transform.localScale = initialScale;

				} else if (TouchControls.LeftPressed){
					rb.velocity = new Vector2(-speed, rb.velocity.y);
					transform.localScale = reverseScale;
				} 

				animator.SetBool("Fly", false);
				animator.SetFloat("Speed", Mathf.Abs(rb.velocity.magnitude));
				
				if(EnergyCurrent < EnergyMax){
					EnergyCurrent += 2;

					if (EnergyCurrent > EnergyMax){
						EnergyCurrent = EnergyMax;
					}
					EnergyBar.UpdateBar(EnergyCurrent, EnergyMax);
				} 
			}
		}
	}

	public void Stop(){
		//--stop the character
		animator.SetFloat("Speed", 0);
	}

	public void PullSwitch(){
		active = false;
		Stop();
		// rb.velocity = new Vector2(0, 0);
		animator.SetTrigger("MakePullSwitch");
		Invoke("Continue", 1);
	}

	void Continue(){
		//--continue after being paused
		active = true;
	}



}
