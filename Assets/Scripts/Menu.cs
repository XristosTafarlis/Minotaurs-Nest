using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour{
	
	[SerializeField]
	private GameObject pauseMenuUI;
	
	[SerializeField]
	private GameObject settingsMenuUI;
	
	[SerializeField]
	private GameObject mazeSizeInputField;
	
	[SerializeField]
	private GameObject mazeRendererReference;
	
	[HideInInspector]
	public static int mazeS;
	
	public static bool gameIsPaused = false;
	private bool inSettings = false;
	
		
	void Update(){
		if(Input.GetKeyDown(KeyCode.Escape)){
			
			if(gameIsPaused == true){
				if(!inSettings){
					Resume();
				}else{
					Back();
				}
				
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
		inSettings = true;
		pauseMenuUI.SetActive(false);
		settingsMenuUI.SetActive(true);
	}
	
	public void Back(){
		inSettings = false;
		settingsMenuUI.SetActive(false);
		pauseMenuUI.SetActive(true);
	}
	
	public void Sensitivity(float sens){
		PlayerLook.mouseSensitivity = sens;
		//Debug.Log(sens);
	}
	
	public void SetSize(float sze){
		Debug.Log("Maze size set to : " + sze);
		mazeS = (int)sze;
		mazeRendererReference.GetComponent<MazeRenderer>().mazeSize = (int)sze;
	}
	
	public void Restart(){
		gameIsPaused = false;						//Unpause
		Time.timeScale = 1f;						//Unstuck
		Cursor.lockState = CursorLockMode.None;		//Free mouse
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
	
	public void quitGame(){
		Application.Quit();
	}
}