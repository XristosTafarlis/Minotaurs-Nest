using UnityEngine.UI;
using UnityEngine;

public class MainUI : MonoBehaviour{
	
	[SerializeField] GameObject player;
	
	[SerializeField] private Text amphorasTxt;
	private int amphoras, maxAmphoras;
	
	void Start(){
		maxAmphoras = player.GetComponent<PlayerScript>().amphorasNeeded;
	}
	
	void Update(){
		if(amphoras < maxAmphoras){
			amphoras = player.GetComponent<PlayerScript>().amphorasPicked;
			amphorasTxt.text = "Amphoras : " + amphoras + " of " + maxAmphoras;
		}else{
			amphorasTxt.text = "Amphoras : " + maxAmphoras + " of " + maxAmphoras + ", you can now kill the minotaur";
		}
	}
}
