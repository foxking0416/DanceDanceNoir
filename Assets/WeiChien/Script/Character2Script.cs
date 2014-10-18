using UnityEngine;
using System.Collections;

public class Character2Script : MonoBehaviour {

	public Shader transparentShader;
	public Shader normalShader;
	public AudioClip audioPickUpKey;
	public AudioClip audioOpenCase;
	public GameObject gameObjCamera;
	public GameObject gameObjTextEvidence;

	private GameObject gameObjGridMap;
	private GridMap map;
	private GameObject player2Camera;
	private GameObject gameObjPhase1;
	private GameObject objTextEvidence;
	private Phase1 phase1;
	private GlobalScript global;

	int positionX;
	int positionZ;
	int width;
	int holdKeyStatus;//0:no key; 31:blue key; 32:Yellow key; 33:Red Key; 34:Green Key; 35:Orange Key 
	ArrayList obstacleArray1 = new ArrayList();
	ArrayList obstacleArray2 = new ArrayList();
	int globalObjectType;
	// Use this for initialization

	void Start () {

		gameObjGridMap = GameObject.Find ("Map");
		map = gameObjGridMap.GetComponent< GridMap >();

	
		gameObjPhase1 = GameObject.Find("Phase1");
		phase1 = gameObjPhase1.GetComponent< Phase1 > ();
		global = gameObjPhase1.GetComponent<GlobalScript> ();
		width = map.GetMapSize ();

		positionX = 10;
		positionZ = 6;
		gameObject.transform.position = ComputePosition(positionX,0 ,positionZ);

		player2Camera = Instantiate (gameObjCamera, ComputeCameraPosition(positionX,0 ,positionZ, 45), Quaternion.Euler (new Vector3 (45, 45, 0))) as GameObject;	
		player2Camera.camera.orthographicSize = 40;

		objTextEvidence = Instantiate (gameObjTextEvidence, new Vector3(0,0,0), Quaternion.identity) as GameObject;	

		holdKeyStatus = 0;
	}
	
