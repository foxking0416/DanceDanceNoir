﻿using UnityEngine;
using System.Collections;

public class ObstacleManager : MonoBehaviour
{
	////////////////////////////////////////////////////
	// Game object members.
	////////////////////////////////////////////////////

	public float secondsBetweenObstacles;
	public float timeUntilFirstObject;

	private float _timeSinceLastObstacle;
	private float _timeSinceGameStarted;
	//private float _timeMoveObstacle;
	//private float periodMoveObstacle;

	//private int initialX;

	private GameObject gameObjGrid;
	private Grid grid;

	////////////////////////////////////////////////////
	// Game object initialization.
	////////////////////////////////////////////////////
	void Start()
	{
		gameObjGrid = GameObject.FindGameObjectWithTag ("Grid");
		grid = gameObjGrid.GetComponent< Grid > ();

		_timeSinceLastObstacle = 0.0f;
		_timeSinceGameStarted = 0.0f;
		//_timeMoveObstacle = 0.0f;
		//periodMoveObstacle = 1.0f;
		//initialX = 29;
	}


	////////////////////////////////////////////////////
	// Called once per frame of gameplay.
	////////////////////////////////////////////////////
	void Update()
	{
		_timeSinceLastObstacle += Time.deltaTime;

		if ( _timeSinceGameStarted < timeUntilFirstObject ) {
			_timeSinceGameStarted += Time.deltaTime;
		}

		if ( ( _timeSinceLastObstacle >= secondsBetweenObstacles ) &&
		     ( _timeSinceGameStarted >= timeUntilFirstObject ) ) {
			_timeSinceLastObstacle = 0.0f;
			CreateCrate();
		}

	}


	////////////////////////////////////////////////////
	// Randomly select one of the obstacle generators in the scene,
	// and create a new crate obstacle at that obstacle generator.
	////////////////////////////////////////////////////
	private void CreateCrate()
	{
		var obstacleGeneratorList = FindObjectsOfType( typeof( ObstacleGenerator ) );
		if ( obstacleGeneratorList.Length == 0 ) {
			return;
		}
		var anObstacleGenerator = ( ObstacleGenerator )obstacleGeneratorList[Random.Range( 0, obstacleGeneratorList.Length )];

		anObstacleGenerator.CreateCrate();
	}
}