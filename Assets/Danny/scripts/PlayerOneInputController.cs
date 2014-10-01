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
			player.jump();
		}
		else if ( Input.GetKeyDown( KeyCode.DownArrow ) ) {
			player.slide();
		}
		else if ( Input.GetKeyDown( KeyCode.RightArrow ) ) {
			player.sprint();
		}
	}
}