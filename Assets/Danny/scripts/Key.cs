using UnityEngine;
using System.Collections;

public class Key : MonoBehaviour {

	public int keyPositionDiscreteX;
	public int keyPositionDiscreteY;
	
	private GameObject gameObjGrid;
	private Grid grid;

	public int keyColor;

	public Material materialBlue;
	public Material materialYellow;
	public Material materialRed;
	public Material materialGreen;
	public Material materialOrange;
	
	// Use this for initialization
	void Start () {
		gameObjGrid = GameObject.FindGameObjectWithTag ("Grid");
		grid = gameObjGrid.GetComponent< Grid > ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Pick(){
		grid.setObjectInGrid (keyPositionDiscreteX, keyPositionDiscreteY, -1);
		Destroy (gameObject);
	}
	
	public void MoveKey(){
		gameObjGrid = GameObject.FindGameObjectWithTag ("Grid");
		grid = gameObjGrid.GetComponent< Grid > ();
		//grid.setObjectInGrid (keyPositionDiscreteX, keyPositionDiscreteY, -1);
		keyPositionDiscreteX--;
		if (keyPositionDiscreteX < 0) {
			Destroy (gameObject);
		} 
		else {
			grid.setObjectInGrid (keyPositionDiscreteX, keyPositionDiscreteY, keyColor);
			transform.position = grid.computeCratePosition (keyPositionDiscreteX, keyPositionDiscreteY);
		}
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
}
