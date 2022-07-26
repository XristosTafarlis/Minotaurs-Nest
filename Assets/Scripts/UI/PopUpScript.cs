using UnityEngine;

public class PopUpScript : MonoBehaviour{
	Color color;
	
	void Start(){
		Invoke("Destroy", 3f);
	}
	
	void Destroy(){
		Destroy(gameObject);
	}
}
