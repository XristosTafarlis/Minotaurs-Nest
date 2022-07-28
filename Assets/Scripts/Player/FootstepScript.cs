using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepScript : MonoBehaviour{
	
	[SerializeField] AudioClip[] footstepClips;
	[SerializeField] GameObject footstep;
	
	void Start(){
		footstep.SetActive(false);
	}
	
	void Update()	{
		if(Input.GetKey("w")){
			footsteps();
		}

		if(Input.GetKey("s")){
			footsteps();
		}

		if(Input.GetKey("a")){
			footsteps();
		}

		if(Input.GetKey("d")){
			footsteps();
		}

		if(Input.GetKeyUp("w")){
			StopFootsteps();
		}

		if(Input.GetKeyUp("s")){
			StopFootsteps();
		}

		if(Input.GetKeyUp("a")){
			StopFootsteps();
		}

		if(Input.GetKeyUp("d")){
			StopFootsteps();
		}

	}

	void footsteps(){
		footstep.SetActive(true);
	}

	void StopFootsteps(){
		footstep.SetActive(false);
	}
}