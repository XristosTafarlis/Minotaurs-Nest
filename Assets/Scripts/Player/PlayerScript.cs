using UnityEngine;

public class PlayerScript : MonoBehaviour{
	
	public int amphorasNeeded = 4;
	public int amphorasPicked = 0;

	void Start(){
		
	}

	void Update(){
		
	}
	
	private void OnTriggerEnter(Collider other) {
		if(other.gameObject.CompareTag("Amphora")){
			Destroy(other.gameObject, 1f);
			amphorasNeeded -= 1;
			amphorasPicked += 1;
		}
		Debug.Log(amphorasPicked);
	}
}