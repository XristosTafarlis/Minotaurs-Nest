using UnityEngine;

public class PropsSpawner : MonoBehaviour{

	[Header("Refferences")]
	[SerializeField] GameObject amphora;
	[SerializeField] GameObject mazeRenderer;
	[SerializeField] GameObject groundPlane;
	[SerializeField] GameObject player;
	[SerializeField] GameObject guard;
	[SerializeField] GameObject minotaur;

	[Space(20)]
	public int numberOfAmphoras = 12;

	int size;
	float width;

	void Awake() {
		size = mazeRenderer.GetComponent<MazeRenderer>().size;
		width = mazeRenderer.GetComponent<MazeRenderer>().width;
	}

	void Start(){
		SpawnPlayer();
		SpawnAmphoras();
		RescaleGroundPlane();
		
		Invoke("MinotaurActivate", 1);
	}

	void MinotaurActivate(){
		minotaur.SetActive(true);
	}

	void RescaleGroundPlane(){
		//Rescale ground plane for better performace
		if(size % 2 == 0){
			groundPlane.transform.position = new Vector3( -width/2, -1.6f, -width/2);
		}else{
			groundPlane.transform.position = new Vector3( 0f, -1.6f, 0f);
		}
		groundPlane.transform.localScale = new Vector3(size * width + 6f, 1f, size * width + 6f);
	}

	void SpawnPlayer(){
		//Player
		if(size % 2 == 0){
			player.transform.position = new Vector3 ( 0f, -0.4f, -size*width/2 - width/2 - 2.346f);
		}else{
			player.transform.position = new Vector3 ( 0f, -0.4f, -size*width/2 - width/2 - 1.2f);
		}
		player.SetActive(true);

		//Guard
		if(size % 2 == 0){
			guard.transform.position = new Vector3 ( -0f, 0f, -size*width/2 - width/2);
		}else{
			guard.transform.position = new Vector3 ( -0f, 0f, -size*width/2 - width/2 + 1.5f);
		}
	}

	void SpawnAmphoras(){
		for(int i = 0; i < numberOfAmphoras; i++){
			Instantiate(amphora, new Vector3( Random.Range( -(size/2), size/2 ) * width, -1f, Random.Range( -(size/2) , size/2 ) * width), transform.rotation);
		}
	}
}