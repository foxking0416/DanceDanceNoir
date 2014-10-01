using UnityEngine;
using System.Collections;

public class Character2Script : MonoBehaviour {

	public Shader transparentShader;
	public Shader normalShader;
	private GameObject gameObjGridMap;
	private GridMap map;
	private GameObject player2Camera;



	int positionX;
	int positionZ;
	int width;
	int holdKeyStatus;//0:no key; 31:blue key; 32:Yellow key; 33:Red Key; 34:Green Key; 35:Orange Key 
	ArrayList obstacleArray1 = new ArrayList();
	ArrayList obstacleArray2 = new ArrayList();
	// Use this for initialization

	void Start () {

		gameObjGridMap = GameObject.Find ("Map");
		map = gameObjGridMap.GetComponent< GridMap >();
		player2Camera= GameObject.Find ("Player2Camera2");
		width = map.GetMapSize ();

		positionX = 10;
		positionZ = 10;
		gameObject.transform.position = ComputePosition(positionX,0 ,positionZ);
		player2Camera.transform.localRotation = Quaternion.Euler (new Vector3 (45, 45, 0));//(30, 45, 0)
		player2Camera.transform.position = ComputeCameraPosition(positionX,0 ,positionZ, 45);
		player2Camera.camera.orthographicSize = 20;
		holdKeyStatus = 0;
	}
	
	// Update is called once per frame
	void Update () {
		int objectType = map.GetObjectTypeOnMap (positionX, positionZ);
		if (objectType == 11) {
			//Game Over
			Debug.Log("You are caught by enemy!!!!!!");
		}
		if (Input.GetKeyDown ("r")) {
			Vector3 oldRotation = player2Camera.transform.localRotation.eulerAngles;
			oldRotation += new Vector3 (0, 5, 0);
			player2Camera.transform.localRotation = Quaternion.Euler (oldRotation);
			player2Camera.transform.position = ComputeCameraPosition(positionX,0 ,positionZ, player2Camera.transform.localRotation.eulerAngles.y);

		}
		if (Input.GetKeyDown ("e")) {
			Vector3 oldRotation = player2Camera.transform.localRotation.eulerAngles;
			oldRotation -= new Vector3 (0, 5, 0);
			player2Camera.transform.localRotation = Quaternion.Euler (oldRotation);
			player2Camera.transform.position = ComputeCameraPosition(positionX,0 ,positionZ, player2Camera.transform.localRotation.eulerAngles.y);
			
		}


		if (Input.GetKeyDown ("w")) {
			MoveUp();

			player2Camera.transform.position = ComputeCameraPosition(positionX,0 ,positionZ, player2Camera.transform.localRotation.eulerAngles.y);
		}
		if (Input.GetKeyDown ("a")) {
			MoveLeft();
			player2Camera.transform.position = ComputeCameraPosition(positionX,0 ,positionZ, player2Camera.transform.localRotation.eulerAngles.y);
		}
		if (Input.GetKeyDown ("d")) {
			MoveRight();
			player2Camera.transform.position = ComputeCameraPosition(positionX,0 ,positionZ, player2Camera.transform.localRotation.eulerAngles.y);
		}
		if (Input.GetKeyDown ("s")) {
			MoveDown();
			player2Camera.transform.position = ComputeCameraPosition(positionX,0 ,positionZ, player2Camera.transform.localRotation.eulerAngles.y);
		}

		//pick up the key
		if (Input.GetKeyDown ("g") && holdKeyStatus == 0) {
			PickUpKey(objectType);
		}
//
/*		//open the cabinet
		if (Input.GetKeyDown ("c")) {
			OpenCabinet();
		}*/
//
//		if (Input.GetKeyDown ("h")) {
//			CollectEvidence(objectType);		
//		}
//
//		if (Input.GetKeyDown ("u")) {
//			PickUpSuperEnergy(objectType);		
//		}

	}


	void MoveUp(){

		++positionZ;
	 	
		if (positionZ > width ) {
			--positionZ;		
		} 
		int objectType = map.GetObjectTypeOnMap (positionX, positionZ);
		if (!CanWalkThrough(objectType)) {
			--positionZ;		
		}
		MakeObjectNormal (obstacleArray1);
		ComputeObstructViewObject (positionX, positionZ);
		gameObject.transform.position = ComputePosition(positionX,0 ,positionZ);
	}

	void MoveLeft(){

		--positionX;

		if (positionX < 1) {
			++positionX;		
		}
		int objectType = map.GetObjectTypeOnMap (positionX, positionZ);
		if (!CanWalkThrough(objectType)) {
			++positionX;		
		}

		MakeObjectNormal (obstacleArray1);
		ComputeObstructViewObject (positionX, positionZ);
		gameObject.transform.position = ComputePosition(positionX,0 ,positionZ);
	}

