using UnityEngine;
//using UnityEditor.AI;

public class MazeRenderer : MonoBehaviour{

    [Range(4, 50)]
    public int mazeSize = 10;

    public float size = 1f;

    [SerializeField]
    private Transform wallPrefab = null;

    [SerializeField]
    private Transform player = null;
	
	[SerializeField]
    private Transform fence = null;
	
	[SerializeField]
    private Transform minotaurPrefab = null;


    void Start(){
		
		//Moving player to starting position
		player.transform.position = new Vector3 ( 0f, -0.4f, -mazeSize*size/2 - size);
		
		//Moving fence to starting position
		//Instantiate(fencePrefab, new Vector3( -1f, 0, -( mazeSize * size / 2 + (size / 2) ) ), transform.rotation);
		fence.transform.position = new Vector3 ( -0f, 0f, -mazeSize*size/2 - size/2);
		
		//Instantiating minotaur
		Invoke("minotaurInstantiate", 1);
		
        var maze = MazeGenerator.Generate(mazeSize, mazeSize);
        Draw(maze);
		
		//NavMeshBuilder.ClearAllNavMeshes();
		//NavMeshBuilder.BuildNavMesh();
		
    }
	
	void minotaurInstantiate(){
		Instantiate(minotaurPrefab, new Vector3( 0, 0, 0), transform.rotation);
	}

    private void Draw(WallState[,] maze){
        for (int i = 0; i < mazeSize; ++i){
            for (int j = 0; j < mazeSize; ++j){
                var cell = maze[i, j];
				var position = new Vector3((-mazeSize / 2 + i) * size, 0, (-mazeSize / 2 + j) * size);

                if (cell.HasFlag(WallState.UP)){
                    var topWall = Instantiate(wallPrefab, transform) as Transform;
                    topWall.position = position + new Vector3(0, 0, size / 2);
                    topWall.localScale = new Vector3(size, topWall.localScale.y, topWall.localScale.z);
                }

                if (cell.HasFlag(WallState.LEFT)){
                    var leftWall = Instantiate(wallPrefab, transform) as Transform;
                    leftWall.position = position + new Vector3(-size / 2, 0, 0);
                    leftWall.localScale = new Vector3(size, leftWall.localScale.y, leftWall.localScale.z);
                    leftWall.eulerAngles = new Vector3(0, 90, 0);
                }

                if (i == mazeSize - 1){
                    if (cell.HasFlag(WallState.RIGHT)){
                        var rightWall = Instantiate(wallPrefab, transform) as Transform;
                        rightWall.position = position + new Vector3(+size / 2, 0, 0);
                        rightWall.localScale = new Vector3(size, rightWall.localScale.y, rightWall.localScale.z);
                        rightWall.eulerAngles = new Vector3(0, 90, 0);
                    }
                }

                if (j == 0){
                    if (cell.HasFlag(WallState.DOWN)){
                        var bottomWall = Instantiate(wallPrefab, transform) as Transform;
                        bottomWall.position = position + new Vector3(0, 0, -size / 2);
                        bottomWall.localScale = new Vector3(size, bottomWall.localScale.y, bottomWall.localScale.z);
                    }
                }
            }
        }
    }
}