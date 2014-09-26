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
		UpdateObjectsStatus (3, 3, 1);

		UpdateObjectsStatus (4, 4, 21);

		gameObject.transform.position = new Vector3 (10 * 5.0f, 0.0f, 10 * 5.0f);
		gameObject.transform.localScale = new Vector3 (10, 1, 10);

		GenerateEnvironment ();


	}

	void Initial(){

	}

	// Update is called once per frame
	void Update () {
	
	}

	void GenerateEnvironment(){
		//Generate Obstacle

		GenerateWall ();

		for (int i = 0; i < width * width; ++i) {
			switch(map[i]){
			case 0:
				break;
			case 1:
				Instantiate (gameObjObstacle, ComputePosition(i % width + 1, 0,  i / width + 1), Quaternion.identity);	
				break;
			case 21:
				Instantiate (gameObjCabinet, ComputePosition(i % width + 1, 0,  i / width + 1), Quaternion.identity);
				break;
			case 31: //Add Blue key
				GameObject objKeyBlue = Instantiate (gameObjKey, ComputePosition(i % width + 1, 0, i / width + 1), Quaternion.identity) as GameObject;
				KeyScript keyBlue = objKeyBlue.GetComponent<KeyScript>();
				keyBlue.positionX = i % width + 1;
				keyBlue.positionZ = i / width + 1;
				keyBlue.SetColor(31);
				break;
			case 32: //Add Yellow key
				GameObject objKeyYellow = Instantiate (gameObjKey, ComputePosition(i % width + 1, 0, i / width + 1), Quaternion.identity) as GameObject;
				KeyScript keyYellow = objKeyYellow.GetComponent<KeyScript>();
				keyYellow.positionX = i % width + 1;
				keyYellow.positionZ = i / width + 1;
				keyYellow.SetColor(32);
				break;
			case 33: //Add Red Key
				GameObject objKeyRed = Instantiate (gameObjKey, ComputePosition(i % width + 1, 0, i / width + 1), Quaternion.identity) as GameObject;
				KeyScript keyRed = objKeyRed.GetComponent<KeyScript>();
				keyRed.positionX = i % width + 1;
				keyRed.positionZ = i / width + 1;
				keyRed.SetColor(33);
				break;
			case 34:
				GameObject objKeyGreen = Instantiate (gameObjKey, ComputePosition(i % width + 1, 0, i / width + 1), Quaternion.identity) as GameObject;
				KeyScript keyGreen = objKeyGreen.GetComponent<KeyScript>();
				keyGreen.positionX = i % width + 1;
				keyGreen.positionZ = i / width + 1;
				keyGreen.SetColor(34);
				break;
			/*case 35:
				GameObject obj = Instantiate (gameObjKey, ComputePosition(i % width + 1, 0, i / width + 1), Quaternion.identity) as GameObject;
				KeyScript key = obj.GetComponent<KeyScript>();
				key.positionX = i % width + 1;
				key.positionZ = i / width + 1;
				key.SetColor(35);
				break;*/
			}		
		}
	}

	void GenerateWall(){
		for (int i = 0; i < width + 2; ++i) {
			Instantiate (gameObjObstacle, ComputePosition(0, 0,  i), Quaternion.identity);	
			Instantiate (gameObjObstacle, ComputePosition(width + 1, 0, i), Quaternion.identity);	
		}

		for (int i = 1; i < width + 1; ++i) {
			Instantiate (gameObjObstacle, ComputePosition(i, 0,  0), Quaternion.identity);	
			Instantiate (gameObjObstacle, ComputePosition(i, 0, width + 1), Quaternion.identity);	
		}
	}

	public int GetObjectOnMap(int indexX, int indexZ){
		int index = (indexZ - 1) * width + (indexX - 1);
		return map [index];
	}

	public void UpdateObjectsStatus(int indexX, int indexZ, int objectStatus){
		int index = (indexZ - 1) * width + (indexX - 1);
		map [index] = objectStatus;
	}

	Vector3 ComputePosition(int x, int y, int z){
		Vector3 pos = new Vector3 (-2.5f + x * 5.0f, 0.0f, -2.5f + z * 5.0f);
		return pos;
	}

	public int GetMapSize(){
		return width;
	}
}
