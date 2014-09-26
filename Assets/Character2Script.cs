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

	}


	void MoveUp(){

		++positionZ;
	 	int objectType = map.GetObjectOnMap (positionX, positionZ);

		if (positionZ > width || objectType == 1) {
			--positionZ;		
		}
		gameObject.transform.position = ComputePosition(positionX,0 ,positionZ);
	}

	void MoveLeft(){

		--positionX;
		int objectType = map.GetObjectOnMap (positionX, positionZ);
		if (positionX < 1 || objectType == 1) {
			++positionX;		
		}
		gameObject.transform.position = ComputePosition(positionX,0 ,positionZ);
	}

	void MoveDown(){

		--positionZ;
		int objectType = map.GetObjectOnMap (positionX, positionZ);
		if (positionZ < 1 || objectType == 1) {
			++positionZ;		
		}
		gameObject.transform.position = ComputePosition(positionX,0 ,positionZ);
	}

	void MoveRight(){

		++positionX;
		int objectType = map.GetObjectOnMap (positionX, positionZ);
		if (positionX > width || objectType == 1) {
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
