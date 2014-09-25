using UnityEngine;
using System.Collections;

public class GridMap : MonoBehaviour {

	public GameObject gameObjObstacle;
	public GameObject gameObjKey;
	public GameObject gameObjCabinet;

	private GameObject gameObjObstacleGenerated;

	//ArrayList map = new ArrayList();
	int[] map;// = new int[400]; 
	int width = 20;

	// Use this for initialization
	void Start () {
		map = new int[width * width]; 


		Vector3 obstaclePos = new Vector3 (0, 0, 0);

		gameObjObstacleGenerated = Instantiate (gameObjObstacle, obstaclePos, Quaternion.identity) as GameObject;
	}

	void Initial(){

	}

	// Update is called once per frame
	void Update () {
	
	}

	void GenerateEnvironment(){
		//Generate Obstacle

		GameObject objAsteroid = Instantiate (gameObjObstacle, ComputePosition(3,0,3), Quaternion.identity) as GameObject;
	
	}

	public void UpdateObjectsStatus(int indexX, int indexZ, int objectStatus){
		int index = indexZ * width + indexX;
		map [index] = objectStatus;
	}

	Vector3 ComputePosition(int x, int y, int z){
		Vector3 pos = new Vector3 (-2.5f + x * 5.0f, 0.0f, -2.5f + z * 5.0f);
		return pos;
	}
}
