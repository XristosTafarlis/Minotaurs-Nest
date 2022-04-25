using UnityEngine;

public class PauseMenu : MonoBehaviour{
	
	[SerializeField]
	private GameObject pauseMenuUI;
	
	public static bool gameIsPaused = false;
	
	void Update(){
		if(Input.GetKeyDown(KeyCode.Escape)){
			
			if(gameIsPaused == true){
				Resume();
			}else{
				Pause();
			}
		}
	}

	void Pause(){
		Cursor.lockState = CursorLockMode.None;		//Free mouse
		pauseMenuUI.SetActive(true);
		Time.timeScale = 0f;
		gameIsPaused = true;        
	}
	
	public void Resume(){
		Cursor.lockState = CursorLockMode.Locked;	//Locked mouse
		pauseMenuUI.SetActive(false);
		Time.timeScale = 1f;
		gameIsPaused = false;
	}
	
	public void settings(){
		
	}
	
	public void quitGame(){
		Application.Quit();
	}
}
