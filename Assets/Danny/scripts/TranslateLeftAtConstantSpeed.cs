using UnityEngine;
using System.Collections;

public class TranslateLeftAtConstantSpeed : MonoBehaviour
{
	////////////////////////////////////////////////////
	// Game object members.
	////////////////////////////////////////////////////

	public float translationSpeed;
	private float _timeMoveObstacle;
	private float periodMoveObstacle;
	public int obstacleInitialX;
	public int obstacleInitialY;
	
	private GameObject gameObjGrid;
	private Grid grid;
	
	void Start()
	{
		gameObjGrid = GameObject.FindGameObjectWithTag ("Grid");
		grid = gameObjGrid.GetComponent< Grid > ();

		_timeMoveObstacle = 0.0f;
		periodMoveObstacle = 1.0f;
	}

	////////////////////////////////////////////////////
	// Getters.
	////////////////////////////////////////////////////
	

	
	////////////////////////////////////////////////////
	// Called once per frame of gameplay.
	////////////////////////////////////////////////////
	void Update ()
	{
		_timeMoveObstacle += Time.deltaTime;

		// Move game object to the left every frame.
		if (_timeMoveObstacle > periodMoveObstacle) {
			MoveObstacle();
			_timeMoveObstacle = 0;
		}

		Camera cam = ( Camera )GameObject.FindGameObjectWithTag( "Phase1Player1Camera" ).camera;
		if ( cam == null ) {
			return;
		}

		// Destroy game object if it moves off the left side of the viewable game area.
		//Vector3 screenSpacePosition = cam.WorldToScreenPoint( gameObject.transform.position );
		if ( obstacleInitialX < 0.0f ) {
			Destroy( gameObject );
		}
	}

	public void MoveObstacle(){
		grid.setObjectInGrid (obstacleInitialX, obstacleInitialY, -1);
		grid.setObjectInGrid (obstacleInitialX, 1, -1);
		obstacleInitialX--;
		grid.setObjectInGrid (obstacleInitialX, obstacleInitialY, 1);
		grid.setObjectInGrid (obstacleInitialX, 1, 1);
		transform.position = grid.computePlayerPosition (obstacleInitialX, obstacleInitialY);
	}
	
}