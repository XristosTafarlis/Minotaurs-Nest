using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour{
	
	[SerializeField] GameObject pauseMenuUI;
	[SerializeField] GameObject settingsMenuUI;
	[SerializeField] GameObject mazeRendererReference;
	[SerializeField] GameObject bloodImage;
	[SerializeField] Slider[] sliders;
	
	[HideInInspector] public static int mazeS;
	
	public static bool gameIsPaused = false;
	private bool inSettings = false;
	
	void Awake() {
		SliderSet(sliders);
	}
	
	void Update(){
		if(Input.GetKeyDown(KeyCode.Escape)){
			if(gameIsPaused == true){
				if(!inSettings){
					bloodImage.SetActive(true);
					Resume();
				}else{
					Back();
				}
				
			}else{
				bloodImage.SetActive(false);
				Pause();
			}
		}
	}

	void SliderSet(Slider[] slider){
		foreach(Slider sld in sliders){
		float originalValue = sld.value;
		
		sld.value = sld.maxValue;
		
		sld.value = originalValue;
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
		bloodImage.SetActive(true);
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
		SceneManager.LoadScene(0);
	}
	
	public void quitGame(){
		Application.Quit();
	}
}