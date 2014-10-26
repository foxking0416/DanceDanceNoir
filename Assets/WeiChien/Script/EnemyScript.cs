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


	// Use this for initialization
	void Start () {
		gameObjGridMap = GameObject.Find ("Map");
		map = gameObjGridMap.GetComponent< GridMap >();
		width = map.GetMapSize ();
		timer = 0;
		beatTime = 2.50f;


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
		phase1Obj.noteSpeedDecrease ();
		map.UpdateObjectsStatus (positionX, positionZ, 0);
		map.UpdateObjectOnObjectMap (positionX, positionZ, null);
		Destroy (gameObject);
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
		
		if (positionZ > width) {
						--positionZ;		
		} 
		else {
				int objectType = map.GetObjectTypeOnMap (positionX, positionZ);
				if (!CanWalkThrough (objectType)) {
						--positionZ;		
				}
		}
		gameObject.transform.position = ComputePosition(positionX,0 ,positionZ);
		map.UpdateObjectsStatus (positionX, positionZ, 11);
		map.UpdateObjectOnObjectMap (positionX, positionZ, gameObject);
	}
	
	void MoveLeft(){

		map.UpdateObjectsStatus (positionX, positionZ, 0);
		map.UpdateObjectOnObjectMap (positionX, positionZ, null);
		--positionX;
		
		if (positionX < 1) {
			++positionX;		
		} 
		else {
			int objectType = map.GetObjectTypeOnMap (positionX, positionZ);
			if (!CanWalkThrough (objectType)) {
					++positionX;		
			}
		}
		gameObject.transform.position = ComputePosition(positionX,0 ,positionZ);
		map.UpdateObjectsStatus (positionX, positionZ, 11);
		map.UpdateObjectOnObjectMap (positionX, positionZ, gameObject);
	}
	
	void MoveDown(){

		map.UpdateObjectsStatus (positionX, positionZ, 0);
		map.UpdateObjectOnObjectMap (positionX, positionZ, null);
		--positionZ;
		
		if (positionZ < 1) {
			++positionZ;		
		} 
		else {
			int objectType = map.GetObjectTypeOnMap (positionX, positionZ);
			if (!CanWalkThrough (objectType)) {
					++positionZ;		
			}
		}
		
		gameObject.transform.position = ComputePosition(positionX,0 ,positionZ);
		map.UpdateObjectsStatus (positionX, positionZ, 11);
		map.UpdateObjectOnObjectMap (positionX, positionZ, gameObject);
	}
	
	void MoveRight(){

		map.UpdateObjectsStatus (positionX, positionZ, 0);
		map.UpdateObjectOnObjectMap (positionX, positionZ, null);
		++positionX;
		if (positionX > width) {
			--positionX;		
		} 
		else {
			int objectType = map.GetObjectTypeOnMap (positionX, positionZ);
			if (!CanWalkThrough (objectType)) {
					--positionX;		
			}
		}
		gameObject.transform.position = ComputePosition(positionX,0 ,positionZ);
		map.UpdateObjectsStatus (positionX, positionZ, 11);
		map.UpdateObjectOnObjectMap (positionX, positionZ, gameObject);
	}

	Vector3 ComputePosition(int x, int y, int z){
		Vector3 pos = new Vector3 (-2.5f + x * 5.0f, 1.5f, -2.5f + z * 5.0f);
		return pos;
	}

	bool CanWalkThrough(int type){
		if (type == 1 || type == 2 || type == 3 || type == 4 || type == 5 || type == 6|| type == 10 || type == 21 || type == 22 || type == 23 || type == 24 || type == 25)
			return false;
		else 
			return true;
	}
}
