using UnityEngine;
using System.Collections;

public class CabinetScript : MonoBehaviour {
	private GameObject gameObjGridMap;
	private GridMap map;
	
	public int positionX;
	public int positionZ;

	public Material materialBlue;
	public Material materialYellow;
	public Material materialRed;
	public Material materialGreen;
	public Material materialOrange;

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
		case 21:
			gameObject.renderer.material = materialBlue;
			break;
		case 22:
			gameObject.renderer.material = materialYellow;
			break;
		case 23:
			gameObject.renderer.material = materialRed;
			break;
		case 24:
			gameObject.renderer.material = materialGreen;
			break;
		case 25:
			gameObject.renderer.material = materialOrange;
			break;
		}
	}

	public void OpenCabinet(){
		//Create the evidence game object

		map.UpdateObjectsStatus (positionX, positionZ, 0);
		Destroy (gameObject);
	}
}
