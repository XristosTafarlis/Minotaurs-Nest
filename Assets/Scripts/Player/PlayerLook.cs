using UnityEngine;

public class PlayerLook : MonoBehaviour{
	[SerializeField] Transform playerBody;
	public static float mouseSensitivity = 1f;
	float xRotation = 0f;
	
	void Start(){
		Cursor.lockState = CursorLockMode.Locked;
	}

	void Update(){
		
		Look();
		
		if(Input.GetMouseButton(1)){
			ZoomIn();
		}if(Input.GetMouseButtonUp(1)){
			ZoomOut();
		}
	}
	
	void ZoomIn(){
		GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, 30, 0.045f);
	}
	
	void ZoomOut(){
		GetComponent<Camera>().fieldOfView = 60f;	//Zoom out
	}
	
	void Look(){
		if(!Menu.gameIsPaused){
		float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity / 2;
		float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity / 2;
			
		xRotation -= mouseY;
		xRotation = Mathf.Clamp(xRotation, -90f, 90f);
		
		transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
		playerBody.Rotate(Vector3.up * mouseX);
		}
	}
}