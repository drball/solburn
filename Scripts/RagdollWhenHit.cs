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
    public Rigidbody2D newRagdollRb;

	// Use this for initialization
	void Start () {
		// rb = body.GetComponent<Rigidbody2D>();
		collider = GetComponent<BoxCollider2D>();
		// childRbs = GetComponentsInChildren<Rigidbody2D>( ) as Rigidbody2D[];
		childRends = GetComponentsInChildren<Renderer>( ) as Renderer[];
		initialPosition = transform.localPosition;
		// rend = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	// void Update () {
	// 	if(Input.GetKey("d")){
	//         rb.AddForce(transform.up * 10, ForceMode2D.Impulse);
	//         Debug.Log("up!");
	//     }
	// }

	void OnTriggerEnter2D(Collider2D other) {

		// Debug.Log("other = "+other.name);

        if (other.tag == "DynamicLand" || other.tag == "PlayerVehicle"){

            Debug.Log("collided with "+other.name);

            if(alive){

            	alive = false;

                Hide(); //--hide the character
        		
		        // MakeFloppy();
                SpawnRagdoll();

				collider.enabled = false;

				if (animator){
                    animator.enabled = false;
                }
				
		        Vector2 force = other.GetComponent<Rigidbody2D>().velocity * 10;
		        Debug.Log("hit force = "+force);

		        // rb.AddForce(force, ForceMode2D.Impulse);

                Debug.Log("add force "+force+" to npc");

		        // Invoke("Reset",2);

        		Invoke("StartFlashing",2.5f);
		       
            }
        }
        
    }

    void StartFlashing(){
    	Debug.Log("start flashing");
    	StartCoroutine("Blink");
    }

    IEnumerator Blink(){
    	Hide();
    	yield return new WaitForSeconds(0.02f);
    	Show();
    	yield return new WaitForSeconds(0.02f);
    	Hide();
    	yield return new WaitForSeconds(0.02f);
    	Show();
    	yield return new WaitForSeconds(0.02f);
    	Hide();
    	Destroy(gameObject);
    }

    void Reset(){
    		// Debug.Log("reset");
    		// // EnableAllRbs();
    		// animator.enabled = true;
    		// animator.SetTrigger("MakeIdle");
    	Destroy(gameObject);
    }

    void Hide(){
		//-disable renderer of all children
    	foreach( Renderer childRend in childRends ){
            childRend.enabled = false;
            // Debug.Log("make "+childRend+" hidden");
           
        }
    }

    void Show(){
		//-enable renderer of all children
    	foreach( Renderer childRend in childRends ){
            childRend.enabled = true;
            // Debug.Log("make "+childRend+" show");
        }
    }

    // void MakeFloppy(){
    // 	//--make ragdoll
    // 	foreach( Rigidbody2D childRb in childRbs ){
    //         childRb.isKinematic = false;
    //     }
    // }

    void SpawnRagdoll(){
        newRagdoll = Instantiate(RagdollRef, transform.position, transform.rotation);
        newRagdoll.transform.parent = transform;
    }

   //  void EnableAllRbs(){

   //  	foreach( Rigidbody2D childRb in childRbs ){
			// // childRb.gameObject.transform.parent = parentObj.transform;
   //          childRb.isKinematic = true;
   //          Debug.Log("make "+childRb+" not kinematic");
   //          childRb.angularVelocity = 0f;
			// childRb.velocity = Vector2.zero;
   //      }
   //  }

 

}
