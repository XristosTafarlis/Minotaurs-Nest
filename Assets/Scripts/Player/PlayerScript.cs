using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour{
	[Header("Refferences")]
	[SerializeField] GameObject swordHolder;
	[SerializeField] AudioSource damageAudioSuorce;
	[SerializeField] GameObject cam;
	[SerializeField] GameObject killTheMinotaur;
	[SerializeField] GameObject bloodImage;
	Color color;
	
	[Header("Variables")]
	public int playerHealth = 100;
	public int amphorasNeeded = 4;
	public static bool playsIsAlive = true;
	
	[HideInInspector] public int amphorasPicked = 0;
	
	private void Start() {
		color = bloodImage.GetComponent<Image>().color;
	}
	void Update() {
		if (playerHealth <= 0 ){
			Debug.Log("Player is Dead");
			Invoke("GameOver", 2f);
			gameObject.GetComponent<PlayerMovement>().enabled = false;
			gameObject.GetComponent<CharacterController>().enabled = false;
		}
		color.a = (100 - playerHealth) * 0.01f;
		bloodImage.GetComponent<Image>().color = color;
	}

	public void PlayerTakeDamage(int damage){
		cam.GetComponent<PlayerLook>().shake = true;
		damageAudioSuorce.pitch = Random.Range(0.8f, 1.1f);
		damageAudioSuorce.volume = Random.Range(0.4f, 0.5f);
		damageAudioSuorce.Play();
		
		playerHealth -= damage;
	}
	
	private void OnTriggerEnter(Collider other) {
		if(other.gameObject.CompareTag("Amphora")){
			amphorasPicked += 1;
			other.GetComponent<AudioSource>().Play();				//Play pickup sound
			other.GetComponent<CapsuleCollider>().enabled = false;	//Disable amphora's collider
			Destroy(other.gameObject, 0.5f);						//Destroy the amphora
			
			if(amphorasPicked >= amphorasNeeded){
				if(killTheMinotaur != null){
					killTheMinotaur.SetActive(true);
				}
				swordHolder.SetActive(true);
			}
		}
	}
	
	void GameOver(){
		SceneManager.LoadScene(1);
	}
}