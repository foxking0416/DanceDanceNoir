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
		Vector3 cameraPosition = new Vector3( 200.0f, 0.0f, -10.0f );
		Vector3 backgroundPosition = new Vector3( 200.0f, 0.0f, 0.0f );
		Vector3 playerPosition = new Vector3( 200.0f, -4.0f, -1.0f );
		Vector3 obstacleManagerPosition = new Vector3( 220.0f, 0.0f, 0.0f );
		Vector3 obstacleGenerator1Position = new Vector3( 210.0f, -4.3f, -1.0f );
		Vector3 obstacleGenerator2Position = new Vector3( 210.0f, -2.5f, -1.0f );

		Instantiate( camera, cameraPosition, transform.rotation );
		Instantiate( background, backgroundPosition, transform.rotation );
		Instantiate( player, playerPosition, transform.rotation );
		Instantiate( obstacleManager, obstacleManagerPosition, transform.rotation );
		Instantiate( obstacleGenerator, obstacleGenerator1Position, transform.rotation );
		Instantiate( obstacleGenerator, obstacleGenerator2Position, transform.rotation );
	}
}