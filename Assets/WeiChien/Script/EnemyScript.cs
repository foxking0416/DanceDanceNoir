using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

	private GameObject gameObjGridMap;
	private GridMap map;

	public int positionX;
	public int positionZ;
	int width;
	float timer;
	float beatTime;

	// Use this for initialization
	void Start () {
		gameObjGridMap = GameObject.Find ("Map");
		map = gameObjGridMap.GetComponent< GridMap >();
		width = map.GetMapSize ();
		timer = 0;
		beatTime = 1.0f;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;

		if (timer > beatTime) {
			RandomMove();
			timer = 0;
		}


	}

	public void RandomMove(){
		int moveDir = Random.Range (1, 5);
		switch (moveDir) {
		case 1:
			MoveUp();
			break;
		case 2:
			MoveDown();
			break;
		case 3:
			MoveLeft();
			break;
		case 4:
			MoveRight();
			break;
		}

	}


	void MoveUp(){

		map.UpdateObjectsStatus (positionX, positionZ, 0);
		++positionZ;
		
		if (positionZ > width ) {
			--positionZ;		
		} 
		int objectType = map.GetObjectOnMap (positionX, positionZ);
		if (objectType == 1 || objectType == 11 || objectType == 21 || objectType == 22 || objectType == 23 || objectType == 24 || objectType == 25) {
			--positionZ;		
		}
		
		gameObject.transform.position = ComputePosition(positionX,0 ,positionZ);
		map.UpdateObjectsStatus (positionX, positionZ, 11);
	}
	
	void MoveLeft(){

		map.UpdateObjectsStatus (positionX, positionZ, 0);
		--positionX;
		
		if (positionX < 1) {
			++positionX;		
		}
		int objectType = map.GetObjectOnMap (positionX, positionZ);
		if (objectType == 1 || objectType == 11 || objectType == 21 || objectType == 22 || objectType == 23 || objectType == 24 || objectType == 25) {
			++positionX;		
		}

		gameObject.transform.position = ComputePosition(positionX,0 ,positionZ);
		map.UpdateObjectsStatus (positionX, positionZ, 11);
	}
	
	void MoveDown(){

		map.UpdateObjectsStatus (positionX, positionZ, 0);
		--positionZ;
		
		if (positionZ < 1) {
			++positionZ;		
		}
		int objectType = map.GetObjectOnMap (positionX, positionZ);
		if (objectType == 1 || objectType == 11 || objectType == 21 || objectType == 22 || objectType == 23 || objectType == 24 || objectType == 25) {
			++positionZ;		
		}
		
		gameObject.transform.position = ComputePosition(positionX,0 ,positionZ);
		map.UpdateObjectsStatus (positionX, positionZ, 11);
	}
	
	void MoveRight(){

		map.UpdateObjectsStatus (positionX, positionZ, 0);
		++positionX;
		if (positionX > width) {
			--positionX;		
		}
		int objectType = map.GetObjectOnMap (positionX, positionZ);
		if (objectType == 1 || objectType == 11 || objectType == 21 || objectType == 22 || objectType == 23 || objectType == 24 || objectType == 25) {
			--positionX;		
		}
		gameObject.transform.position = ComputePosition(positionX,0 ,positionZ);
		map.UpdateObjectsStatus (positionX, positionZ, 11);
	}

	Vector3 ComputePosition(int x, int y, int z){
		Vector3 pos = new Vector3 (-2.5f + x * 5.0f, 0.0f, -2.5f + z * 5.0f);
		return pos;
	}
}
