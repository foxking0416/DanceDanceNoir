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
	float rotateAngle = 60;
	int moveBeatCount = 0;
	int beatNeedToMove = 0;

	// Use this for initialization
	void Start () {
		gameObjGridMap = GameObject.Find ("Map");
		map = gameObjGridMap.GetComponent< GridMap >();
		width = map.GetMapSize ();
		timer = 0;
		beatTime = 2.50f;

		beatNeedToMove = Random.Range (1, 5);
	}
	
	// Update is called once per frame
	void Update () {
		//timer += Time.deltaTime;
		rotateAngle += Time.deltaTime * 200;
		/*if (timer > beatTime) {
			RandomMove();
			timer = 0;
		}*/
		if(Input.GetKeyDown(KeyCode.G))
			RandomMove();
		gameObject.transform.localRotation = Quaternion.Euler (new Vector3 (0, rotateAngle, 0));
	}

	public void Destroy(){
		GameObject phase1 = GameObject.Find ("Phase1");
		Phase1 phase1Obj = phase1.GetComponent<Phase1> ();
		phase1Obj.noteSpeedReset ();
		map.UpdateObjectsStatus (positionX, positionZ, 0);
		map.UpdateObjectOnObjectMap (positionX, positionZ, null);
		Destroy (gameObject);
	}

	public void countBeat(){
		moveBeatCount++;

		if (moveBeatCount > beatNeedToMove) {
			moveBeatCount = 0;
			RandomMove();
			beatNeedToMove = Random.Range (1, 5);
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
		map.UpdateObjectOnObjectMap (positionX, positionZ, null);
		++positionZ;
		bool moveSuccess = true;
		if (positionZ > width) {
			--positionZ;	
			moveSuccess = false;
		} 
		else {
			int objectType = map.GetObjectTypeOnMap (positionX, positionZ);
			if (!CanWalkThrough (objectType)) {
				--positionZ;	
				moveSuccess = false;
			}
		}
		gameObject.transform.position = ComputePosition(positionX,0 ,positionZ);
		map.UpdateObjectsStatus (positionX, positionZ, 11);
		map.UpdateObjectOnObjectMap (positionX, positionZ, gameObject);
		if (moveSuccess == false)
			RandomMove ();
	}
	
	void MoveLeft(){

		map.UpdateObjectsStatus (positionX, positionZ, 0);
		map.UpdateObjectOnObjectMap (positionX, positionZ, null);
		--positionX;
		bool moveSuccess = true;
		if (positionX < 1) {
			++positionX;
			moveSuccess = false;
		} 
		else {
			int objectType = map.GetObjectTypeOnMap (positionX, positionZ);
			if (!CanWalkThrough (objectType)) {
				++positionX;
				moveSuccess = false;
			}
		}
		gameObject.transform.position = ComputePosition(positionX,0 ,positionZ);
		map.UpdateObjectsStatus (positionX, positionZ, 11);
		map.UpdateObjectOnObjectMap (positionX, positionZ, gameObject);
		if (moveSuccess == false)
			RandomMove ();
	}
	
	void MoveDown(){

		map.UpdateObjectsStatus (positionX, positionZ, 0);
		map.UpdateObjectOnObjectMap (positionX, positionZ, null);
		--positionZ;
		bool moveSuccess = true;
		if (positionZ < 1) {
			++positionZ;	
			moveSuccess = false;
		} 
		else {
			int objectType = map.GetObjectTypeOnMap (positionX, positionZ);
			if (!CanWalkThrough (objectType)) {
				++positionZ;
				moveSuccess = false;
			}
		}
		
		gameObject.transform.position = ComputePosition(positionX,0 ,positionZ);
		map.UpdateObjectsStatus (positionX, positionZ, 11);
		map.UpdateObjectOnObjectMap (positionX, positionZ, gameObject);
		if (moveSuccess == false)
			RandomMove ();
	}
	
	void MoveRight(){

		map.UpdateObjectsStatus (positionX, positionZ, 0);
		map.UpdateObjectOnObjectMap (positionX, positionZ, null);
		++positionX;
		bool moveSuccess = true;
		if (positionX > width) {
			--positionX;		
			moveSuccess = false;
		} 
		else {
			int objectType = map.GetObjectTypeOnMap (positionX, positionZ);
			if (!CanWalkThrough (objectType)) {
				--positionX;	
				moveSuccess = false;
			}
		}
		gameObject.transform.position = ComputePosition(positionX,0 ,positionZ);
		map.UpdateObjectsStatus (positionX, positionZ, 11);
		map.UpdateObjectOnObjectMap (positionX, positionZ, gameObject);
		if (moveSuccess == false)
			RandomMove ();
	}

	Vector3 ComputePosition(int x, int y, int z){
		Vector3 pos = new Vector3 (-2.5f + x * 5.0f, 1.5f, -2.5f + z * 5.0f);
		return pos;
	}

	bool CanWalkThrough(int type){
		if (type == 1 || type == 2 || type == 211 || type == 3 || type == 4 || type == 5 || type == 6|| type == 10 || type == 11 || type == 21 || type == 22 || type == 23 || type == 24 || type == 25)
			return false;
		else 
			return true;
	}
}
