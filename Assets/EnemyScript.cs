using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

	private GameObject gameObjGridMap;
	private GridMap map;

	int positionX;
	int positionZ;
	int width;

	// Use this for initialization
	void Start () {
		gameObjGridMap = GameObject.Find ("Map");
		map = gameObjGridMap.GetComponent< GridMap >();
		width = map.GetMapSize ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void RandomMove(){

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
}
