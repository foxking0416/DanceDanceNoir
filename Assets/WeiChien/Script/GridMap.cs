using UnityEngine;
using System.Collections;

public class GridMap : MonoBehaviour {

	public GameObject gameObjObstacle;
	public GameObject gameObjKey;
	public GameObject gameObjCabinet;
	public GameObject gameObjEnemy;
	public GameObject gameObjSuperEnergy;
	public GameObject gameObjRefrigerator;
	public GameObject gameObjCharacter;

	private GameObject gameObjObstacleGenerated;
	
	int[] map;// = new int[400]; 

	int width = 20;

	//enum objType{Enemy, Obstacle, BlueCabinet, YellowCabinet, RedCabinet, GreenCabinet, OrangeCabinet, BlueKey, YellowKey, RedKey, GreenKey, OrangeKey};

	// Use this for initialization
	void Start () {

		map = new int[width * width]; 

		UpdateObjectsStatus (7, 1, 1);
		UpdateObjectsStatus (7, 2, 1);
		UpdateObjectsStatus (7, 3, 1);
		UpdateObjectsStatus (7, 4, 1);
		UpdateObjectsStatus (7, 5, 1);
		UpdateObjectsStatus (7, 6, 1);
		UpdateObjectsStatus (7, 7, 1);
		UpdateObjectsStatus (6, 7, 1);
		UpdateObjectsStatus (14, 1, 1);
		UpdateObjectsStatus (14, 2, 1);
		UpdateObjectsStatus (14, 3, 1);
		UpdateObjectsStatus (14, 4, 1);
		UpdateObjectsStatus (14, 5, 1);
		UpdateObjectsStatus (8, 20, 1);
		UpdateObjectsStatus (8, 19, 1);
		UpdateObjectsStatus (8, 17, 1);
		UpdateObjectsStatus (9, 17, 1);
		UpdateObjectsStatus (9, 18, 1);
		UpdateObjectsStatus (9, 19, 1);
		UpdateObjectsStatus (9, 20, 1);
		UpdateObjectsStatus (15, 13, 1);
		UpdateObjectsStatus (16, 13, 1);
		UpdateObjectsStatus (17, 13, 1);
		UpdateObjectsStatus (18, 13, 1);
		UpdateObjectsStatus (19, 13, 1);
		UpdateObjectsStatus (20, 13, 1);
		
		UpdateObjectsStatus (9, 10, 2);
		
		UpdateObjectsStatus (11, 11, 11);//enemy
		UpdateObjectsStatus (12, 11, 11);//enemy

		UpdateObjectsStatus (18, 20, 21);//blue cabinet
		UpdateObjectsStatus ( 8, 18, 22);//yellow cabinet
		UpdateObjectsStatus (18, 12, 23);//red cabinet
		UpdateObjectsStatus (13,  3, 24);//green cabinet
		UpdateObjectsStatus ( 2,  1, 25);//orange cabinet
		
		
		UpdateObjectsStatus ( 5,  5, 31);//blue key
		UpdateObjectsStatus ( 3, 16, 32);//yellow key
		UpdateObjectsStatus (18,  4, 33);//red key
		UpdateObjectsStatus (16, 16, 34);//green key
		UpdateObjectsStatus (10, 12, 35);//orange key


		gameObject.transform.position = new Vector3 ((float)width / 2 * 5.0f, 0.0f, (float)width / 2 * 5.0f);
		gameObject.transform.localScale = new Vector3 ((float)width / 2, 1, (float)width / 2);

		GenerateEnvironment ();

		GameObject objCharacter = Instantiate (gameObjCharacter, ComputePosition(10, 0,  9), Quaternion.identity) as GameObject;

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
				GameObject objWall = Instantiate (gameObjObstacle, ComputePosition(i % width + 1, 0,  i / width + 1), Quaternion.identity) as GameObject;	
				objWall.transform.localScale = new Vector3(2.0f, 1, 8);
				objWall.transform.localRotation = Quaternion.Euler( new Vector3(0, 0, 0));
				break;
			case 2://Add Super Energy
				GameObject objSuperEnergy = Instantiate (gameObjSuperEnergy, ComputePosition(i % width + 1, 0,  i / width + 1), Quaternion.identity) as GameObject;	
				objSuperEnergy.tag = "SuperEnergy";
				SuperEnergyScript superEnergy = objSuperEnergy.GetComponent<SuperEnergyScript>();
				superEnergy.positionX = i % width + 1;
				superEnergy.positionZ = i / width + 1;
				break;
			case 11://Add Enemy
				GameObject objEnemy = Instantiate (gameObjEnemy, ComputePosition(i % width + 1, 0,  i / width + 1), Quaternion.identity) as GameObject;	
				objEnemy.tag = "Enemy";
				EnemyScript enemy = objEnemy.GetComponent<EnemyScript>();
				enemy.positionX = i % width + 1;
				enemy.positionZ = i / width + 1;
				break;

			case 21://Add Blue Cabinet
				/*GameObject objCabinetBlue = Instantiate (gameObjCabinet, ComputePosition(i % width + 1, 0,  i / width + 1), Quaternion.identity) as GameObject;
				objCabinetBlue.tag = "BlueCabinet";
				CabinetScript cabinetBlue = objCabinetBlue.GetComponent<CabinetScript>();
				cabinetBlue.positionX = i % width + 1;
				cabinetBlue.positionZ = i / width + 1;
				cabinetBlue.SetColor(21);*/
				GameObject objRefrigerator = Instantiate (gameObjRefrigerator, ComputePosition(i % width + 1, 0,  i / width + 1), Quaternion.identity) as GameObject;
				objRefrigerator.tag = "BlueCabinet";
				objRefrigerator.transform.localScale = new Vector3(5, 5, 5);
				objRefrigerator.transform.localRotation = Quaternion.Euler( new Vector3(0, 180, 0));
				CabinetScript cabinetBlue = objRefrigerator.GetComponent<CabinetScript>();
				cabinetBlue.positionX = i % width + 1;
				cabinetBlue.positionZ = i / width + 1;
				cabinetBlue.SetColor(21);
				break;
			case 22://Add Yellow Cabinet
				GameObject objCabinetYellow = Instantiate (gameObjCabinet, ComputePosition(i % width + 1, 0,  i / width + 1), Quaternion.identity) as GameObject;
				objCabinetYellow.tag = "YellowCabinet";
				CabinetScript cabinetYellow = objCabinetYellow.GetComponent<CabinetScript>();
				cabinetYellow.positionX = i % width + 1;
				cabinetYellow.positionZ = i / width + 1;
				cabinetYellow.SetColor(22);
				break;
			case 23://Add Red Cabinet
				GameObject objCabinetRed = Instantiate (gameObjCabinet, ComputePosition(i % width + 1, 0,  i / width + 1), Quaternion.identity) as GameObject;
				objCabinetRed.tag = "RedCabinet";
				CabinetScript cabinetRed = objCabinetRed.GetComponent<CabinetScript>();
				cabinetRed.positionX = i % width + 1;
				cabinetRed.positionZ = i / width + 1;
				cabinetRed.SetColor(23);
				break;
			case 24://Add Green Cabinet
				GameObject objCabinetGreen = Instantiate (gameObjCabinet, ComputePosition(i % width + 1, 0,  i / width + 1), Quaternion.identity) as GameObject;
				objCabinetGreen.tag = "GreenCabinet";
				CabinetScript cabinetGreen = objCabinetGreen.GetComponent<CabinetScript>();
				cabinetGreen.positionX = i % width + 1;
				cabinetGreen.positionZ = i / width + 1;
				cabinetGreen.SetColor(24);
				break;
			case 25://Add Orange Cabinet
				GameObject objCabinetOrange = Instantiate (gameObjCabinet, ComputePosition(i % width + 1, 0,  i / width + 1), Quaternion.identity) as GameObject;
				objCabinetOrange.tag = "OrangeCabinet";
				CabinetScript cabinetOrange = objCabinetOrange.GetComponent<CabinetScript>();
				cabinetOrange.positionX = i % width + 1;
				cabinetOrange.positionZ = i / width + 1;
				cabinetOrange.SetColor(25);
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
				objKeyYellow.tag = "YellowKey";
				KeyScript keyYellow = objKeyYellow.GetComponent<KeyScript>();
				keyYellow.positionX = i % width + 1;
				keyYellow.positionZ = i / width + 1;
				keyYellow.SetColor(32);
				break;
			case 33: //Add Red Key
				GameObject objKeyRed = Instantiate (gameObjKey, ComputePosition(i % width + 1, 0, i / width + 1), Quaternion.identity) as GameObject;
				objKeyRed.tag = "RedKey";
				KeyScript keyRed = objKeyRed.GetComponent<KeyScript>();
				keyRed.positionX = i % width + 1;
				keyRed.positionZ = i / width + 1;
				keyRed.SetColor(33);
				break;
			case 34://Add Green Key
				GameObject objKeyGreen = Instantiate (gameObjKey, ComputePosition(i % width + 1, 0, i / width + 1), Quaternion.identity) as GameObject;
				objKeyGreen.tag = "GreenKey";
				KeyScript keyGreen = objKeyGreen.GetComponent<KeyScript>();
				keyGreen.positionX = i % width + 1;
				keyGreen.positionZ = i / width + 1;
				keyGreen.SetColor(34);
				break;
			case 35://Add Orange Key
				GameObject objKeyOrange = Instantiate (gameObjKey, ComputePosition(i % width + 1, 0, i / width + 1), Quaternion.identity) as GameObject;
				objKeyOrange.tag = "OrangeKey";
				KeyScript keyOrange = objKeyOrange.GetComponent<KeyScript>();
				keyOrange.positionX = i % width + 1;
				keyOrange.positionZ = i / width + 1;
				keyOrange.SetColor(35);
				break;
			}		
		}
	}

	void GenerateWall(){
		//Vertical Wall
		for (int i = 0; i < width + 2; ++i) {
			GameObject objWall = Instantiate (gameObjObstacle, ComputePosition(0, 0,  i), Quaternion.identity) as GameObject;
			objWall.transform.localScale = new Vector3(2.0f, 5, 8);
			objWall.transform.localRotation = Quaternion.Euler( new Vector3(0, 90, 0));
			GameObject objWall2 = Instantiate (gameObjObstacle, ComputePosition(width + 1, 0, i), Quaternion.identity) as GameObject;	
			objWall2.transform.localScale = new Vector3(2.0f, 5, 8);
			objWall2.transform.localRotation = Quaternion.Euler( new Vector3(0, 90, 0));
		}

		//Horizontal Wall
		for (int i = 1; i < width + 1; ++i) {
			GameObject objWall = Instantiate (gameObjObstacle, ComputePosition(i, 0,  0), Quaternion.identity) as GameObject;	
			objWall.transform.localScale = new Vector3(2.0f, 5, 8);
			objWall.transform.localRotation = Quaternion.Euler( new Vector3(0, 0, 0));
			GameObject objWall2 = Instantiate (gameObjObstacle, ComputePosition(i, 0, width + 1), Quaternion.identity) as GameObject;	
			objWall2.transform.localScale = new Vector3(2.0f, 5, 8);
			objWall2.transform.localRotation = Quaternion.Euler( new Vector3(0, 0, 0));
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
