using UnityEngine;

public class MazeRenderer : MonoBehaviour{
	[Range(4, 50)] public int size;
	[Range(0.5f, 5f)] public float width = 3f;

	[SerializeField]
	private Transform wallPrefab = null;

	void Awake(){
		if(Menu.mazeS > 3)
			size = Menu.mazeS;
	}

	void Start(){
		var maze = MazeGenerator.Generate(size, size);
		Draw(maze);
	}

	#region Maze Making Algrothem

	private void Draw(WallState[,] maze){
		for (int i = 0; i < size; ++i){
			for (int j = 0; j < size; ++j){
				var cell = maze[i, j];
				var position = new Vector3((-size / 2 + i) * width, 0, (-size / 2 + j) * width);

				if (cell.HasFlag(WallState.UP)){
					var topWall = Instantiate(wallPrefab, transform) as Transform;
					topWall.position = position + new Vector3(0, 0, width / 2);
					topWall.localScale = new Vector3(width, topWall.localScale.y, topWall.localScale.z);
				}

				if (cell.HasFlag(WallState.LEFT)){
					var leftWall = Instantiate(wallPrefab, transform) as Transform;
					leftWall.position = position + new Vector3(-width / 2, 0, 0);
					leftWall.localScale = new Vector3(width, leftWall.localScale.y, leftWall.localScale.z);
					leftWall.eulerAngles = new Vector3(0, 90, 0);
				}

				if (i == size - 1){
					if (cell.HasFlag(WallState.RIGHT)){
						var rightWall = Instantiate(wallPrefab, transform) as Transform;
						rightWall.position = position + new Vector3(+width / 2, 0, 0);
						rightWall.localScale = new Vector3(width, rightWall.localScale.y, rightWall.localScale.z);
						rightWall.eulerAngles = new Vector3(0, 90, 0);
					}
				}

				if (j == 0){
					if (cell.HasFlag(WallState.DOWN)){
						var bottomWall = Instantiate(wallPrefab, transform) as Transform;
						bottomWall.position = position + new Vector3(0, 0, -width / 2);
						bottomWall.localScale = new Vector3(width, bottomWall.localScale.y, bottomWall.localScale.z);
					}
				}
			}
		}
	}

	#endregion

}