	// Update is called once per frame
	void Update () {
		globalObjectType = map.GetObjectTypeOnMap (positionX, positionZ);
		if (globalObjectType == 11) {
			//Game Over
			Debug.Log("You are caught by enemy!!!!!!");
		}

		/*
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
		Debug.Log(PlayerPrefs.GetInt("Signal2"));
		
		if(PlayerPrefs.GetInt("Signal2") == (int)Action.Up)
		//if (Input.GetKeyDown ("w")) 
		{
			Debug.Log("Move up");
			MoveUp();

			player2Camera.transform.position = ComputeCameraPosition(positionX,0 ,positionZ, player2Camera.transform.localRotation.eulerAngles.y);
		}
		if(PlayerPrefs.GetInt("Signal2") == (int)Action.Left){
		//if (Input.GetKeyDown ("a")) {
			MoveLeft();
			player2Camera.transform.position = ComputeCameraPosition(positionX,0 ,positionZ, player2Camera.transform.localRotation.eulerAngles.y);
		}
		if(PlayerPrefs.GetInt("Signal2") == (int)Action.Right){
		//if (Input.GetKeyDown ("d")) {
			MoveRight();
			player2Camera.transform.position = ComputeCameraPosition(positionX,0 ,positionZ, player2Camera.transform.localRotation.eulerAngles.y);
		}
		if(PlayerPrefs.GetInt("Signal2") == (int)Action.Down){
		//if (Input.GetKeyDown ("s")) {
			MoveDown();
			player2Camera.transform.position = ComputeCameraPosition(positionX,0 ,positionZ, player2Camera.transform.localRotation.eulerAngles.y);
		}

		//pick up the key
		if (Input.GetKeyDown ("g") && holdKeyStatus == 0) {
			PickUpKey(objectType);
		}*/
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

	 

	public void MoveUp(){

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
		gameObject.transform.localRotation = Quaternion.Euler (new Vector3 (0, 0, 0));
		player2Camera.transform.position = ComputeCameraPosition(positionX,0 ,positionZ, player2Camera.transform.localRotation.eulerAngles.y);
	}

	public void MoveRight(){
		
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
		gameObject.transform.localRotation = Quaternion.Euler (new Vector3 (0, 90, 0));
		player2Camera.transform.position = ComputeCameraPosition (positionX, 0, positionZ, player2Camera.transform.localRotation.eulerAngles.y);
	}

	public void MoveDown(){
		
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
		gameObject.transform.localRotation = Quaternion.Euler (new Vector3 (0, 180, 0));
		player2Camera.transform.position = ComputeCameraPosition (positionX, 0, positionZ, player2Camera.transform.localRotation.eulerAngles.y);
	}

	public void MoveLeft(){

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
		gameObject.transform.localRotation = Quaternion.Euler (new Vector3 (0, 270, 0));
		player2Camera.transform.position = ComputeCameraPosition (positionX, 0, positionZ, player2Camera.transform.localRotation.eulerAngles.y);
	}






	Vector3 ComputePosition(int x, int y, int z){
		Vector3 pos = new Vector3 (-2.5f + x * 5.0f, 0.0f, -2.5f + z * 5.0f);
		return pos;
	}

	Vector3 ComputeCameraPosition(int x, int y, int z, float angle){
		Vector3 pos = new Vector3 (-2.5f + x * 5.0f - 100.0f * Mathf.Cos(angle), 80.0f, -2.5f + z * 5.0f - 100.0f * Mathf.Cos(angle));
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

	public void PickUpKey(){
		switch(globalObjectType){
		case 31://Pick up Blue key
			GameObject objKeyBlue = GameObject.FindGameObjectWithTag("BlueKey");
			KeyScript keyBlue = objKeyBlue.GetComponent<KeyScript>();
			keyBlue.Pick();
			holdKeyStatus = globalObjectType;
			AudioSource.PlayClipAtPoint (audioPickUpKey, gameObject.transform.position);
			Debug.Log ("Pick up Blue key");
			break;
		case 32://Pick up Yellow Key
			GameObject objKeyYellow = GameObject.FindGameObjectWithTag("YellowKey");
			KeyScript keyYellow = objKeyYellow.GetComponent<KeyScript>();
			keyYellow.Pick();
			holdKeyStatus = globalObjectType;
			AudioSource.PlayClipAtPoint (audioPickUpKey, gameObject.transform.position);
			Debug.Log ("Pick up Yellow key");
			break;
		case 33://Pick up Red Key
			GameObject objKeyRed = GameObject.FindGameObjectWithTag("RedKey");
			KeyScript keyRed = objKeyRed.GetComponent<KeyScript>();
			keyRed.Pick();
			holdKeyStatus = globalObjectType;
			AudioSource.PlayClipAtPoint (audioPickUpKey, gameObject.transform.position);
			Debug.Log ("Pick up Red key");
			break;
		case 34://Pick up Green Key
			GameObject objKeyGreen = GameObject.FindGameObjectWithTag("GreenKey");
			KeyScript keyGreen = objKeyGreen.GetComponent<KeyScript>();
			keyGreen.Pick();
			holdKeyStatus = globalObjectType;
			AudioSource.PlayClipAtPoint (audioPickUpKey, gameObject.transform.position);
			Debug.Log ("Pick up Green key");
			break;
		case 35://Pick up Orange Key
			GameObject objKeyOrange = GameObject.FindGameObjectWithTag("OrangeKey");
			KeyScript keyOrange = objKeyOrange.GetComponent<KeyScript>();
			keyOrange.Pick();
			holdKeyStatus = globalObjectType;
			AudioSource.PlayClipAtPoint (audioPickUpKey, gameObject.transform.position);
			Debug.Log ("Pick up Orange key");
			break;
		}
	}
	
	public void OpenCabinet(){
		if(global.holdKeyStatus == 31 && CheckAround(positionX, positionZ, 21)){
		//if(holdKeyStatus == 31 && CheckAround(positionX, positionZ, 21)){
			GameObject objCabinetBlue = GameObject.FindGameObjectWithTag ("BlueCabinet");
			CabinetScript cabinetBlue = objCabinetBlue.GetComponent<CabinetScript> ();
			cabinetBlue.OpenCabinet ();
			//holdKeyStatus = 0;
			global.holdKeyStatus = 0;
			global.CollectEvidence(31);
			phase1.numberOfCollectedEvidence++;
			AudioSource.PlayClipAtPoint (audioOpenCase, gameObject.transform.position);
			Debug.Log("Open Blue Cabinet");
		}
		else if(global.holdKeyStatus == 32 && CheckAround(positionX, positionZ, 22)){
		//else if(holdKeyStatus == 32 && CheckAround(positionX, positionZ, 22)){
			GameObject objCabinetYellow = GameObject.FindGameObjectWithTag ("YellowCabinet");
			CabinetScript cabinetYellow = objCabinetYellow.GetComponent<CabinetScript> ();
			cabinetYellow.OpenCabinet ();
			//holdKeyStatus = 0;
			global.holdKeyStatus = 0;
			global.CollectEvidence(32);
			phase1.numberOfCollectedEvidence++;
			AudioSource.PlayClipAtPoint (audioOpenCase, gameObject.transform.position);
			Debug.Log("Open Yellow Cabinet");
		}
		else if(global.holdKeyStatus == 33 && CheckAround(positionX, positionZ, 23)){
		//else if(holdKeyStatus == 33 && CheckAround(positionX, positionZ, 23)){
			GameObject objCabinetRed = GameObject.FindGameObjectWithTag ("RedCabinet");
			CabinetScript cabinetRed = objCabinetRed.GetComponent<CabinetScript> ();
			cabinetRed.OpenCabinet ();
			//holdKeyStatus = 0;
			global.holdKeyStatus = 0;
			global.CollectEvidence(33);
			phase1.numberOfCollectedEvidence++;
			AudioSource.PlayClipAtPoint (audioOpenCase, gameObject.transform.position);
			Debug.Log("Open Red Cabinet");
		}
		else if(global.holdKeyStatus == 34 && CheckAround(positionX, positionZ, 24)){
		//else if(holdKeyStatus == 34 && CheckAround(positionX, positionZ, 24)){
			GameObject objCabinetGreen = GameObject.FindGameObjectWithTag ("GreenCabinet");
			CabinetScript cabinetGreen = objCabinetGreen.GetComponent<CabinetScript> ();
			cabinetGreen.OpenCabinet ();
			//holdKeyStatus = 0;
			global.holdKeyStatus = 0;
			global.CollectEvidence(34);
			phase1.numberOfCollectedEvidence++;
			AudioSource.PlayClipAtPoint (audioOpenCase, gameObject.transform.position);
			Debug.Log("Open Green Cabinet");
		}
		else if(global.holdKeyStatus == 35 && CheckAround(positionX, positionZ, 25)){
		//else if(holdKeyStatus == 35 && CheckAround(positionX, positionZ, 25)){
			GameObject objCabinetOrange = GameObject.FindGameObjectWithTag ("OrangeCabinet");
			CabinetScript cabinetOrange = objCabinetOrange.GetComponent<CabinetScript> ();
			cabinetOrange.OpenCabinet ();
			//holdKeyStatus = 0;
			global.holdKeyStatus = 0;
			global.CollectEvidence(35);
			phase1.numberOfCollectedEvidence++;
			AudioSource.PlayClipAtPoint (audioOpenCase, gameObject.transform.position);
			Debug.Log("Open Orange Cabinet");
		}
	}
	
	/*void CollectEvidence(int objectType){
		if (objectType == 3) {
			Debug.Log ("Collect the evidence");
			GameObject objEvidence = GameObject.FindGameObjectWithTag ("Evidence");
			EvidenceScript evidence = objEvidence.GetComponent<EvidenceScript> ();
			evidence.Collect ();
		}
	}*/
	
	public void PickUpSuperEnergy(){
		if (globalObjectType == 2) {
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
