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

	void Start ()
	{	
		gameObjGrid = GameObject.FindGameObjectWithTag( "Grid" );
		grid = gameObjGrid.GetComponent< Grid >();
	}

	////////////////////////////////////////////////////
	// Instantiate a new crate obstacle.
	////////////////////////////////////////////////////
	public void CreateCrate()
	{
		int start_col = grid.getWidth();

		if (height == 2) {
			grid.setObjectInGrid (start_col, 2, 1);
			grid.setObjectInGrid (start_col, 1, 1);
			GameObject objCrate1 = Instantiate( crate, grid.computeCratePosition(start_col, 2), transform.rotation ) as GameObject;
			GameObject objCrate2 = Instantiate( crate, grid.computeCratePosition(start_col, 1), transform.rotation ) as GameObject;
			objCrate1.tag = "Crate";
			objCrate2.tag = "Crate";
			TranslateLeftAtConstantSpeed translateScript1 = objCrate1.GetComponent< TranslateLeftAtConstantSpeed >();
			TranslateLeftAtConstantSpeed translateScript2 = objCrate2.GetComponent< TranslateLeftAtConstantSpeed >();
			translateScript1.obstaclePositionDiscreteX = start_col;
			translateScript1.obstaclePositionDiscreteY = 2;
			translateScript2.obstaclePositionDiscreteX = start_col;
			translateScript2.obstaclePositionDiscreteY = 1;
		} else if (height == 0) {
			grid.setObjectInGrid (start_col, 1, 1);
			grid.setObjectInGrid (start_col, 0, 1);
			GameObject objCrate1 = Instantiate( crate, grid.computeCratePosition(start_col, 0), transform.rotation ) as GameObject;
			GameObject objCrate2 = Instantiate( crate, grid.computeCratePosition(start_col, 1), transform.rotation ) as GameObject;
			objCrate1.tag = "Crate";
			objCrate2.tag = "Crate";
			TranslateLeftAtConstantSpeed translateScript1 = objCrate1.GetComponent< TranslateLeftAtConstantSpeed >();
			TranslateLeftAtConstantSpeed translateScript2 = objCrate2.GetComponent< TranslateLeftAtConstantSpeed >();
			translateScript1.obstaclePositionDiscreteX = start_col;
			translateScript1.obstaclePositionDiscreteY = 0;
			translateScript2.obstaclePositionDiscreteX = start_col;
			translateScript2.obstaclePositionDiscreteY = 1;
		}
	}
}