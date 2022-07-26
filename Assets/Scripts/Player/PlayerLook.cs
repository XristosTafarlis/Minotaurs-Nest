using System.Collections;
using UnityEngine;

public class PlayerLook : MonoBehaviour{
	[SerializeField] Transform playerBody;
	[HideInInspector] public bool shake;
	
	public static float mouseSensitivity = 1f;
	float xRotation = 0f;
	
	void Start(){
		Cursor.lockState = CursorLockMode.Locked;
	}

	void Update(){		
		
		if(shake){
			StartCoroutine(Shaking());
		}
		
		Look();
		if(Input.GetMouseButton(1)){
			ZoomIn();
		}if(Input.GetMouseButtonUp(1)){
			ZoomOut();
		}
	}
	
	public IEnumerator Shaking(){
		float elapsed = 0.0f;
		shake = false;
		
		while (elapsed < 0.35f){
			float x = Random.Range(-0.1f, 0.1f);
			float y = Random.Range(0.6f, 0.8f);
			
			transform.localPosition = new Vector3 (x, y, 0);
			
			elapsed += Time.deltaTime;
			
			yield return null;
		}
		transform.localPosition = new Vector3(0, 0.7f, 0);
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