using UnityEngine;
using System.Collections;

public class CarController : MonoBehaviour {

	public WheelJoint2D FrontWheel;
	public WheelJoint2D BackWheel;

	// JointMotor2D motorFront;
	// JointMotor2D motorBack;

	public float speedForward;
	public float speedBackward;
	public Vector2 respawnPos;
	public bool active = false;
	public Renderer[] childRends;
	public TouchControls TouchControls;
	public GameObject ThrusterL;
	// public GameObject ThrusterR;
	public ParticleSystem DustParticles;
	public Transform RaycastBottom;
	public Animator animator;

	private Vector2 initialPos;
	public Renderer rend;

	private Vector2 frontWheelInitialPos;
	private Vector2 backWheelInitialPos;

	public Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		// rb = GetComponent<Rigidbody2D>();
		initialPos = transform.localPosition;
		respawnPos = initialPos;

		childRends = GetComponentsInChildren<Renderer>( ) as Renderer[];

		if(FrontWheel){
			frontWheelInitialPos = FrontWheel.transform.localPosition;
			backWheelInitialPos = BackWheel.transform.localPosition;
		}
		
		if(ThrusterL){
			ThrusterL.SetActive(false);
		}

		DustParticles.enableEmission = false;
	}

	
	// Update is called once per frame
	void Update () {

		DustParticles.enableEmission = false;

		if(active){
			RaycastHit2D nearGroundHit = Physics2D.Linecast(transform.position, RaycastBottom.position, 1 << LayerMask.NameToLayer("Ground"));

			if (nearGroundHit)
			{
				// Debug.Log("HIT "+nearGroundHit.point);
				DustParticles.enableEmission = true;
				DustParticles.transform.position = nearGroundHit.point;
			}
		}

	}

	void FixedUpdate () {

		if(active){

			if(ThrusterL){
				ThrusterL.SetActive(false);
			}

			if(TouchControls.RightPressed) {

				rb.AddForce(transform.right * speedForward);

				if(ThrusterL){
					ThrusterL.SetActive(true);
				}

			} else if (TouchControls.LeftPressed){

				rb.AddForce(transform.right * -speedBackward);
				// ThrusterR.SetActive(true);
			}

			//--set a max speed
			rb.velocity = Vector2.ClampMagnitude(rb.velocity, speedForward/10);

			//--constrain the rotation
			if( transform.rotation.z > 0.3f ){
				// Debug.Log("too far right");
				rb.AddTorque(-4,0);
			} else if ( transform.rotation.z < -0.3f){
				rb.AddTorque(4,0);
				// Debug.Log("too far left");
			}
		}
	}


	public void Respawn(){
		Debug.Log("Respawn player to "+respawnPos);

		//--reset the wheels first, as this affects the entire pos for some reason
		FrontWheel.transform.localPosition = frontWheelInitialPos;
		BackWheel.transform.localPosition = backWheelInitialPos;

		transform.position = respawnPos;
		transform.rotation = Quaternion.identity;

		
	}

	void UpdateRespawnPoint(Vector2 newPos){
		Debug.Log("updating respawnPos to "+newPos);
		respawnPos = newPos;
	}

	public void ActivateVehicle(){
		active = true;
		StartCoroutine("Blink");
		Debug.Log("activating "+gameObject.name);
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


}
