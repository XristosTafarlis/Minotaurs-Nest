using UnityEngine;


public class PropsSpawner : MonoBehaviour{
	
	public GameObject amphora;
	public GameObject mazeRenderer;
	private int size;
	private float width;
	
	public int numberOfAmphoras = 12;
	
    void Start(){
		size = mazeRenderer.GetComponent<MazeRenderer>().mazeSize;
		width = mazeRenderer.GetComponent<MazeRenderer>().size;
		
		for(int i = 0; i < numberOfAmphoras; i++){
			Instantiate(amphora, new Vector3( Random.Range( -(size/2), size/2 ) * width, -1f, Random.Range( -(size/2) , size/2 ) * width), transform.rotation);
		}
    }

    // Update is called once per frame
    void Update(){
		
    }
}