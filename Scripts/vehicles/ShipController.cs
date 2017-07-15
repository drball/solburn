using UnityEngine;
using System.Collections;

public class ShipController : MonoBehaviour {

	public Vector2 respawnPos;
	public bool active = false;
	private Renderer[] childRends;
	public TouchControls TouchControls;

	private Vector2 initialPos;
	public Renderer rend;

	private Rigidbody2D rb;

	public ParticleSystem ThrustParticlesLeft;
	public ParticleSystem ThrustParticlesRight;
	public GameObject ThrusterL;
	public GameObject ThrusterR;

	public float speed = 10f;
	private float upSpeed;
	public float rotationSpeed = 6f;
	public Animator animator;
	public Transform RaycastBottomFar;
	public Transform RaycastBottomNear;
	public bool landingDistance;
	public ParticleSystem DustParticles;
	private int badRotationTimer;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		initialPos = transform.localPosition;
		respawnPos = initialPos;

		childRends = GetComponentsInChildren<Renderer>( ) as Renderer[];

		ThrustParticlesLeft.enableEmission = false;
		ThrustParticlesRight.enableEmission = false;

		upSpeed = speed * 2.5f;

		DustParticles.enableEmission = false;

		InvokeRepeating("Timer", 1, 1);

		if(active){
			//--mostly for debug - if we start with this vehicle active 
			ActivateVehicle();
		}

	}

	void Update () {

		DustParticles.enableEmission = false;

		if(active){

			//--show/hide the dust effect & legs

			RaycastHit2D nearGroundHit = Physics2D.Linecast(transform.position, RaycastBottomFar.position, 1 << LayerMask.NameToLayer("Ground"));
			landingDistance = Physics2D.Linecast(transform.position, RaycastBottomNear.position, 1 << LayerMask.NameToLayer("Ground"));
			// Debug.DrawLine(transform.position, RaycastBottomNear.position, Color.red);

			if (nearGroundHit)
			{
				// Debug.Log("HIT "+nearGroundHit.point);
				DustParticles.enableEmission = true;
				DustParticles.transform.position = nearGroundHit.point;
	
			}

			animator.SetBool("NearGround", landingDistance);
			
		}
	}

	
	//--for physics movement, use FixedUpdate
	void FixedUpdate () {


		ThrusterL.SetActive(false);
		ThrusterR.SetActive(false);

		if(active){

			// ThrustParticlesLeft.enableEmission = false;
			// ThrustParticlesRight.enableEmission = false;

			if (TouchControls.LeftPressed && TouchControls.RightPressed){

				rb.AddRelativeForce (Vector2.up * upSpeed);

				// ThrustParticlesLeft.enableEmission = true;
				// ThrustParticlesRight.enableEmission = true;

				ThrusterL.SetActive(true);
				ThrusterR.SetActive(true);

				if( transform.rotation.z > 0.1f ){
					rb.AddTorque(-rotationSpeed*2,0);
				} else if ( transform.rotation.z < -0.1f){
					rb.AddTorque(rotationSpeed*2,0);
				}

			} else {
				if(TouchControls.LeftPressed) {

					
					rb.AddRelativeForce (Vector2.up * (upSpeed/1.25f));
					rb.AddRelativeForce (Vector2.left * speed);
				
					// ThrustParticlesLeft.enableEmission = true;
					ThrusterR.SetActive(true);

					if( transform.rotation.z < 0.15f ){
						rb.AddTorque(rotationSpeed,0);
					}

				} else if (TouchControls.RightPressed){

					rb.AddRelativeForce (Vector2.up * (upSpeed/1.25f));
					rb.AddRelativeForce (Vector2.right * speed);

					// ThrustParticlesRight.enableEmission = true;
					ThrusterL.SetActive(true);

					if( transform.rotation.z > -0.15f ){
						rb.AddTorque(-rotationSpeed,0);
					}

				} else {

					ThrusterL.SetActive(false);
					ThrusterR.SetActive(false);

					if( transform.rotation.z > 0.1f ){
						rb.AddTorque(-rotationSpeed*2,0);
					} else if ( transform.rotation.z < -0.1f){
						rb.AddTorque(rotationSpeed*2,0);
					}

				}
			}

		}
	}


	public void Respawn(){
		Debug.Log("Respawn player");
		transform.position = respawnPos;
		transform.rotation = Quaternion.identity;

	}

	void UpdateRespawnPoint(Vector2 newPos){
		respawnPos = newPos;
	}

	public void ActivateVehicle(){
		active = true;
		StartCoroutine("Blink");
		// Debug.Log("activating "+gameObject.name);
		animator.SetBool("Active",active);
	}

    void Hide(){
		//-disable renderer of all children
    	foreach( Renderer childRend in childRends ){
            childRend.enabled = false;
            // Debug.Log("make "+childRend+" hidden");
        }
        rend.enabled = false;
    }

    void Show(){
		//-enable renderer of all children
    	foreach( Renderer childRend in childRends ){
            childRend.enabled = true;
            // Debug.Log("make "+childRend+" show");
        }
        rend.enabled = true;
    }

    IEnumerator Blink(){
    	// Debug.Log("starting blink");
    	Hide();
    	yield return new WaitForSeconds(0.05f);
    	Show();
    	yield return new WaitForSeconds(0.05f);
    	Hide();
    	yield return new WaitForSeconds(0.05f);
    	Show();
    	yield return new WaitForSeconds(0.05f);
    	Hide();
    	yield return new WaitForSeconds(0.05f);
    	Show();
    }

    void DeactivateVehicle (){
    	active = false;
    	animator.SetBool("Active",active);
    }

    void Timer(){
    	//--1 second cron
    	//--count how long it's been upside down

    	if((transform.rotation.z > 0.7) || (transform.rotation.z < -0.7)){
			badRotationTimer++;
		} else {
			badRotationTimer = 0;
		}

	
		//--restart if been on it's side for more than 3 seconds
		if((badRotationTimer > 3) && (active == true)){
			badRotationTimer = 0;
			Respawn();
		}

    }


}
