using UnityEngine;

public class PlayerScript : MonoBehaviour{
	
	public int amphorasNeeded = 4;
	[HideInInspector] public int amphorasPicked = 0;

	void Update(){
		
	}
	
	private void OnTriggerEnter(Collider other) {
		if(other.gameObject.CompareTag("Amphora")){
			other.GetComponent<CapsuleCollider>().enabled = false;
			Destroy(other.gameObject, 1f);
			amphorasNeeded -= 1;
			amphorasPicked += 1;
			Debug.Log(amphorasPicked);
		}
	}
}