	void MoveDown(){

		--positionZ;

		if (positionZ < 1) {
			++positionZ;		
		}
		int objectType = map.GetObjectTypeOnMap (positionX, positionZ);
		if (!CanWalkThrough(objectType)) {
			++positionZ;		
		}
		MakeObjectNormal (obstacleArray1);
		ComputeObstructViewObject (positionX, positionZ);
		gameObject.transform.position = ComputePosition(positionX,0 ,positionZ);
	}

	void MoveRight(){

		++positionX;
		if (positionX > width) {
			--positionX;		
		}
		int objectType = map.GetObjectTypeOnMap (positionX, positionZ);
		if (!CanWalkThrough(objectType)) {
			--positionX;		
		}
		MakeObjectNormal (obstacleArray1);
		ComputeObstructViewObject (positionX, positionZ);
		gameObject.transform.position = ComputePosition(positionX,0 ,positionZ);
	}


	Vector3 ComputePosition(int x, int y, int z){
		Vector3 pos = new Vector3 (-2.5f + x * 5.0f, 0.0f, -2.5f + z * 5.0f);
		return pos;
	}

	Vector3 ComputeCameraPosition(int x, int y, int z, float angle){
		Vector3 pos = new Vector3 (-2.5f + x * 5.0f - 50.0f * Mathf.Cos(angle), 40.0f, -2.5f + z * 5.0f - 50.0f * Mathf.Cos(angle));
		//Vector3 pos = new Vector3 (-2.5f + x * 5.0f , 1.0f, -2.5f + z * 5.0f );
		return pos;
	}
	
	bool CheckAround(int cx, int cz, int type){
		int objectType;
		Debug.Log ("Judge");
		if (cx > 0 && cz > 0 && cx <= width && cz <= width) {
			objectType = map.GetObjectTypeOnMap (cx, cz);
			if (objectType == type)
					return true;
		}
		if (cx + 1 > 0 && cz > 0 && cx + 1 <= width && cz <= width) {
			objectType = map.GetObjectTypeOnMap (cx + 1, cz);
			if (objectType == type)
				return true;
		}
		if (cx > 0 && cz + 1 > 0 && cx <= width && cz + 1 <= width) {
			objectType = map.GetObjectTypeOnMap (cx, cz + 1);
			if (objectType == type)
				return true;
		}
		if (cx - 1 > 0 && cz > 0 && cx -1 <= width && cz <= width) {
			objectType = map.GetObjectTypeOnMap (cx - 1, cz);
			if (objectType == type)
				return true;
		}
		if (cx > 0 && cz - 1 > 0 && cx <= width && cz -1 <= width) {
			objectType = map.GetObjectTypeOnMap (cx, cz - 1);
			if (objectType == type)
				return true;
		}

		return false;
	}

	void PickUpKey(int objectType){
		switch(objectType){
		case 31://Pick up Blue key
			GameObject objKeyBlue = GameObject.FindGameObjectWithTag("BlueKey");
			KeyScript keyBlue = objKeyBlue.GetComponent<KeyScript>();
			keyBlue.Pick();
			holdKeyStatus = objectType;
			Debug.Log ("Pick up Blue key");
			break;
		case 32://Pick up Yellow Key
			GameObject objKeyYellow = GameObject.FindGameObjectWithTag("YellowKey");
			KeyScript keyYellow = objKeyYellow.GetComponent<KeyScript>();
			keyYellow.Pick();
			holdKeyStatus = objectType;
			Debug.Log ("Pick up Yellow key");
			break;
		case 33://Pick up Red Key
			GameObject objKeyRed = GameObject.FindGameObjectWithTag("RedKey");
			KeyScript keyRed = objKeyRed.GetComponent<KeyScript>();
			keyRed.Pick();
			holdKeyStatus = objectType;
			Debug.Log ("Pick up Red key");
			break;
		case 34://Pick up Green Key
			GameObject objKeyGreen = GameObject.FindGameObjectWithTag("GreenKey");
			KeyScript keyGreen = objKeyGreen.GetComponent<KeyScript>();
			keyGreen.Pick();
			holdKeyStatus = objectType;
			Debug.Log ("Pick up Green key");
			break;
		case 35://Pick up Orange Key
			GameObject objKeyOrange = GameObject.FindGameObjectWithTag("OrangeKey");
			KeyScript keyOrange = objKeyOrange.GetComponent<KeyScript>();
			keyOrange.Pick();
			holdKeyStatus = objectType;
			Debug.Log ("Pick up Orange key");
			break;
		}
	}
	
