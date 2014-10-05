using UnityEngine;
using System.Collections;
using System.Linq;

[RequireComponent( typeof( SpriteRenderer ) )]
public class PlayerOne : MonoBehaviour
{
	////////////////////////////////////////////////////
	// Game object members.
	////////////////////////////////////////////////////

	public float idlePenalty;
	public float sprintSpeed;
	public float sprintDuration;
	public float jumpHeight;
	public float slideDuration;

	private bool isIdle;

	private bool isJumping;
	private bool isFalling;
	private float verticalRestPosition;

	private bool isSliding;
	private float timeSliding;

	private bool isSprinting;
	private float timeSprinting;

	private Camera cam;
	private SpriteRenderer spriteRenderer;

	// Temporary variables used for development and testing.
	private float slideTranslation = 1.0f;
	private float jumpingStep = 0.05f;


	////////////////////////////////////////////////////
	// Game object initialization.
	////////////////////////////////////////////////////
	void Start()
	{
		isIdle = true;
		isSprinting = false;
		isJumping = false;
		isSliding = false;
		isFalling = false;

		verticalRestPosition = transform.position.y;

		cam = ( Camera )GameObject.Find( "phase1_player1_camera" ).camera;
		spriteRenderer = ( SpriteRenderer )gameObject.GetComponentInChildren<SpriteRenderer>();
	}


	////////////////////////////////////////////////////
	// Called once per frame of gameplay.
	////////////////////////////////////////////////////
	void Update()
	{
		// Check if losing condition has been met.
		Vector3 screenSpacePosition = cam.WorldToScreenPoint( gameObject.transform.position );
		if ( screenSpacePosition.x < -12.0f ) {
			Debug.Log( "GAME OVER!" );
		}

		// Player is not performing any actions.
		if ( isIdle ) {
			transform.Translate( new Vector3 ( idlePenalty * -1.0f * Time.deltaTime, 0.0f, 0.0f ) );
		}

		// Player is sliding.
		if ( isSliding ) {
			if ( timeSliding >= slideDuration ) {
				isSliding = false;
				isIdle = true;

				// Undo the translation performed for sliding visualization.
				transform.Translate( new Vector3 ( 0.0f, slideTranslation, 0.0f ) );
			}
			timeSliding += Time.deltaTime;
		}

		// Player is jumping.
		if ( isJumping ) {
			if ( transform.position.y < ( verticalRestPosition + jumpHeight ) ) {
				transform.Translate( new Vector3 ( 0.0f, jumpingStep, 0.0f ) );
			}
			else {
				isJumping = false;
				isFalling = true;
			}
		}

		// Player's jump has peaked, and their character is now falling back to the ground plane.
		if ( isFalling ) {
			if ( transform.position.y > verticalRestPosition ) {
				transform.Translate( new Vector3 ( 0.0f, -jumpingStep, 0.0f ) );
			}
			else {
				isFalling = false;
				isIdle = true;
			}
		}

		// Player is running to the right side of the screen.
		if ( isSprinting ) {
			if ( timeSprinting >= sprintDuration ) {
				isSprinting = false;
				isIdle = true;
			}
			transform.Translate( new Vector3 ( sprintSpeed * Time.deltaTime, 0.0f, 0.0f ) );
			timeSprinting += Time.deltaTime;
		}

		// Collision detection.
		//TestForCollisions();
	}


	////////////////////////////////////////////////////
	// Player should jump.
	////////////////////////////////////////////////////
	public void jump()
	{
		if ( !isSprinting && !isSliding && !isFalling ) {
			isJumping = true;
			isIdle = false;
		}
	}


	////////////////////////////////////////////////////
	// Player should slide.
	////////////////////////////////////////////////////
	public void slide()
	{
		if ( !isSprinting && !isJumping && !isFalling ) {
			isSliding = true;
			isIdle = false;

			timeSliding = 0.0f;

			// Translate player sprite down to temporarily visualize sliding.
			transform.Translate( new Vector3 ( 0.0f, -slideTranslation, 0.0f ) );
		}
	}


	////////////////////////////////////////////////////
	// Player should sprint.
	////////////////////////////////////////////////////
	public void sprint()
	{
		if ( !isJumping && !isSliding && !isFalling ) {
			isSprinting = true;
			isIdle = false;

			timeSprinting = 0.0f;
		}
	}


//	// Collision detection.
//	private void TestForCollisions()
//	{
//		// TODO: Get player sprite renderer bounds.
//		SpriteRenderer thisSprite = spriteRenderer;
//		
//		// TODO: Get nearest crate sprite renderer bounds.
//		var crates =
//			from crateList in FindObjectsOfType( typeof( TranslateLeftAtConstantSpeed ) )
//				let aCrate = ( TranslateLeftAtConstantSpeed )crateList
//				let thatSprite = ( SpriteRenderer )aCrate.GetComponentInChildren<SpriteRenderer>()
//				where thisSprite.bounds.Intersects( thatSprite.bounds )
//				select aCrate;
//
//		var collisionObject = crates.FirstOrDefault();
//		if ( collisionObject != null ) {
//			SpriteRenderer crateSprite = ( SpriteRenderer )collisionObject.GetComponentInChildren<SpriteRenderer>();
//			Debug.Log( "Collision" );
//			Debug.Log( thisSprite.bounds );
//			Debug.Log( crateSprite.bounds );
//		}
//	}

	void OnTriggerEnter2D( Collider2D otherCollider ) {
		TranslateLeftAtConstantSpeed crate = otherCollider.gameObject.GetComponent<TranslateLeftAtConstantSpeed>();
		if ( crate != null ) {
			Debug.Log( "Collision!" );
		}
	}
}