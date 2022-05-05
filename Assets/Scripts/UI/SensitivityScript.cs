using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SensitivityScript : MonoBehaviour{
	
	Text sensitivityText;
	
	void Start(){
		sensitivityText = GetComponent<Text>();
	}

	public void textUpdate(float value){
		sensitivityText.text = ("wea");
	}
}
