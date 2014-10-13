using UnityEngine;
using System.Collections;

public class GridMap : MonoBehaviour {

	public GameObject gameObjObstacleWall;//type 1
	public GameObject gameObjObstacleChair;//type 2
	public GameObject gameObjObstacleChair2;//type 6
	public GameObject gameObjObstacleTable;//type 3
	public GameObject gameObjObstacleRefrigerator;//type 4
	public GameObject gameObjObstacleStove;//type 5
	
	public GameObject gameObjEnemy;//type 11
	public GameObject gameObjSuperEnergy;//type 15

	public GameObject gameObjCase;//type 21~25
	public GameObject gameObjKey;//type 31~35
	public Shader wallTransparentShader;
	public Shader wallNormalShader;


	public GameObject gameObjCharacter;

	private GameObject gameObjObstacleGenerated;
	
	int[] map;// = new int[400]; 
	GameObject[] objectMap;
	int width = 20;
	

	// Use this for initialization
	void Start () {

		map = new int[width * width]; 
		objectMap = new GameObject[(width + 2) * (width + 2)];
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
		UpdateObjectsStatus (10, 11, 2);//chair
		UpdateObjectsStatus (12, 13, 211);//chair
		UpdateObjectsStatus (10, 13, 3);//table
		UpdateObjectsStatus (13, 20, 4);//Refrigerator
		UpdateObjectsStatus (16, 12, 5);//Stove
		UpdateObjectsStatus (8, 13, 6);//chair2

		
		UpdateObjectsStatus (11, 2, 15);//super energy
		
		UpdateObjectsStatus (11, 11, 11);//enemy
		UpdateObjectsStatus (12, 11, 11);//enemy

		UpdateObjectsStatus (20, 14, 21);//blue cabinet
		UpdateObjectsStatus (17, 12, 22);//yellow cabinet
		UpdateObjectsStatus (8, 18, 23);//red cabinet
		UpdateObjectsStatus (6,  6, 24);//green cabinet
		UpdateObjectsStatus ( 14,  10, 25);//orange cabinet
		
		
		UpdateObjectsStatus ( 5,  3, 31);//blue key
		UpdateObjectsStatus ( 3, 16, 32);//yellow key
		UpdateObjectsStatus (18,  4, 33);//red key
		UpdateObjectsStatus (19, 18, 34);//green key
		UpdateObjectsStatus (10, 20, 35);//orange key


		gameObject.transform.position = new Vector3 ((float)width / 2 * 5.0f, 0.0f, (float)width / 2 * 5.0f);
		gameObject.transform.localScale = new Vector3 ((float)width / 2, 1, (float)width / 2);

		GenerateEnvironment ();

		GameObject objCharacter = Instantiate (gameObjCharacter, ComputePosition(10, 0, 10), Quaternion.identity) as GameObject;
		objCharacter.transform.localScale = new Vector3 (10, 10, 10);
		objCharacter.tag = "Player2";
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
			case 1://Add obstacle wall
				GameObject objWall = Instantiate (gameObjObstacleWall, ComputePosition(i % width + 1, 0,  i / width + 1), Quaternion.identity) as GameObject;	
				objWall.tag = "Wall";
				objWall.transform.localScale = new Vector3(2.0f, 5, 8);
				objWall.transform.localRotation = Quaternion.Euler( new Vector3(0, 0, 0));
				objectMap[i % width + 1 + (width + 2) * (i / width + 1)] = objWall;
				break;
			case 2://Add obstacle chair
				GameObject objChair = Instantiate (gameObjObstacleChair, ComputePosition(i % width + 1, 0,  i / width + 1), Quaternion.identity) as GameObject;	
				objChair.tag = "Chair";
				objChair.transform.localScale = new Vector3(5, 5, 5);
				objChair.transform.localRotation = Quaternion.Euler( new Vector3(0, 0, 0));
				break;
			case 211://Add obstacle chair
				GameObject objChaira = Instantiate (gameObjObstacleChair, ComputePosition(i % width + 1, 0,  i / width + 1), Quaternion.identity) as GameObject;	
				objChaira.tag = "Chair";
				objChaira.transform.localScale = new Vector3(5, 5, 5);
				objChaira.transform.localRotation = Quaternion.Euler( new Vector3(0, 270, 0));
				break;
			case 3://Add obstacle table
				GameObject objTable = Instantiate (gameObjObstacleTable, ComputePosition(i % width + 1, 0,  i / width + 1), Quaternion.identity) as GameObject;	
				objTable.tag = "Table";
				objTable.transform.localScale = new Vector3(8, 5, 8);
				objTable.transform.localRotation = Quaternion.Euler( new Vector3(0, 0, 0));
				UpdateObjectsStatus(i % width, i / width, 10);
				UpdateObjectsStatus(i % width, i / width + 1, 10);
				UpdateObjectsStatus(i % width, i / width + 2, 10);
				UpdateObjectsStatus(i % width + 1, i / width, 10);
				UpdateObjectsStatus(i % width + 1, i / width + 2, 10);
				UpdateObjectsStatus(i % width + 2, i / width, 10);
				UpdateObjectsStatus(i % width + 2, i / width + 1, 10);
				UpdateObjectsStatus(i % width + 2, i / width + 2, 10);
				break;
			case 4://Add obstacle Refrigerator
				GameObject objRefrigerator = Instantiate (gameObjObstacleRefrigerator, ComputePosition(i % width + 1, 0,  i / width + 1), Quaternion.identity) as GameObject;
				objRefrigerator.tag = "Refrigerator";
				objRefrigerator.transform.localScale = new Vector3(5, 5, 5);
				objRefrigerator.transform.localRotation = Quaternion.Euler( new Vector3(0, 180, 0));
				objectMap[i % width + 1 + (width + 2) * (i / width + 1)] = objRefrigerator;
				break;
			case 5://Add obstacle Stove
				GameObject objStove = Instantiate (gameObjObstacleStove, ComputePosition_Stove(i % width + 1, 0,  i / width + 1), Quaternion.identity) as GameObject;
				objStove.tag = "Stove";
				objStove.transform.localScale = new Vector3(6.8f, 5.2f, 5.2f);
				objStove.transform.localRotation = Quaternion.Euler( new Vector3(270, 180, 0));
				objectMap[i % width + 1 + (width + 2) * (i / width + 1)] = objStove;
				objectMap[i % width + (width + 2) * (i / width + 1)] = objStove;
				if(i % width != 0)
					UpdateObjectsStatus(i % width, i / width + 1, 10);
				break;
			case 6://Add obstacle chair
				GameObject objChair2 = Instantiate (gameObjObstacleChair2, ComputePosition_Chair2(i % width + 1, 0,  i / width + 1), Quaternion.identity) as GameObject;	
				objChair2.tag = "Chair";
				objChair2.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
				objChair2.transform.localRotation = Quaternion.Euler( new Vector3(270, 270, 0));
				break;
			case 11://Add Enemy
				GameObject objEnemy = Instantiate (gameObjEnemy, ComputePosition(i % width + 1, 0,  i / width + 1), Quaternion.identity) as GameObject;	
				objEnemy.tag = "Enemy";
				EnemyScript enemy = objEnemy.GetComponent<EnemyScript>();
				enemy.positionX = i % width + 1;
				enemy.positionZ = i / width + 1;
				break;
			case 15://Add Super Energy
				GameObject objSuperEnergy = Instantiate (gameObjSuperEnergy, ComputePosition_SuperEnergy(i % width + 1, 0,  i / width + 1), Quaternion.identity) as GameObject;	
				objSuperEnergy.tag = "SuperEnergy";
				objSuperEnergy.transform.localScale = new Vector3(3, 3, 3);
				objSuperEnergy.transform.localRotation = Quaternion.Euler( new Vector3(270, 0, 0));
				SuperEnergyScript superEnergy = objSuperEnergy.GetComponent<SuperEnergyScript>();
				superEnergy.positionX = i % width + 1;
				superEnergy.positionZ = i / width + 1;
				break;
			case 21://Add Blue Cabinet
				GameObject objCabinetBlue = Instantiate (gameObjCase, ComputePosition_Case(i % width + 1, 0,  i / width + 1), Quaternion.identity) as GameObject;
				objCabinetBlue.tag = "BlueCabinet";
				objCabinetBlue.transform.localScale = new Vector3(3, 5, 5);
				objCabinetBlue.transform.localRotation= Quaternion.Euler( new Vector3(90, 0, 0));
				CabinetScript cabinetBlue = objCabinetBlue.GetComponent<CabinetScript>();
				cabinetBlue.positionX = i % width + 1;
				cabinetBlue.positionZ = i / width + 1;
				cabinetBlue.SetColor(21);
				break;
			case 22://Add Yellow Cabinet
				GameObject objCabinetYellow = Instantiate (gameObjCase, ComputePosition_Case(i % width + 1, 0,  i / width + 1), Quaternion.identity) as GameObject;
				objCabinetYellow.tag = "YellowCabinet";
				objCabinetYellow.transform.localScale = new Vector3(3, 5, 5);
				objCabinetYellow.transform.localRotation= Quaternion.Euler( new Vector3(90, 0, 0));
				CabinetScript cabinetYellow = objCabinetYellow.GetComponent<CabinetScript>();
				cabinetYellow.positionX = i % width + 1;
				cabinetYellow.positionZ = i / width + 1;
				cabinetYellow.SetColor(22);
				break;
			case 23://Add Red Cabinet
				GameObject objCabinetRed = Instantiate (gameObjCase, ComputePosition_Case(i % width + 1, 0,  i / width + 1), Quaternion.identity) as GameObject;
				gameObjCase.tag = "RedCabinet";
				objCabinetRed.transform.localScale = new Vector3(3, 5, 5);
				objCabinetRed.transform.localRotation= Quaternion.Euler( new Vector3(90, 0, 0));
				CabinetScript cabinetRed = objCabinetRed.GetComponent<CabinetScript>();
				cabinetRed.positionX = i % width + 1;
				cabinetRed.positionZ = i / width + 1;

				cabinetRed.SetColor(23);
				break;
			case 24://Add Green Cabinet
				GameObject objCabinetGreen = Instantiate (gameObjCase, ComputePosition_Case(i % width + 1, 0,  i / width + 1), Quaternion.identity) as GameObject;
				objCabinetGreen.tag = "GreenCabinet";
				objCabinetGreen.transform.localScale = new Vector3(3, 5, 5);
				objCabinetGreen.transform.localRotation= Quaternion.Euler( new Vector3(90, 0, 0));
				CabinetScript cabinetGreen = objCabinetGreen.GetComponent<CabinetScript>();
				cabinetGreen.positionX = i % width + 1;
				cabinetGreen.positionZ = i / width + 1;
				cabinetGreen.SetColor(24);
				break;
			case 25://Add Orange Cabinet
				GameObject objCabinetOrange = Instantiate (gameObjCase, ComputePosition_Case(i % width + 1, 0,  i / width + 1), Quaternion.identity) as GameObject;
				objCabinetOrange.tag = "OrangeCabinet";
				objCabinetOrange.transform.localScale = new Vector3(3, 5, 5);
				objCabinetOrange.transform.localRotation= Quaternion.Euler( new Vector3(90, 0, 0));
				CabinetScript cabinetOrange = objCabinetOrange.GetComponent<CabinetScript>();
				cabinetOrange.positionX = i % width + 1;
				cabinetOrange.positionZ = i / width + 1;
				cabinetOrange.SetColor(25);
				break;

			case 31: //Add Blue key
				GameObject objKeyBlue = Instantiate (gameObjKey, ComputePosition_Key(i % width + 1, 0, i / width + 1), Quaternion.identity) as GameObject;
				objKeyBlue.tag = "BlueKey";
				objKeyBlue.transform.localScale = new Vector3(3, 3, 3);
				objKeyBlue.transform.localRotation= Quaternion.Euler( new Vector3(0, 0, 180));
				KeyScript keyBlue = objKeyBlue.GetComponent<KeyScript>();
				keyBlue.positionX = i % width + 1;
				keyBlue.positionZ = i / width + 1;
				keyBlue.SetColor(31);
				break;
			case 32: //Add Yellow key
				GameObject objKeyYellow = Instantiate (gameObjKey, ComputePosition_Key(i % width + 1, 0, i / width + 1), Quaternion.identity) as GameObject;
				objKeyYellow.tag = "YellowKey";
				objKeyYellow.transform.localScale = new Vector3(3, 3, 3);
				objKeyYellow.transform.localRotation= Quaternion.Euler( new Vector3(0, 0, 180));
				KeyScript keyYellow = objKeyYellow.GetComponent<KeyScript>();
				keyYellow.positionX = i % width + 1;
				keyYellow.positionZ = i / width + 1;
				keyYellow.SetColor(32);
				break;
			case 33: //Add Red Key
				GameObject objKeyRed = Instantiate (gameObjKey, ComputePosition_Key(i % width + 1, 0, i / width + 1), Quaternion.identity) as GameObject;
				objKeyRed.tag = "RedKey";
				objKeyRed.transform.localScale = new Vector3(3, 3, 3);
				objKeyRed.transform.localRotation= Quaternion.Euler( new Vector3(0, 0, 180));
				KeyScript keyRed = objKeyRed.GetComponent<KeyScript>();
				keyRed.positionX = i % width + 1;
				keyRed.positionZ = i / width + 1;
				keyRed.SetColor(33);
				break;
			case 34://Add Green Key
				GameObject objKeyGreen = Instantiate (gameObjKey, ComputePosition_Key(i % width + 1, 0, i / width + 1), Quaternion.identity) as GameObject;
				objKeyGreen.tag = "GreenKey";
				objKeyGreen.transform.localScale = new Vector3(3, 3, 3);
				objKeyGreen.transform.localRotation= Quaternion.Euler( new Vector3(0, 0, 180));
				KeyScript keyGreen = objKeyGreen.GetComponent<KeyScript>();
				keyGreen.positionX = i % width + 1;
				keyGreen.positionZ = i / width + 1;
				keyGreen.SetColor(34);
				break;
			case 35://Add Orange Key
				GameObject objKeyOrange = Instantiate (gameObjKey, ComputePosition_Key(i % width + 1, 0, i / width + 1), Quaternion.identity) as GameObject;
				objKeyOrange.tag = "OrangeKey";
				objKeyOrange.transform.localScale = new Vector3(3, 3, 3);
				objKeyOrange.transform.localRotation= Quaternion.Euler( new Vector3(0, 0, 180));
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
		for (int i = 0; i < width + 2; ++i) {//
			GameObject objWall = Instantiate (gameObjObstacleWall, ComputePosition(0, 0,  i), Quaternion.identity) as GameObject;
			objWall.transform.localScale = new Vector3(2.0f, 5, 8);
			objWall.transform.localRotation = Quaternion.Euler( new Vector3(0, 90, 0));

			objectMap[0 + (width + 2) * i] = objWall;

			GameObject objWall2 = Instantiate (gameObjObstacleWall, ComputePosition(width + 1, 0, i), Quaternion.identity) as GameObject;	
			objWall2.transform.localScale = new Vector3(2.0f, 5, 8);
			objWall2.transform.localRotation = Quaternion.Euler( new Vector3(0, 90, 0));
			objectMap[width + 1 + (width + 2) * i] = objWall;
		}

		//Horizontal Wall
		for (int i = 1; i < width + 1; ++i) {
			GameObject objWall = Instantiate (gameObjObstacleWall, ComputePosition(i, 0,  0), Quaternion.identity) as GameObject;	
			objWall.transform.localScale = new Vector3(2.0f, 5, 8);
			objWall.transform.localRotation = Quaternion.Euler( new Vector3(0, 0, 0));
			objectMap[i + (width + 2) * 0] = objWall;
			GameObject objWall2 = Instantiate (gameObjObstacleWall, ComputePosition(i, 0, width + 1), Quaternion.identity) as GameObject;	
			objWall2.transform.localScale = new Vector3(2.0f, 5, 8);
			objWall2.transform.localRotation = Quaternion.Euler( new Vector3(0, 0, 0));
			objectMap[i + (width + 2) * (width + 1)] = objWall;
		}
	}

