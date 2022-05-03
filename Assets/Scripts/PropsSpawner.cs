using UnityEngine;

public class PropsSpawner : MonoBehaviour{
	
	[SerializeField] GameObject amphora;
	[SerializeField] GameObject mazeRenderer;
	[SerializeField] Transform player = null;
	[SerializeField] Transform guard = null;
	[SerializeField] GameObject minotaur = null;
	
	int size;
	float width;
	
	int numberOfAmphoras = 12;
	
	void Start(){
		size = mazeRenderer.GetComponent<MazeRenderer>().mazeSize;
		width = mazeRenderer.GetComponent<MazeRenderer>().size;
		
		//Spawner
		Invoke("Spawn", 0.1f);
		
		//Spawn minotaur
		Invoke("minotaurActivate", 1);
		
		for(int i = 0; i < numberOfAmphoras; i++){
			Instantiate(amphora, new Vector3( Random.Range( -(size/2), size/2 ) * width, -1f, Random.Range( -(size/2) , size/2 ) * width), transform.rotation);
		}
    }
	
	void minotaurActivate(){
		minotaur.SetActive(true);
	}
	
	void Spawn(){
		//Moving player to starting position
		player.transform.position = new Vector3 ( 0f, -0.4f, -size*width/2 - width);
		
		//Moving guard to starting position
		if(size % 2 == 0)
			guard.transform.position = new Vector3 ( -0f, 0f, -size*width/2 - width/2);
		else
			guard.transform.position = new Vector3 ( -0f, 0f, -size*width/2 - width/2 + 1.5f);
	}
}