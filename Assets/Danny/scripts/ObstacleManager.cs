using UnityEngine;
using System.Collections;

public class ObstacleManager : MonoBehaviour
{
//	////////////////////////////////////////////////////
//	// Game object members.
//	////////////////////////////////////////////////////
//
//	public float secondsBetweenObstacles;
//	public float timeUntilFirstObject;
//
//	private float _timeSinceLastObstacle;
//	private float _timeSinceGameStarted;
//	//private float _timeMoveObstacle;
//	//private float periodMoveObstacle;
//
//	//private int initialX;
//
//	private GameObject gameObjGrid;
//	private Grid grid;
//
//	////////////////////////////////////////////////////
//	// Game object initialization.
//	////////////////////////////////////////////////////
//	void Start()
//	{
//		gameObjGrid = GameObject.FindGameObjectWithTag ("Grid");
//		grid = gameObjGrid.GetComponent< Grid > ();
//
//
//	}
//
//
//	////////////////////////////////////////////////////
//	// Called once per frame of gameplay.
//	////////////////////////////////////////////////////
//	void Update()
//	{
//
//		if (Input.GetKeyDown (KeyCode.Alpha1))
//			CreateCrate();
//	}
//
//
//	////////////////////////////////////////////////////
//	// Randomly select one of the obstacle generators in the scene,
//	// and create a new crate obstacle at that obstacle generator.
//	////////////////////////////////////////////////////
//	private void CreateCrate()
//	{
//		/*var obstacleGeneratorList = FindObjectsOfType( typeof( ObstacleGenerator ) );
//		if ( obstacleGeneratorList.Length == 0 ) {
//			return;
//		}
//		var anObstacleGenerator = ( ObstacleGenerator )obstacleGeneratorList[Random.Range( 0, obstacleGeneratorList.Length )];
//
//		anObstacleGenerator.CreateCrate();*/
//	}
}