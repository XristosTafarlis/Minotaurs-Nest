using UnityEngine;
//using UnityEditor.AI;

public class MazeRenderer : MonoBehaviour{
	[Range(4, 50)] public int mazeSize;
	[Range(0.5f, 5f)] public float size = 3f;

	[SerializeField]
	private Transform wallPrefab = null;
	
	void Awake(){
		if(Menu.mazeS > 3)
			mazeSize = Menu.mazeS;
	}

	void Start(){		
		var maze = MazeGenerator.Generate(mazeSize, mazeSize);
		Draw(maze);
		
		//NavMeshBuilder.ClearAllNavMeshes();
		//NavMeshBuilder.BuildNavMesh();
	}

	#region Maze Making Algrothem

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
	
	#endregion
	
}