using UnityEngine;
using System.Collections;

public class TranslateLeftAtConstantSpeed : MonoBehaviour
{
	////////////////////////////////////////////////////
	// Game object members.
	////////////////////////////////////////////////////

	public float translationSpeed;

	private Camera cam;


	////////////////////////////////////////////////////
	// Getters.
	////////////////////////////////////////////////////

	public float getTranslationSpeed()
	{
		return translationSpeed;
	}


	////////////////////////////////////////////////////
	// Game object initialization.
	////////////////////////////////////////////////////
	void Start()
	{
		cam = ( Camera )GameObject.Find( "phase1_player1_camera" ).camera;
	}

	
	////////////////////////////////////////////////////
	// Called once per frame of gameplay.
	////////////////////////////////////////////////////
	void Update ()
	{
		// Move game object to the left every frame.
		transform.Translate( new Vector3 ( translationSpeed * -1.0f * Time.deltaTime, 0.0f, 0.0f ) );

		// Destroy game object if it moves off the left side of the viewable game area.
		Vector3 screenSpacePosition = cam.WorldToScreenPoint( gameObject.transform.position );
		if ( screenSpacePosition.x < 0.0f ) {
			Destroy( gameObject );
		}
	}
}