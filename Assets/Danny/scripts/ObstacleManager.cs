using UnityEngine;
using System.Collections;

public class ObstacleManager : MonoBehaviour
{
	////////////////////////////////////////////////////
	// Game object members.
	////////////////////////////////////////////////////

	public float secondsBetweenObstacles;

	private float _timeSinceLastObstacle;


	////////////////////////////////////////////////////
	// Game object initialization.
	////////////////////////////////////////////////////
	void Start()
	{
		_timeSinceLastObstacle = 0.0f;
	}


	////////////////////////////////////////////////////
	// Called once per frame of gameplay.
	////////////////////////////////////////////////////
	void Update()
	{
		_timeSinceLastObstacle += Time.deltaTime;

		if ( _timeSinceLastObstacle >= secondsBetweenObstacles ) {
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