	void OpenCabinet(){
		if(holdKeyStatus == 31 && CheckAround(positionX, positionZ, 21)){
			GameObject objCabinetBlue = GameObject.FindGameObjectWithTag ("BlueCabinet");
			CabinetScript cabinetBlue = objCabinetBlue.GetComponent<CabinetScript> ();
			cabinetBlue.OpenCabinet ();
			holdKeyStatus = 0;
			Debug.Log("Open Blue Cabinet");
		}
		else if(holdKeyStatus == 32 && CheckAround(positionX, positionZ, 22)){
			GameObject objCabinetYellow = GameObject.FindGameObjectWithTag ("YellowCabinet");
			CabinetScript cabinetYellow = objCabinetYellow.GetComponent<CabinetScript> ();
			cabinetYellow.OpenCabinet ();
			holdKeyStatus = 0;
			Debug.Log("Open Yellow Cabinet");
		}
		else if(holdKeyStatus == 33 && CheckAround(positionX, positionZ, 23)){
			GameObject objCabinetRed = GameObject.FindGameObjectWithTag ("RedCabinet");
			CabinetScript cabinetRed = objCabinetRed.GetComponent<CabinetScript> ();
			cabinetRed.OpenCabinet ();
			holdKeyStatus = 0;
			Debug.Log("Open Red Cabinet");
		}
		else if(holdKeyStatus == 34 && CheckAround(positionX, positionZ, 24)){
			GameObject objCabinetGreen = GameObject.FindGameObjectWithTag ("GreenCabinet");
			CabinetScript cabinetGreen = objCabinetGreen.GetComponent<CabinetScript> ();
			cabinetGreen.OpenCabinet ();
			holdKeyStatus = 0;
			Debug.Log("Open Green Cabinet");
		}
		else if(holdKeyStatus == 35 && CheckAround(positionX, positionZ, 25)){
			GameObject objCabinetOrange = GameObject.FindGameObjectWithTag ("OrangeCabinet");
			CabinetScript cabinetOrange = objCabinetOrange.GetComponent<CabinetScript> ();
			cabinetOrange.OpenCabinet ();
			holdKeyStatus = 0;
			Debug.Log("Open Orange Cabinet");
		}
	}
	
	void CollectEvidence(int objectType){
		if (objectType == 3) {
			Debug.Log ("Collect the evidence");
			GameObject objEvidence = GameObject.FindGameObjectWithTag ("Evidence");
			EvidenceScript evidence = objEvidence.GetComponent<EvidenceScript> ();
			evidence.Collect ();
		}
	}
	
	void PickUpSuperEnergy(int objectType){
		if (objectType == 2) {
			Debug.Log ("Pick up Super energy");
			GameObject objSuperEnergy = GameObject.FindGameObjectWithTag ("SuperEnergy");
			SuperEnergyScript superEnergy = objSuperEnergy.GetComponent<SuperEnergyScript> ();
			superEnergy.Pick ();
		}
	}

	bool CanWalkThrough(int type){
		if (type == 1 || type == 2 || type == 3 || type == 4 || type == 5 || type == 6 || type == 10 || type == 21 || type == 22 || type == 23 || type == 24 || type == 25)
			return false;
		else 
			return true;
	}

	void ComputeObstructViewObject(int cx, int cz){

		MakeObjectTransparent(map.GetObjectOnObjectMap (cx-1, cz));
		MakeObjectTransparent(map.GetObjectOnObjectMap (cx-1, cz-1));
		MakeObjectTransparent(map.GetObjectOnObjectMap (cx-1, cz-2));
		MakeObjectTransparent(map.GetObjectOnObjectMap (cx, cz-1));
		MakeObjectTransparent(map.GetObjectOnObjectMap (cx-2, cz-1));
		MakeObjectTransparent(map.GetObjectOnObjectMap (cx-2, cz-2));
	}



	void MakeObjectTransparent(GameObject obj){
		if (obj != null) {

			if (obj.tag == "Refrigerator") {
				Debug.Log ("RefridgeratorPrefab");
				GameObject refriBody = GameObject.Find("Refridgerator");
				refriBody.renderer.material.shader = transparentShader;
				GameObject refriDoor = GameObject.Find("Refridgerator_Door");
				refriDoor.renderer.material.shader = transparentShader;
				obstacleArray1.Add (refriBody);
				obstacleArray1.Add (refriDoor);
			} 
			else {
				Debug.Log(obj.name);
					obj.renderer.material.shader = transparentShader;
					obstacleArray1.Add (obj);
			}
		}
	}

	void MakeObjectNormal(ArrayList arr){
		foreach (GameObject obj in arr) {
			obj.renderer.material.shader = normalShader;			
		}

		arr.Clear ();
	}
	
}
