using UnityEngine;

public class FootstepScript : MonoBehaviour{
	[Header ("Refferences")]
	[SerializeField] AudioClip[] footstepClips;
	[SerializeField] GameObject player;
	[SerializeField] float crouchSpeed = 0.85f;
	[SerializeField] float walkingSpeed = 0.6f;
	[SerializeField] float runnigSpeed = 0.45f;

	float footstepTimer = 0;

	AudioSource audioSource;

	void Start(){
		audioSource = GetComponent<AudioSource>();
	}

	void Update()	{
		HandleFootesteps();
	}

	void HandleFootesteps(){
		if(!player.GetComponent<PlayerMovement>().isGrounded) return;
		if(!player.GetComponent<PlayerMovement>().isWalking) return;

		footstepTimer -= Time.deltaTime;

		if(footstepTimer <= 0 ){
			audioSource.clip = footstepClips[Random.Range(0, footstepClips.Length)];
			audioSource.Play();
			if(Input.GetKey(KeyCode.LeftShift)){
				footstepTimer = runnigSpeed;
			}else if(Input.GetKey(KeyCode.LeftControl)){
				footstepTimer = crouchSpeed;
			}else{
				footstepTimer = walkingSpeed;
			}
		}
	}
}