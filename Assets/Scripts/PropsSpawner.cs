//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;


public class PropsSpawner : MonoBehaviour{
	
	public GameObject point;
	public GameObject mazeRenderer;
	private int size;
	private float width;
	
	public int numberOfAmphores = 12;
	
    void Start(){
		size = mazeRenderer.GetComponent<MazeRenderer>().mazeSize;
		width = mazeRenderer.GetComponent<MazeRenderer>().size;
		
		for(int i = 0; i < numberOfAmphores; i++){
			Instantiate(point, new Vector3( Random.Range( -(size/2), size/2 ) * width, 0f, Random.Range( -(size/2) , size/2 ) * width), transform.rotation);
		}
    }

    // Update is called once per frame
    void Update(){
		
    }
}