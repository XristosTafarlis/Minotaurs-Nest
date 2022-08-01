using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour{
	[Header("Refferences")]
	[SerializeField] GameObject swordHolder;
	[SerializeField] AudioSource damageAudioSuorce;
	[SerializeField] GameObject cam;
	[SerializeField] GameObject popUpText;
	[SerializeField] GameObject bloodImage;
	[SerializeField] Text healthText;
	[SerializeField] Image healthBar;
	Color color;

	[Header("Variables")]
	public int playerHealth = 100;
	private float maxHealth;
	public int amphorasNeeded = 4;
	public static bool playsIsAlive = true;

	[HideInInspector] public int amphorasPicked = 0;

	private void Start() {
		maxHealth = playerHealth * 1f;
		color = bloodImage.GetComponent<Image>().color;
	}
	void Update() {
		isDead();
		blood();
		health();
	}

	void health(){
		healthBar.fillAmount = (playerHealth/maxHealth);
		if(playerHealth >= 0){
			healthText.text = "Health " + playerHealth;
		}else{
			healthText.text = "You died";
		}
	}

	void blood(){
		color.a = (maxHealth - playerHealth) * 0.01f;
		bloodImage.GetComponent<Image>().color = color;
	}

	void isDead(){
		if (playerHealth <= 0 ){
			Invoke("GameOver", 2f);
			Cursor.lockState = CursorLockMode.Locked;
			GetComponent<PlayerMovement>().enabled = false;
			GetComponent<CharacterController>().enabled = false;
			swordHolder.GetComponent<SwordController>().enabled = false;
		}
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

			if(amphorasPicked == amphorasNeeded){
				popUpText.SetActive(true);
				swordHolder.SetActive(true);
			}
		}
	}

	void GameOver(){
		SceneManager.LoadScene(1);
	}
}