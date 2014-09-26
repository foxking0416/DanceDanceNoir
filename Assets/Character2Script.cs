using UnityEngine;
using System.Collections;

public class Character2Script : MonoBehaviour {

	private GameObject gameObjGridMap;
	private GridMap map;

	int holdKeyStatus;//0: no key; 1: index
	int positionX;
	int positionZ;
	int width;

	// Use this for initialization
	void Start () {

		gameObjGridMap = GameObject.Find ("Map");
		map = gameObjGridMap.GetComponent< GridMap >();
		width = map.GetMapSize ();

		positionX = 10;
		positionZ = 10;
		gameObject.transform.position = ComputePosition(positionX,0 ,positionZ);
	}
	
	// Update is called once per frame
	void Update () {
		int objectType = map.GetObjectOnMap (positionX, positionZ);
		if (objectType == 11) {
			//Game Over		
		}



		if (Input.GetKeyDown ("w")) {
			MoveUp();
		}
		if (Input.GetKeyDown ("a")) {
			MoveLeft();
		}
		if (Input.GetKeyDown ("d")) {
			MoveRight();
		}
		if (Input.GetKeyDown ("s")) {
			MoveDown();
		}

		//pick up the key
		if (Input.GetKeyDown ("g")) {

			switch(objectType){
			case 31://Pick up Blue key
				GameObject objKeyBlue = GameObject.FindGameObjectWithTag("BlueKey");
				KeyScript keyBlue = objKeyBlue.GetComponent<KeyScript>();
				keyBlue.Pick();
				break;
			case 32://Pick up Yellow Key
				GameObject objKeyYellow = GameObject.FindGameObjectWithTag("YellowKey");
				KeyScript keyYellow = objKeyYellow.GetComponent<KeyScript>();
				keyYellow.Pick();
				break;
			case 33://Pick up Red Key
				GameObject objKeyRed = GameObject.FindGameObjectWithTag("RedKey");
				KeyScript keyRed = objKeyRed.GetComponent<KeyScript>();
				keyRed.Pick();
				break;
			case 34://Pick up Green Key
				GameObject objKeyGreen = GameObject.FindGameObjectWithTag("GreenKey");
				KeyScript keyGreen = objKeyGreen.GetComponent<KeyScript>();
				keyGreen.Pick();
				break;
			case 35://Pick up Orange Key
				GameObject objKeyOrange = GameObject.FindGameObjectWithTag("OrangeKey");
				KeyScript keyOrange = objKeyOrange.GetComponent<KeyScript>();
				keyOrange.Pick();
				break;
			}
		}

		//open the cabinet
		if (Input.GetKeyDown ("g")) {
			switch (objectType) {
			case 21://Pick up Blue key
					GameObject objCabinetBlue = GameObject.FindGameObjectWithTag ("BlueCabinet");
					CabinetScript cabinetBlue = objCabinetBlue.GetComponent<CabinetScript> ();
					cabinetBlue.OpenCabinet ();
					break;
			case 22://Pick up Yellow Key
					GameObject objCabinetYellow = GameObject.FindGameObjectWithTag ("YellowCabinet");
					CabinetScript cabinetYellow = objCabinetYellow.GetComponent<CabinetScript> ();
					cabinetYellow.OpenCabinet ();
					break;
			case 23://Pick up Red Key
					GameObject objCabinetRed = GameObject.FindGameObjectWithTag ("RedCabinet");
					CabinetScript cabinetRed = objCabinetRed.GetComponent<CabinetScript> ();
					cabinetRed.OpenCabinet ();
					break;
			case 24://Pick up Green Key
					GameObject objCabinetGreen = GameObject.FindGameObjectWithTag ("GreenCabinet");
					CabinetScript cabinetGreen = objCabinetGreen.GetComponent<CabinetScript> ();
					cabinetGreen.OpenCabinet ();
					break;
			case 25://Pick up Orange Key
					GameObject objCabinetOrange = GameObject.FindGameObjectWithTag ("OrangeCabinet");
					CabinetScript cabinetOrange = objCabinetOrange.GetComponent<CabinetScript> ();
					cabinetOrange.OpenCabinet ();
					break;
			}
		}

	}


	void MoveUp(){

		++positionZ;
	 	
		if (positionZ > width ) {
			--positionZ;		
		} 
		int objectType = map.GetObjectOnMap (positionX, positionZ);
		if (objectType == 1) {
			--positionZ;		
		}

		gameObject.transform.position = ComputePosition(positionX,0 ,positionZ);
	}

	void MoveLeft(){

		--positionX;

		if (positionX < 1) {
			++positionX;		
		}
		int objectType = map.GetObjectOnMap (positionX, positionZ);
		if (objectType == 1) {
			++positionX;		
		}



		gameObject.transform.position = ComputePosition(positionX,0 ,positionZ);
	}

	void MoveDown(){

		--positionZ;

		if (positionZ < 1) {
			++positionZ;		
		}
		int objectType = map.GetObjectOnMap (positionX, positionZ);
		if (objectType == 1) {
			++positionZ;		
		}

		gameObject.transform.position = ComputePosition(positionX,0 ,positionZ);
	}

	void MoveRight(){

		++positionX;
		if (positionX > width) {
			--positionX;		
		}
		int objectType = map.GetObjectOnMap (positionX, positionZ);
		if (objectType == 1) {
			--positionX;		
		}
		gameObject.transform.position = ComputePosition(positionX,0 ,positionZ);
	}


	Vector3 ComputePosition(int x, int y, int z){
		Vector3 pos = new Vector3 (-2.5f + x * 5.0f, 0.0f, -2.5f + z * 5.0f);
		return pos;
	}



	void PickUpKey(){}

	void OpenCabinet(){}

	void CollectEvidence(){}

	void GetSuperEnergy(){}

}
