//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour{
	
	public CharacterController controller;

	public float speed = 12f;

    void Update(){
        float x = Input.GetAxis("Horizontal");
		float z = Input.GetAxis("Vertical");
		
		Vector3 move = transform.right * x + transform.forward * z;
		
		//Sprint with Left Shift
		if(Input.GetButtonDown("Fire3")){
			speed *= 2;
		}if(Input.GetButtonUp("Fire3")){
			speed /= 2;
		}
		controller.Move(move * speed * Time.deltaTime);
    }
}
