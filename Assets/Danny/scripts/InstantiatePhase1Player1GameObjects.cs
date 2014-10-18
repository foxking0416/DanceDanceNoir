﻿using UnityEngine;
using System.Collections;

public class InstantiatePhase1Player1GameObjects : MonoBehaviour
{
	////////////////////////////////////////////////////
	// Game object members.
	////////////////////////////////////////////////////

	public GameObject camera;
	public GameObject background;
	public GameObject player;
	public GameObject obstacleManager;
	public GameObject obstacleGenerator;
	public GameObject gameObjGrid;

	private Vector3 basePosition;
	////////////////////////////////////////////////////
	// Game object initialization.
	////////////////////////////////////////////////////
	void Start()
	{
		basePosition = new Vector3( 400.0f, 0.0f, 0.0f );
		GameObject gridObj = Instantiate( gameObjGrid, new Vector3(0,0,0), Quaternion.identity )as GameObject;
		Grid grid = gridObj.GetComponent<Grid> ();


		Vector3 cameraPosition = new Vector3( basePosition.x, basePosition.y, basePosition.z - 10.0f );
		Vector3 backgroundPosition = basePosition;



		int initialPlayerX = 15;
		int initialPlayerY = 1;

		Instantiate( camera, cameraPosition, transform.rotation );
		Instantiate( background, backgroundPosition, transform.rotation );
		GameObject objPlayer = Instantiate( player, grid.computePlayerPosition(initialPlayerX, initialPlayerY), transform.rotation ) as GameObject;
		PlayerOne playerScript =  objPlayer.GetComponent< PlayerOne > ();
		playerScript.playerPositionDiscreteX = initialPlayerX;
		playerScript.playerPositionDiscreteY = initialPlayerY;
		Instantiate( obstacleManager, new Vector3(0,0,0), transform.rotation );
		GameObject lowCrate = Instantiate( obstacleGenerator, new Vector3(0,0,0), transform.rotation ) as GameObject;
		lowCrate.tag = "LowCrateGen";
		ObstacleGenerator lowCrateObj = lowCrate.GetComponent< ObstacleGenerator > ();
		lowCrateObj.height = 0;
		GameObject highCrate = Instantiate( obstacleGenerator, new Vector3(0,0,0), transform.rotation )as GameObject;
		highCrate.tag = "HighCrateGen";
		ObstacleGenerator highCrateObj = highCrate.GetComponent< ObstacleGenerator > ();
		highCrateObj.height = 2;
	}


}