using UnityEngine;
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
	public GameObject grid;

	private Vector3 basePosition;
	////////////////////////////////////////////////////
	// Game object initialization.
	////////////////////////////////////////////////////
	void Start()
	{
		basePosition = new Vector3( 400.0f, 0.0f, 0.0f );

		Vector3 cameraPosition = new Vector3( basePosition.x, basePosition.y, basePosition.z - 10.0f );
		Vector3 backgroundPosition = basePosition;
		Vector3 playerPosition = new Vector3( computePosition(20), basePosition.y - 4.0f, basePosition.z - 1.0f );
		Vector3 obstacleManagerPosition = new Vector3( basePosition.x + 20.0f, basePosition.y, basePosition.z );
		Vector3 obstacleGenerator1Position = new Vector3( computePosition(29), basePosition.y - 4.3f, basePosition.z - 1.0f );
		Vector3 obstacleGenerator2Position = new Vector3( computePosition(29), basePosition.y - 2.5f, basePosition.z - 1.0f );

		Instantiate( grid, backgroundPosition, Quaternion.identity );

		Instantiate( camera, cameraPosition, transform.rotation );
		Instantiate( background, backgroundPosition, transform.rotation );
		GameObject objPlayer = Instantiate( player, playerPosition, transform.rotation ) as GameObject;
		PlayerOne playerScript =  objPlayer.GetComponent< PlayerOne > ();
		playerScript.playerInitialX = 15;
		playerScript.playerInitialY = 1;
		Instantiate( obstacleManager, obstacleManagerPosition, transform.rotation );
		GameObject lowCrate = Instantiate( obstacleGenerator, obstacleGenerator1Position, transform.rotation ) as GameObject;
		ObstacleGenerator lowCrateObj = lowCrate.GetComponent< ObstacleGenerator > ();
		lowCrateObj.height = 0;
		GameObject highCrate = Instantiate( obstacleGenerator, obstacleGenerator2Position, transform.rotation )as GameObject;
		ObstacleGenerator highCrateObj = highCrate.GetComponent< ObstacleGenerator > ();
		highCrateObj.height = 2;
	}

	public float computePosition(int x){
		return basePosition.x - 12 + 24.0f / 29.0f * x;
	}
}