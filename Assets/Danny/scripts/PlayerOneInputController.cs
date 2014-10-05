using UnityEngine;
using System.Collections;

public class PlayerOneInputController : MonoBehaviour
{
	////////////////////////////////////////////////////
	// Game object members.
	////////////////////////////////////////////////////

	private PlayerOne player;


	////////////////////////////////////////////////////
	// Game object initialization.
	////////////////////////////////////////////////////
	void Start()
	{
		player = ( PlayerOne )FindObjectOfType( typeof( PlayerOne ) );
	}


	////////////////////////////////////////////////////
	// Called once per frame of gameplay.
	////////////////////////////////////////////////////
	void Update()
	{
		if ( Input.GetKeyDown( KeyCode.UpArrow ) ) {
			Debug.Log( "Player 1 jump." );
			player.jump();
		}
		else if ( Input.GetKeyDown( KeyCode.DownArrow ) ) {
			Debug.Log( "Player 1 slide." );
			player.slide();
		}
		else if ( Input.GetKeyDown( KeyCode.RightArrow ) ) {
			Debug.Log( "Player 1 sprint." );
			player.sprint();
		}
	}
}