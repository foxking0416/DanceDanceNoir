using UnityEngine;
using System.Collections;

public class KeyScript : MonoBehaviour {
	private GameObject gameObjGridMap;
	private GridMap map;

	int positionX;
	int positionZ;
	int color;
	
	// Use this for initialization
	void Start () {
		gameObjGridMap = GameObject.Find ("Map");
		map = gameObjGridMap.GetComponent< GridMap >();
		
		positionX = 3;
		positionZ = 5;
		gameObject.transform.position = ComputePosition(positionX,0 ,positionZ);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void Pick(){
		map.UpdateObjectsStatus (positionX, positionZ, 0);
	}

	void UpdateMap(){

	}

	Vector3 ComputePosition(int x, int y, int z){
		Vector3 pos = new Vector3 (-2.5f + x * 5.0f, 0.0f, -2.5f + z * 5.0f);
		return pos;
	}
}
