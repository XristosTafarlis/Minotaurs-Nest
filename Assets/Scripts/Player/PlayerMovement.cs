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
	[HideInInspector] public bool isGrounded;
	
	//Variables for reference
	public static bool isWalking;
	public static bool isRuning;

	void Update(){
		//Checking if player is grounded
		isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
		
		//Aplying gravity
		if(isGrounded && velocity.y <0){
			velocity.y = -2f;
		}
		
		//Input
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
		if(Input.GetButton("Jump") && isGrounded){
			velocity.y = Mathf.Sqrt(jumpHeight * -1f * gravity);
		}
		
		#region Running and Walking checks
		
		//Checking if player is on ground and moving
		if( (Mathf.Abs(GetComponent<CharacterController>().velocity.x) > 0.5f || Mathf.Abs(GetComponent<CharacterController>().velocity.z) > 0.5f ) && isGrounded){
			isWalking = true;
			isRuning = false;
			if(Input.GetButton("Fire3")){
				isWalking = false;
				isRuning = true;
			}
		}else{
			isWalking = false;
			isRuning = false;
		}
		
		#endregion
		
		velocity.y += gravity * Time.deltaTime;
		
		controller.Move(velocity * Time.deltaTime);
	}
}