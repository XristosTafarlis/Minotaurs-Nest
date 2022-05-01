using UnityEngine;

public class PlayerScript : MonoBehaviour{
	
	[SerializeField] GameObject swordHolder;
	public int amphorasPicked = 0;
	public int amphorasNeeded = 4;
	
	private void OnTriggerEnter(Collider other) {
		
		if(other.gameObject.CompareTag("Amphora")){
			amphorasPicked += 1;
			other.GetComponent<CapsuleCollider>().enabled = false;
			Destroy(other.gameObject, 1f);
			
			if(amphorasPicked >= amphorasNeeded){
				//GetComponentInChildren<SwordController>().enabled = true;
				swordHolder.SetActive(true);
				Debug.Log("Sword up");
			}
		}
	}
}