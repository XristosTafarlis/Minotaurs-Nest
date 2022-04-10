using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour{
	
	public int amphorasNeeded = 4;
	
	void Start(){
		
	}

	void Update(){
		if(amphorasNeeded == 0){
			Debug.Log(amphorasNeeded);
			amphorasNeeded -= 1;
		}
	}
	
	private void OnTriggerEnter(Collider other) {
		if(other.gameObject.CompareTag("Amphora")){
			Destroy(other.gameObject, 1f);
			amphorasNeeded -= 1;
		}
	}
}