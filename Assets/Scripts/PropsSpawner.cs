using UnityEngine;

public class PropsSpawner : MonoBehaviour{
	
	[Header("Refferences")]
	[SerializeField] GameObject amphora;
	[SerializeField] GameObject mazeRenderer;
	[SerializeField] Transform groundPlane;
	[SerializeField] Transform player = null;
	[SerializeField] Transform guard = null;
	[SerializeField] GameObject minotaur = null;
	
	[Space(20)]
	public int numberOfAmphoras = 12;
	
	int size;
	float width;
	
	void Awake() {
		
		//Spawner
		Invoke("Spawn", 0.1f);
		
		//Spawn minotaur
		Invoke("minotaurActivate", 1);
	}
	
	void Start(){
		size = mazeRenderer.GetComponent<MazeRenderer>().mazeSize;
		width = mazeRenderer.GetComponent<MazeRenderer>().size;
		
		for(int i = 0; i < numberOfAmphoras; i++){
			Instantiate(amphora, new Vector3( Random.Range( -(size/2), size/2 ) * width, -1f, Random.Range( -(size/2) , size/2 ) * width), transform.rotation);
		}
		
		RescaleGroundPlane();
	}
	
	void minotaurActivate(){
		minotaur.SetActive(true);
	}
	
	void RescaleGroundPlane(){
		//Rescale ground plane for better performace
		if(size % 2 == 0)
			groundPlane.position = new Vector3( -width/2, -1.6f, -width/2);
		else
			groundPlane.position = new Vector3( 0f, -1.6f, 0f);
			
		groundPlane.localScale = new Vector3(size * width, 1f, size * width);
	}
	
	void Spawn(){
		//Moving player to starting position
		player.transform.position = new Vector3 ( 0f, -0.4f, -size*width/2 - width/2 - 1);
		
		//Moving guard to starting position
		if(size % 2 == 0)
			guard.transform.position = new Vector3 ( -0f, 0f, -size*width/2 - width/2);
		else
			guard.transform.position = new Vector3 ( -0f, 0f, -size*width/2 - width/2 + 1.5f);
	}
}