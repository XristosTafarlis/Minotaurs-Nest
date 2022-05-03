using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerScript : MonoBehaviour{
	[Header("Refferences")]
	[SerializeField] GameObject swordHolder;
	[SerializeField] AudioSource damageAudioSouorce;
	
	[Header("Variables")]
	[SerializeField] int playerHealth = 100;
	public int amphorasNeeded = 4;
	
	[HideInInspector] public int amphorasPicked = 0;
	
	void Start(){
		
	}
	
	void Update() {
		if (playerHealth <= 0 ){
			Debug.Log("Player is Dead");
			Invoke("SceneRestart", 2f);
			gameObject.GetComponent<PlayerMovement>().enabled = false;
		}
	}

	public void PlayerTakeDamage(int damage){
		
		damageAudioSouorce.pitch = Random.Range(0.8f, 1.1f);
		damageAudioSouorce.volume = Random.Range(0.4f, 0.5f);
		damageAudioSouorce.Play();
		
		playerHealth -= damage;
	}
	
	private void OnTriggerEnter(Collider other) {
		if(other.gameObject.CompareTag("Amphora")){
			amphorasPicked += 1;
			other.GetComponent<CapsuleCollider>().enabled = false;
			Destroy(other.gameObject, 1f);
			
			if(amphorasPicked >= amphorasNeeded){
				swordHolder.SetActive(true);
				Debug.Log("Sword up");
			}
		}
	}
	
	void SceneRestart(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}