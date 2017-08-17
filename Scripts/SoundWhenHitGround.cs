using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [RequireComponent(typeof (AudioSource))]
public class SoundWhenHitGround : MonoBehaviour {

	public AudioClip impact;
    private AudioSource audioSource;
    public Renderer rend;
    public bool isTouchingGround = false;
    public Rigidbody2D rb;


	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource>();
		rend = GetComponent<Renderer>();
	}


    void OnTriggerEnter2D(Collider2D other) {
		//--is the other trigger a vehicle? 

		if (other.tag == "Ground"){
			if(rend.isVisible && !isTouchingGround){

				Debug.Log("MAG "+rb.velocity.magnitude);
				if(rb.velocity.magnitude > 3){
					

					audioSource.pitch = 0.5f;
					audioSource.PlayOneShot(impact, 0.7F);

					isTouchingGround = true;
				}
			}
     
		} 

	}

	void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Ground" && rend.isVisible){

			isTouchingGround = false;
        	
		} 
    }
}
