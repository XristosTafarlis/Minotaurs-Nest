//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour{
	[Header("Refferences")]
	[SerializeField] CharacterController controller;

	[Header("Player stats")]
	[SerializeField] [Range(0.5f, 3f)] float crouchSpeed = 1.5f;
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

	//Not used for now
	#region Variables for references
	/*
	public static bool isWalking;
	public static bool isRuning;
	*/
	#endregion

	void Start() {
		finalSpeed = walkSpeed;
	}

	void Update(){
		//Checking if player is grounded
		isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

		Movement();
		Gravity();
		RunAndCrouch();
		Jump();
		//Lean();

		//Not used for now
		#region Running and Walking checks
		/*
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
		*/
		#endregion
	}

	void Movement(){
		//Input
		float x = Input.GetAxis("Horizontal");
		float z = Input.GetAxis("Vertical");

		//Applying movement
		Vector3 move = transform.right * x + transform.forward * z;
		controller.Move(move * finalSpeed * Time.deltaTime);
	}

	void Gravity(){
		//Aplying gravity
		if(isGrounded && velocity.y <0){
			velocity.y = -2f;
		}
		velocity.y += gravity * Time.deltaTime;
	}

	void RunAndCrouch(){

		//Sprint with Left Shift
		if(Input.GetKeyDown(KeyCode.LeftShift))
			finalSpeed = runSpeed;

		if(Input.GetKeyUp(KeyCode.LeftShift))
			finalSpeed = walkSpeed;

		//Crouching with Left Alt
		if(Input.GetKeyDown(KeyCode.LeftControl)){
			finalSpeed = crouchSpeed;
			transform.localScale = new Vector3(0.6f, 0.3f, 0.6f);
		}

		if(Input.GetKeyUp(KeyCode.LeftControl)){
			finalSpeed = runSpeed;
			transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
		}
	}

	void Jump(){
		if(Input.GetKeyDown(KeyCode.Space) && isGrounded){
			velocity.y = Mathf.Sqrt(jumpHeight * -1f * gravity);
		}
		controller.Move(velocity * Time.deltaTime);
	}

	//void Lean(){
	//	if(Input.GetKey(KeyCode.Q)){
	//		transform.RotateAround(groundCheck.position, new Vector3(0, 0, transform.localEulerAngles.z), 25f);
	//	}if(Input.GetKeyUp(KeyCode.Q)){
	//		transform.RotateAround(groundCheck.position, new Vector3(0, 0, transform.localEulerAngles.z), -25f);
	//	}
	//}

	private void OnDrawGizmos() {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
	}
}