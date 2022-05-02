using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerScript : MonoBehaviour{
	
	[SerializeField] GameObject swordHolder;
	[HideInInspector] public int amphorasPicked = 0;
	public int amphorasNeeded = 4;
	
	[SerializeField] int playerHealth = 100;

	void Update() {
		if (playerHealth <= 0 ){
			Debug.Log("Player is Dead");
			Invoke("SceneRestart", 2f);
		}
	}

	public void PlayerTakeDamage(int damage){
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