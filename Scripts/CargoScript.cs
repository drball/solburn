using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoScript : MonoBehaviour {

	public bool isCollectable = true;
	private Renderer[] childRends;
	public Renderer rend;
	// private float speed = 3f;

	// Use this for initialization
	void Start () {
		childRends = GetComponentsInChildren<Renderer>( ) as Renderer[];
	}
	

	public void Remove(){
		//remove after a few secs 
		Invoke("BlinkThenRemove",2);
	}

	void BlinkThenRemove(){
		StartCoroutine("Blink");
	}

	void Hide(){
		//-disable renderer of all children
    	foreach( Renderer childRend in childRends ){
            childRend.enabled = false;
        }
        rend.enabled = false;
    }

    void Show(){
		//-enable renderer of all children
    	foreach( Renderer childRend in childRends ){
            childRend.enabled = true;
        }
        rend.enabled = true;
    }

    IEnumerator Blink(){
    	// Debug.Log("starting blink");
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
		yield return new WaitForSeconds(0.05f);

    	gameObject.SetActive(false);
    }
}



