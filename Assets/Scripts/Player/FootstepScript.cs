using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepScript : MonoBehaviour{
	
	[SerializeField] AudioClip[] steps;
	
	[SerializeField] AudioSource source;
	
	[SerializeField] CharacterController cc;
		
	void Start(){
		//cc = this.GetComponent<CharacterController>();
	}
	
	void Update(){
		if (cc.isGrounded == true && cc.velocity.magnitude > 2f && source.isPlaying == false){
			source.volume = Random.Range(0.8f, 1f);
			source.pitch = Random.Range(0.8f, 1.1f);
			source.clip = steps[Random.Range(0, steps.Length)];
			source.Play();
		}
	}	
}