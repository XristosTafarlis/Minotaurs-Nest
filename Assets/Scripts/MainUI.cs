using UnityEngine.UI;
using UnityEngine;

public class MainUI : MonoBehaviour{
	
	[SerializeField] GameObject player;
	
	public Text amphorasTxt;
	private int amphoras, maxAmphoras;
	
	void Start(){
		maxAmphoras = player.GetComponent<PlayerScript>().amphorasNeeded;
	}
	
	void FixedUpdate(){
		amphoras = player.GetComponent<PlayerScript>().amphorasPicked;
		amphorasTxt.text = "Amphoras : " + amphoras + " of " + maxAmphoras;
	}
}
