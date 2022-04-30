using UnityEngine;

public class PropsSpawner : MonoBehaviour{
	[SerializeField]
	private GameObject amphora;
	
	[SerializeField]
	private GameObject mazeRenderer;
	
	[SerializeField]
	private Transform player = null;
	
	[SerializeField]
	private Transform fence = null;
	
	[SerializeField]
	private GameObject minotaur = null;
	
	private int size;
	private float width;
	
	public int numberOfAmphoras = 12;
	
	void Start(){
		size = mazeRenderer.GetComponent<MazeRenderer>().mazeSize;
		width = mazeRenderer.GetComponent<MazeRenderer>().size;
		
		//Spawner
		Invoke("Spawn", 0.1f);
		
		//Instantiating minotaur
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
		
		//Moving fence to starting position
		fence.transform.position = new Vector3 ( -0f, 0f, -size*width/2 - width/2);
	}
}