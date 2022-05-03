using UnityEngine.SceneManagement;
using UnityEngine;

public class GameOverScript : MonoBehaviour{
	
	void Start() {
		gameObject.GetComponent<AudioSource>().Play();
		Cursor.lockState = CursorLockMode.None;
	}
	
	public void Restart(){
		SceneManager.LoadScene(0);
	}
	
	public void Quit(){
		Application.Quit();
	}
}