	public int GetObjectTypeOnMap(int indexX, int indexZ){
		int index = (indexZ - 1) * width + (indexX - 1);
		return map [index];
	}

	public GameObject GetObjectOnObjectMap(int indexX, int indexZ){
		int index = (indexZ) * (width + 2) + (indexX );

		if (index < 0 || index >= (width + 2) * (width + 2))
			return null;
		else
			return objectMap [index];
	}

	public void UpdateObjectsStatus(int indexX, int indexZ, int objectStatus){
		int index = (indexZ - 1) * width + (indexX - 1);
		map [index] = objectStatus;
	}

	Vector3 ComputePosition(int x, int y, int z){
		Vector3 pos = new Vector3 (-2.5f + x * 5.0f, 0.0f, -2.5f + z * 5.0f);
		return pos;
	}

	Vector3 ComputePosition_Key(int x, int y, int z){
		Vector3 pos = new Vector3 (-2.5f + x * 5.0f, 0.5f, -2.5f + z * 5.0f);
		return pos;
	}

	Vector3 ComputePosition_Case(int x, int y, int z){
		Vector3 pos = new Vector3 (-2.5f + x * 5.0f, 4.2f, -2.5f + z * 5.0f);
		return pos;
	}

	Vector3 ComputePosition_SuperEnergy(int x, int y, int z){
		Vector3 pos = new Vector3 (-2.5f + x * 5.0f, 3.2f, -2.5f + z * 5.0f);
		return pos;
	}

	Vector3 ComputePosition_Stove(int x, int y, int z){
		Vector3 pos = new Vector3 (-2.5f + x * 5.0f - 1.9f, 0.0f, -2.5f + z * 5.0f );
		return pos;
	}

	Vector3 ComputePosition_Table(int x, int y, int z){
		Vector3 pos = new Vector3 (-2.5f + x * 5.0f - 5.0f, 3.2f, -2.5f + z * 5.0f - 1.0f);
		return pos;
	}

	Vector3 ComputePosition_Chair2(int x, int y, int z){
		Vector3 pos = new Vector3 (-2.5f + x * 5.0f, 0.0f, -2.5f + z * 5.0f);
		return pos;
	}

	public int GetMapSize(){
		return width;
	}
}
