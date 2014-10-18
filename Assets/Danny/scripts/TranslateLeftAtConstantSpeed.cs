using UnityEngine;
using System.Collections;

public class TranslateLeftAtConstantSpeed : MonoBehaviour
{
	////////////////////////////////////////////////////
	// Game object members.
	////////////////////////////////////////////////////


	public int obstaclePositionDiscreteX;
	public int obstaclePositionDiscreteY;
	
	private GameObject gameObjGrid;
	private Grid grid;
	
	void Start()
	{
		gameObjGrid = GameObject.FindGameObjectWithTag ("Grid");
		grid = gameObjGrid.GetComponent< Grid > ();
	}


	////////////////////////////////////////////////////
	// Called once per frame of gameplay.
	////////////////////////////////////////////////////
	void Update ()
	{

		Camera cam = ( Camera )GameObject.FindGameObjectWithTag( "Phase1Player1Camera" ).camera;
		if ( cam == null ) {
			return;
		}

	}

	public void MoveObstacle(){
		gameObjGrid = GameObject.FindGameObjectWithTag ("Grid");
		grid = gameObjGrid.GetComponent< Grid > ();
		grid.setObjectInGrid (obstaclePositionDiscreteX, obstaclePositionDiscreteY, -1);
		grid.setObjectInGrid (obstaclePositionDiscreteX, 1, -1);
		obstaclePositionDiscreteX--;
		if (obstaclePositionDiscreteX < 0) {
			Destroy (gameObject);
		} 
		else {
			grid.setObjectInGrid (obstaclePositionDiscreteX, obstaclePositionDiscreteY, 1);
			grid.setObjectInGrid (obstaclePositionDiscreteX, 1, 1);
			transform.position = grid.computeCratePosition (obstaclePositionDiscreteX, obstaclePositionDiscreteY);
		}
	}
	
}