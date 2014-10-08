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


	////////////////////////////////////////////////////
	// Game object initialization.
	////////////////////////////////////////////////////
	void Start()
	{
		Vector3 basePosition = new Vector3( 400.0f, 0.0f, 0.0f );

		Vector3 cameraPosition = new Vector3( basePosition.x, basePosition.y, basePosition.z - 10.0f );
		Vector3 backgroundPosition = basePosition;
		Vector3 playerPosition = new Vector3( basePosition.x, basePosition.y - 4.0f, basePosition.z - 1.0f );
		Vector3 obstacleManagerPosition = new Vector3( basePosition.x + 20.0f, basePosition.y, basePosition.z );
		Vector3 obstacleGenerator1Position = new Vector3( basePosition.x + 10.0f, basePosition.y - 4.3f, basePosition.z - 1.0f );
		Vector3 obstacleGenerator2Position = new Vector3( basePosition.x + 10.0f, basePosition.y - 2.5f, basePosition.z - 1.0f );

		Instantiate( camera, cameraPosition, transform.rotation );
		Instantiate( background, backgroundPosition, transform.rotation );
		Instantiate( player, playerPosition, transform.rotation );
		//Instantiate( obstacleManager, obstacleManagerPosition, transform.rotation );
		Instantiate( obstacleGenerator, obstacleGenerator1Position, transform.rotation );
		Instantiate( obstacleGenerator, obstacleGenerator2Position, transform.rotation );
	}
}