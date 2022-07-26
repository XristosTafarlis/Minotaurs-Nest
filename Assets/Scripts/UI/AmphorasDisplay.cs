using UnityEngine.UI;
using UnityEngine;

public class AmphorasDisplay : MonoBehaviour{
	
	[Header("Refferences")]
	[SerializeField] GameObject player;
	[SerializeField] GameObject spawner;
	[SerializeField] Text amphorasTxt;
	[SerializeField] Text maxAmphorasTxt;
	
	int amphoras;
	int maxAmphoras;
	
	void Start(){
		maxAmphoras = player.GetComponent<PlayerScript>().amphorasNeeded;
		maxAmphorasTxt.text = "Amphoras in maze : " + spawner.GetComponent<PropsSpawner>().numberOfAmphoras;
	}
	
	void Update(){
		if(amphoras < maxAmphoras){
			amphoras = player.GetComponent<PlayerScript>().amphorasPicked;
			amphorasTxt.text = "Amphoras needed : " + amphoras + " of " + maxAmphoras;
		}else{
			amphorasTxt.text = "You can now kill the minotaur";
		}
	}
}
