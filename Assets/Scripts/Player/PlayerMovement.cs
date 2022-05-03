//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour{
	[Header("Refferences")]
	[SerializeField] CharacterController controller;
	
	[Header("Player stats")]
	[SerializeField] [Range(3f, 8f)] float crouchSpeed = 1.5f;
	[SerializeField] [Range(1f, 5f)] float walkSpeed = 2.5f;
	[SerializeField] [Range(3f, 8f)] float runSpeed = 3.75f;
	[SerializeField] [Range(1f, 3f)] float jumpHeight = 1.5f;
	
	float gravity = -9.81f;
	float finalSpeed;
	
	[Header("Physics checks")]
	[SerializeField] Transform groundCheck;
	[SerializeField] float groundDistance;
	[SerializeField] LayerMask groundMask;

	Vector3 velocity;
	[HideInInspector] bool isGrounded;
	
	//Variables for references
	public static bool isWalking;
	public static bool isRuning;

	void Start() {
		finalSpeed = walkSpeed;
	}

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
		
		//Movement
		Vector3 move = transform.right * x + transform.forward * z;
		
		#region Sprint and Crouch
		
		//Sprint with Left Shift
		if(Input.GetKeyDown(KeyCode.LeftShift))
			finalSpeed = runSpeed;
			
		if(Input.GetKeyUp(KeyCode.LeftShift))
			finalSpeed = walkSpeed;
		
		//Crouching with Left Control
		
		if(Input.GetKeyDown(KeyCode.LeftAlt)){
			finalSpeed = crouchSpeed;
			transform.localScale = new Vector3(0.6f, 0.3f, 0.6f);
		}
		
		if(Input.GetKeyUp(KeyCode.LeftAlt)){
			finalSpeed = runSpeed;
			transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
		}

		#endregion
		
		controller.Move(move * finalSpeed * Time.deltaTime);
		
		//Jumping
		if(Input.GetButtonDown("Jump") && isGrounded){
			velocity.y = Mathf.Sqrt(jumpHeight * -1f * gravity);
		}
		
		//Not used for now
		/*	#region Running and Walking checks
		
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
		
		#endregion	*/
		
		velocity.y += gravity * Time.deltaTime;
		
		controller.Move(velocity * Time.deltaTime);
	}
}