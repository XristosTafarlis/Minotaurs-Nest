using UnityEngine;
using UnityEngine.UI;

public class PopUpScript : MonoBehaviour{
	Text text;

	void Start(){
		text = GetComponent<Text>();
	}

	void Update() {
		if(EnemyAI.minotaruIsDead == false){
			Invoke("Deactivate", 3f);
		}else if (EnemyAI.minotaruIsDead == true){
			text.text = "You have slayed the minotaur";
			text.color = new Color(0.36f, 0.88f, 0.42f);
			Invoke("Deactivate", 3f);
		}
	}
	void Deactivate(){
		gameObject.SetActive(false);
	}
}
