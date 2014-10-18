using UnityEngine;
using System.Collections;

public class Key : MonoBehaviour {

	public int keyPositionDiscreteX;
	public int keyPositionDiscreteY;
	
	private GameObject gameObjGrid;
	private Grid grid;

	private int keyColor;

	// Use this for initialization
	void Start () {
		gameObjGrid = GameObject.FindGameObjectWithTag ("Grid");
		grid = gameObjGrid.GetComponent< Grid > ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void MoveKey(){
		gameObjGrid = GameObject.FindGameObjectWithTag ("Grid");
		grid = gameObjGrid.GetComponent< Grid > ();
		grid.setObjectInGrid (keyPositionDiscreteX, keyPositionDiscreteY, -1);
		keyPositionDiscreteX--;
		if (keyPositionDiscreteX < 0) {
			Destroy (gameObject);
		} 
		else {
			grid.setObjectInGrid (keyPositionDiscreteX, keyPositionDiscreteY, keyColor);
			transform.position = grid.computeCratePosition (keyPositionDiscreteX, keyPositionDiscreteY);
		}
	}
}
