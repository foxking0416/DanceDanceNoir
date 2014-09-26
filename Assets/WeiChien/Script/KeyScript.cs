using UnityEngine;
using System.Collections;

public class KeyScript : MonoBehaviour {
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

	public void Pick(){
		map.UpdateObjectsStatus (positionX, positionZ, 0);
		Destroy (gameObject);
	}

	void UpdateMap(){

	}

	public void SetColor(int color){
		switch (color) {
		case 31:
			gameObject.renderer.material = materialBlue;
			break;
		case 32:
			gameObject.renderer.material = materialYellow;
			break;
		case 33:
			gameObject.renderer.material = materialRed;
			break;
		case 34:
			gameObject.renderer.material = materialGreen;
			break;
		case 35:
			gameObject.renderer.material = materialOrange;
			break;
		}
	}

	Vector3 ComputePosition(int x, int y, int z){
		Vector3 pos = new Vector3 (-2.5f + x * 5.0f, 0.0f, -2.5f + z * 5.0f);
		return pos;
	}
}
