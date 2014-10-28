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
	private GameObject gameObjPhase1;
	private GlobalScript global;
	private GameObject objCaseBlueForShow;
	private GameObject objCaseYellowForShow;
	private GameObject objCaseRedForShow;
	private GameObject objCaseGreenForShow;
	private GameObject objCaseOrangeForShow;

	int[] map;// = new int[400]; 
	GameObject[] objectMap;
	int width = 20;
	

	// Use this for initialization
	void Start () {

		map = new int[width * width]; 
		objectMap = new GameObject[(width + 2) * (width + 2)];
		UpdateObjectsStatus (4, 5, 1);
		UpdateObjectsStatus (4, 6, 1);
		UpdateObjectsStatus (4, 7, 1);
		UpdateObjectsStatus (5, 7, 1);
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
		UpdateObjectsStatus (11, 3, 1);
		UpdateObjectsStatus (12, 3, 1);
		UpdateObjectsStatus (13, 3, 1);
		UpdateObjectsStatus (8, 20, 1);
		UpdateObjectsStatus (8, 19, 1);
		UpdateObjectsStatus (8, 17, 1);
		UpdateObjectsStatus (9, 17, 1);
		UpdateObjectsStatus (9, 18, 1);
		UpdateObjectsStatus (9, 19, 1);
		UpdateObjectsStatus (9, 20, 1);
		UpdateObjectsStatus (15, 14, 1);
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

		
		//UpdateObjectsStatus (11, 2, 15);//super energy
		
		UpdateObjectsStatus (5, 11, 11);//enemy
		UpdateObjectsStatus (18, 18, 11);//enemy

		UpdateObjectsStatus (20, 14, 21);//blue cabinet
		UpdateObjectsStatus (15, 1, 22);//yellow cabinet
		UpdateObjectsStatus (8, 18, 23);//red cabinet
		UpdateObjectsStatus (6,  6, 24);//green cabinet
		UpdateObjectsStatus ( 14,  10, 25);//orange cabinet
		
	

		gameObject.transform.position = new Vector3 ((float)width / 2 * 5.0f, 0.0f, (float)width / 2 * 5.0f);
		gameObject.transform.localScale = new Vector3 ((float)width / 2, 1, (float)width / 2);

		GenerateEnvironment ();

		GameObject objCharacter = Instantiate (gameObjCharacter, ComputePosition(10, 0, 10), Quaternion.identity) as GameObject;
		objCharacter.transform.localScale = new Vector3 (10, 10, 10);
		objCharacter.tag = "Player2";

		gameObjPhase1 = GameObject.Find("Phase1");
		global = gameObjPhase1.GetComponent<GlobalScript> ();
		objCaseBlueForShow = GameObject.FindGameObjectWithTag ("CaseBlueForShow");
		objCaseYellowForShow = GameObject.FindGameObjectWithTag ("CaseYellowForShow");
		objCaseRedForShow = GameObject.FindGameObjectWithTag ("CaseRedForShow");
		objCaseGreenForShow = GameObject.FindGameObjectWithTag ("CaseGreenForShow");
		objCaseOrangeForShow = GameObject.FindGameObjectWithTag ("CaseOrangeForShow");
	}
	

	// Update is called once per frame
	void Update () {
		/*if (Input.GetKeyDown(KeyCode.B)){
			recreateCase();
		}*/

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
				objectMap[i % width + 1 + (width + 2) * (i / width + 1)] = objChair;
				break;
			case 211://Add obstacle chair
				GameObject objChaira = Instantiate (gameObjObstacleChair, ComputePosition(i % width + 1, 0,  i / width + 1), Quaternion.identity) as GameObject;	
				objChaira.tag = "Chair";
				objChaira.transform.localScale = new Vector3(5, 5, 5);
				objChaira.transform.localRotation = Quaternion.Euler( new Vector3(0, 270, 0));
				objectMap[i % width + 1 + (width + 2) * (i / width + 1)] = objChaira;
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
				objectMap[i % width + 0 + (width + 2) * (i / width + 0)] = objTable;
				objectMap[i % width + 1 + (width + 2) * (i / width + 0)] = objTable;
				objectMap[i % width + 2 + (width + 2) * (i / width + 0)] = objTable;
				objectMap[i % width + 0 + (width + 2) * (i / width + 1)] = objTable;
				objectMap[i % width + 1 + (width + 2) * (i / width + 1)] = objTable;
				objectMap[i % width + 2 + (width + 2) * (i / width + 1)] = objTable;
				objectMap[i % width + 0 + (width + 2) * (i / width + 2)] = objTable;
				objectMap[i % width + 1 + (width + 2) * (i / width + 2)] = objTable;
				objectMap[i % width + 2 + (width + 2) * (i / width + 2)] = objTable;
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
				GameObject objChair2 = Instantiate (gameObjObstacleChair2, ComputePosition(i % width + 1, 0,  i / width + 1), Quaternion.identity) as GameObject;	
				objChair2.tag = "Chair";
				objChair2.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
				objChair2.transform.localRotation = Quaternion.Euler( new Vector3(270, 270, 0));
				objectMap[i % width + 1 + (width + 2) * (i / width + 1)] = objChair2;
				break;
			case 11://Add Enemy
				GameObject objEnemy = Instantiate (gameObjEnemy, ComputePosition(i % width + 1, 1.5f,  i / width + 1), Quaternion.identity) as GameObject;	
				objEnemy.tag = "Enemy";
				EnemyScript enemy = objEnemy.GetComponent<EnemyScript>();
				enemy.positionX = i % width + 1;
				enemy.positionZ = i / width + 1;
				objectMap[i % width + 1 + (width + 2) * (i / width + 1)] = objEnemy;
				break;
			case 21://Add Blue Cabinet
				GenerateCase(i % width + 1, i / width + 1, "BlueCabinet", 21);
				break;
			case 22://Add Yellow Cabinet
				GenerateCase(i % width + 1, i / width + 1, "YellowCabinet", 22);
				break;
			case 23://Add Red Cabinet
				GenerateCase(i % width + 1, i / width + 1, "RedCabinet", 23);
				break;
			case 24://Add Green Cabinet
				GenerateCase(i % width + 1, i / width + 1, "GreenCabinet", 24);
				break;
			case 25://Add Orange Cabinet
				GenerateCase(i % width + 1, i / width + 1, "OrangeCabinet", 25);
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

	public void GenerateStar(){
		int indexX = Random.Range (1, 21);
		int indexZ = Random.Range (1, 21);


		while (GetObjectTypeOnMap(indexX, indexZ) != 0) {
			indexX = Random.Range (1, 21);
         	indexZ = Random.Range (1, 21);
		}
		int index = (indexZ - 1) * width + (indexX - 1);

		GameObject objStar = Instantiate (gameObjEnemy, ComputePosition(index % width + 1, 1.5f,  index / width + 1), Quaternion.identity) as GameObject;	
		objStar.tag = "Enemy";
		EnemyScript star = objStar.GetComponent<EnemyScript>();
		star.positionX = index % width + 1;
		star.positionZ = index / width + 1;
		objectMap[index % width + 1 + (width + 2) * (index / width + 1)] = objStar;
		Debug.Log("Create Enemy!!!!");
	}

	private void GenerateCase(int x, int z, string tagname, int color){
		GameObject objCabinet = Instantiate (gameObjCase, ComputePosition (x, 4.2f, z), Quaternion.identity) as GameObject;
		objCabinet.tag = tagname;
		objCabinet.transform.localScale = new Vector3 (3, 5, 5);
		objCabinet.transform.localRotation = Quaternion.Euler (new Vector3 (90, 0, 0));
		CabinetScript cabinet = objCabinet.GetComponent<CabinetScript> ();
		cabinet.positionX = x;
		cabinet.positionZ = z;
		cabinet.SetColor (color);
		objectMap [x + (width + 2) * z] = objCabinet;
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

	public void UpdateObjectOnObjectMap(int indexX, int indexZ, GameObject obj){
		int index = (indexZ) * (width + 2) + (indexX );

		objectMap [index] = obj;
	}


	public void UpdateObjectsStatus(int indexX, int indexZ, int objectStatus){
		int index = (indexZ - 1) * width + (indexX - 1);
		map [index] = objectStatus;
	}


	Vector3 ComputePosition(float x, float y, float z){
		Vector3 pos = new Vector3 (-2.5f + x * 5.0f, y, -2.5f + z * 5.0f);
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


	public int GetMapSize(){
		return width;
	}

	public void recreateCase(){
		int collectNum = global.getCollectEvidenceNumber ();
		if (collectNum == 0)
			return;
		int count = 0;
		bool[] caseOpenStatus = global.getEvidenceCollectStatus ();
		int createCaseIndex = Random.Range (0, 5);
		while (caseOpenStatus[createCaseIndex] == false) {
			createCaseIndex = Random.Range (0, 5);
			count++;
			if(count > 1000)
				return;
		}
		Character2Script player2 = GameObject.FindGameObjectWithTag ("Player2").GetComponent<Character2Script>();;
		int newPosX = Random.Range (1, 21);
		int newPosZ = Random.Range (1, 21);

		count = 0;
		while(GetObjectTypeOnMap(newPosX, newPosZ) == 1 || 
		      GetObjectTypeOnMap(newPosX, newPosZ) == 2 ||
		      GetObjectTypeOnMap(newPosX, newPosZ) == 3 ||
		      GetObjectTypeOnMap(newPosX, newPosZ) == 4 ||
		      GetObjectTypeOnMap(newPosX, newPosZ) == 5 ||
		      GetObjectTypeOnMap(newPosX, newPosZ) == 6 ||
		      GetObjectTypeOnMap(newPosX, newPosZ) == 10 ||
		      GetObjectTypeOnMap(newPosX, newPosZ) == 11 ||
		      GetObjectTypeOnMap(newPosX, newPosZ) == 211 ||
		      GetObjectTypeOnMap(newPosX, newPosZ) == 21 ||
		      GetObjectTypeOnMap(newPosX, newPosZ) == 22 ||
		      GetObjectTypeOnMap(newPosX, newPosZ) == 23 ||
		      GetObjectTypeOnMap(newPosX, newPosZ) == 24 ||
		      GetObjectTypeOnMap(newPosX, newPosZ) == 25 ||
		      (newPosX == player2.getPlayer2PosX() && newPosZ == player2.getPlayer2PosZ())){
			newPosX = Random.Range (1, 21);
			newPosZ = Random.Range (1, 21);
			count++;

			if(count > 1000)
				return;
		}
		if (createCaseIndex == 0) {
			UpdateObjectsStatus(newPosX, newPosZ, 21);
			GenerateCase(newPosX, newPosZ, "BlueCabinet", 21);
			global.removeEvidence(31);
			objCaseBlueForShow.SetActive(true);
		}
		else if(createCaseIndex == 1) {
			UpdateObjectsStatus(newPosX, newPosZ, 22);
			GenerateCase(newPosX, newPosZ, "YellowCabinet", 22);
			global.removeEvidence(32);
			objCaseYellowForShow.SetActive(true);
		}
		else if(createCaseIndex == 2) {
			UpdateObjectsStatus(newPosX, newPosZ, 23);
			GenerateCase(newPosX, newPosZ, "RedCabinet", 23);
			global.removeEvidence(33);
			objCaseRedForShow.SetActive(true);
		}
		else if(createCaseIndex == 3) {
			UpdateObjectsStatus(newPosX, newPosZ, 24);
			GenerateCase(newPosX, newPosZ, "GreenCabinet", 24);
			global.removeEvidence(34);
			objCaseGreenForShow.SetActive(true);
		}
		else if(createCaseIndex == 4) {
			UpdateObjectsStatus(newPosX, newPosZ, 25);
			GenerateCase(newPosX, newPosZ, "OrangeCabinet", 25);
			global.removeEvidence(35);
			objCaseOrangeForShow.SetActive(true);
		}


	}
}
