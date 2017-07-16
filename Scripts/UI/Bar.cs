using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour {

	public float fillAmount;
	public Image BarImage;
	// private float barValue;
	// private float maxBarValue; 

	// Use this for initialization
	void Start () {
		UpdateBar(1f,1f);
	}
	
	public void UpdateBar(float barValue, float maxBarValue){
		fillAmount = barValue/maxBarValue;
		BarImage.fillAmount = fillAmount;
	}
}
