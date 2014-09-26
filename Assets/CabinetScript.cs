using UnityEngine;
using System.Collections;

public class CabinetScript : MonoBehaviour {
	private GameObject gameObjGridMap;
	private GridMap map;
	
	public int positionX;
	public int positionZ;
	

	// Use this for initialization
	void Start () {
		gameObjGridMap = GameObject.Find ("Map");
		map = gameObjGridMap.GetComponent< GridMap >();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetColor(int color){
		switch (color) {
		case 31:
			
			break;
		case 32:
			break;
		case 33:
			break;
		case 34:
			break;
		case 35:
			break;
		}
	}

	public void OpenCabinet(){
		//Create the evidence game object

		map.UpdateObjectsStatus (positionX, positionZ, 0);
		Destroy (gameObject);
	}
}
