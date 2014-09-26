using UnityEngine;
using System.Collections;

public class GridMap : MonoBehaviour {

	public GameObject gameObjObstacle;
	public GameObject gameObjKey;
	public GameObject gameObjCabinet;
	public GameObject gameObjEnemy;

	private GameObject gameObjObstacleGenerated;

	//ArrayList map = new ArrayList();
	int[] map;// = new int[400]; 
	GameObject[] objectMap;
	int width = 20;

	// Use this for initialization
	void Start () {
		map = new int[width * width]; 
		objectMap = new GameObject[width * width]; 
		UpdateObjectsStatus (3, 3, 1);

		UpdateObjectsStatus (6, 6, 11);
		UpdateObjectsStatus (4, 4, 21);
		UpdateObjectsStatus (5, 5, 31);


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
			case 1://Add obstacle
				Instantiate (gameObjObstacle, ComputePosition(i % width + 1, 0,  i / width + 1), Quaternion.identity);	
				break;
			case 11://Add Enemy
				Instantiate (gameObjEnemy, ComputePosition(i % width + 1, 0,  i / width + 1), Quaternion.identity);	
				break;

			case 21://Add Blue Cabinet
				GameObject objCabinetBlue = Instantiate (gameObjCabinet, ComputePosition(i % width + 1, 0,  i / width + 1), Quaternion.identity) as GameObject;
				objCabinetBlue.tag = "BlueCabinet";
				CabinetScript cabinetBlue = objCabinetBlue.GetComponent<CabinetScript>();
				cabinetBlue.positionX = i % width + 1;
				cabinetBlue.positionZ = i / width + 1;
				break;
			case 22://Add Yellow Cabinet
				GameObject objCabinetYellow = Instantiate (gameObjCabinet, ComputePosition(i % width + 1, 0,  i / width + 1), Quaternion.identity) as GameObject;
				CabinetScript cabinetYellow = objCabinetYellow.GetComponent<CabinetScript>();
				cabinetYellow.positionX = i % width + 1;
				cabinetYellow.positionZ = i / width + 1;
				break;
			case 23://Add Red Cabinet
				GameObject objCabinetRed = Instantiate (gameObjCabinet, ComputePosition(i % width + 1, 0,  i / width + 1), Quaternion.identity) as GameObject;
				CabinetScript cabinetRed = objCabinetRed.GetComponent<CabinetScript>();
				cabinetRed.positionX = i % width + 1;
				cabinetRed.positionZ = i / width + 1;
				break;
			case 24://Add Green Cabinet
				GameObject objCabinetGreen = Instantiate (gameObjCabinet, ComputePosition(i % width + 1, 0,  i / width + 1), Quaternion.identity) as GameObject;
				CabinetScript cabinetGreen = objCabinetGreen.GetComponent<CabinetScript>();
				cabinetGreen.positionX = i % width + 1;
				cabinetGreen.positionZ = i / width + 1;
				break;
			case 25://Add Orange Cabinet
				GameObject objCabinetOrange = Instantiate (gameObjCabinet, ComputePosition(i % width + 1, 0,  i / width + 1), Quaternion.identity) as GameObject;
				CabinetScript cabinetOrange = objCabinetOrange.GetComponent<CabinetScript>();
				cabinetOrange.positionX = i % width + 1;
				cabinetOrange.positionZ = i / width + 1;
				break;

			case 31: //Add Blue key
				GameObject objKeyBlue = Instantiate (gameObjKey, ComputePosition(i % width + 1, 0, i / width + 1), Quaternion.identity) as GameObject;
				objKeyBlue.tag = "BlueKey";
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
			case 34://Add Green Key
				GameObject objKeyGreen = Instantiate (gameObjKey, ComputePosition(i % width + 1, 0, i / width + 1), Quaternion.identity) as GameObject;
				KeyScript keyGreen = objKeyGreen.GetComponent<KeyScript>();
				keyGreen.positionX = i % width + 1;
				keyGreen.positionZ = i / width + 1;
				keyGreen.SetColor(34);
				break;
			case 35://Add Orange Key
				GameObject objKeyOrange = Instantiate (gameObjKey, ComputePosition(i % width + 1, 0, i / width + 1), Quaternion.identity) as GameObject;
				KeyScript keyOrange = objKeyOrange.GetComponent<KeyScript>();
				keyOrange.positionX = i % width + 1;
				keyOrange.positionZ = i / width + 1;
				keyOrange.SetColor(35);
				break;
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
