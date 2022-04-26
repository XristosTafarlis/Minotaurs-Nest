//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour{
	
	[SerializeField] private CharacterController controller;
	
	[SerializeField] private float speed = 3f;
	[SerializeField] private float runSpeedMultiplier = 2f;
	[SerializeField] private float gravity = -9.81f;
	[SerializeField] private float jumpHeight = 3f;
	
	[SerializeField] private Transform groundCheck;
	[SerializeField] private float groundDistance;
	[SerializeField] private LayerMask groundMask;

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
			speed *= runSpeedMultiplier;
		}if(Input.GetButtonUp("Fire3")){
			speed /= runSpeedMultiplier;
		}
		controller.Move(move * speed * Time.deltaTime);
		
		//Jumping
		if(Input.GetButtonDown("Jump") && isGrounded){
			velocity.y = Mathf.Sqrt(jumpHeight * -1f * gravity);
		}
		
		velocity.y += gravity * Time.deltaTime;
		
		controller.Move(velocity * Time.deltaTime);
	}
}