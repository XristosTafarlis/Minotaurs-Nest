//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour{
	
	public CharacterController controller;
	
	public float speed = 12f;
	public float gravity = -9.81f;
	public float jumpHeight = 3f;
	
	public Transform groundCheck;
	public float groundDistance;
	public LayerMask groundMask;

	Vector3 velocity;
	bool isGrounded;

    void Update(){
		//Checking if player is grounded
		isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
		
		//Aplying gravity
		if(isGrounded && velocity.y <0){
			velocity.y = -2f;
		}
		
        float x = Input.GetAxis("Horizontal");
		float z = Input.GetAxis("Vertical");
		
		//Movement WASD
		Vector3 move = transform.right * x + transform.forward * z;
		
		//Sprint with Left Shift
		if(Input.GetButtonDown("Fire3")){
			speed += 1;
		}if(Input.GetButtonUp("Fire3")){
			speed -= 1;
		}
		controller.Move(move * speed * Time.deltaTime);
		
		//Jumping
		if(Input.GetButton("Jump") && isGrounded){
			velocity.y = Mathf.Sqrt(jumpHeight * -1f * gravity);
		}
		
		velocity.y += gravity * Time.deltaTime;
		
		controller.Move(velocity * Time.deltaTime);
    }
}