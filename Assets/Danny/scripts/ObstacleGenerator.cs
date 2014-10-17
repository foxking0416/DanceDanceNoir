using UnityEngine;
using System.Collections;

public class ObstacleGenerator : MonoBehaviour
{
	////////////////////////////////////////////////////
	// Game object members.
	////////////////////////////////////////////////////

	public GameObject crate;
	private GameObject gameObjGrid;
	private Grid grid;
	public int height = 0;

	void Start () {
		
		gameObjGrid = GameObject.FindGameObjectWithTag ("Grid");
		grid = gameObjGrid.GetComponent< Grid > ();
	}

	////////////////////////////////////////////////////
	// Instantiate a new crate obstacle.
	////////////////////////////////////////////////////
	public void CreateCrate()
	{
		if (height == 2) {
			grid.setObjectInGrid (29, 2, 1);
			grid.setObjectInGrid (29, 1, 1);
			GameObject objCrate = Instantiate( crate, grid.computeCratePosition(29, 2), transform.rotation ) as GameObject;
			objCrate.tag = "Crate";
			TranslateLeftAtConstantSpeed translateScript = objCrate.GetComponent< TranslateLeftAtConstantSpeed >();
			translateScript.obstacleInitialX = 29;
			translateScript.obstacleInitialY = 2;
		} else if (height == 0) {
			grid.setObjectInGrid (29, 1, 1);
			grid.setObjectInGrid (29, 0, 1);
			GameObject objCrate = Instantiate( crate, grid.computeCratePosition(29, 0), transform.rotation ) as GameObject;
			objCrate.tag = "Crate";
			TranslateLeftAtConstantSpeed translateScript = objCrate.GetComponent< TranslateLeftAtConstantSpeed >();
			translateScript.obstacleInitialX = 29;
			translateScript.obstacleInitialY = 0;
		}

	}
	


}