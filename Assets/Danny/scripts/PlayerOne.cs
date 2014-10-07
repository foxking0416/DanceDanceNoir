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

	// Player states.
	private bool isColliding;
	private bool isJumping;
	private bool isFalling;
	private bool isSliding;
	private bool isSprinting;

	private float playerSpeedHorizontal;
	private float playerSpeedVertical;

	private float verticalRestPosition;
	private float timeSliding;
	private float timeSprinting;

	// Temporary variables used for development and testing.
	private float slideTranslation = 1.0f;
	private float jumpingStep = 4.0f;


	////////////////////////////////////////////////////
	// Game object initialization.
	////////////////////////////////////////////////////
	void Start()
	{
		isColliding = false;
		isJumping = false;
		isFalling = false;
		isSprinting = false;
		isSliding = false;

		playerSpeedHorizontal = -idlePenalty;
		playerSpeedVertical = 0.0f;

		verticalRestPosition = transform.position.y;
		timeSliding = 0.0f;
		timeSprinting = 0.0f;
	}


	////////////////////////////////////////////////////
	// Called once per frame of gameplay.
	////////////////////////////////////////////////////
	void Update()
	{
		if ( IsGameOver() ) {
			GameOver();
		}
		else {
			UpdatePlayerState();
			UpdatePlayerPosition();
		}
	}


	////////////////////////////////////////////////////
	// Method to check if losing condition has been met.
	// Player one loses if character is pushed off left side of screen.
	////////////////////////////////////////////////////
	private bool IsGameOver()
	{
		Camera cam = ( Camera )GameObject.FindGameObjectWithTag( "Phase1Player1Camera" ).camera;
		if ( cam == null ) {
			return false;
		}
		
		Vector3 screenSpacePosition = cam.WorldToScreenPoint( gameObject.transform.position );
		if ( screenSpacePosition.x < -12.0f ) {
			return true;
		}
		else {
			return false;
		}
	}


	////////////////////////////////////////////////////
	// Method to handle actions that occur when player one has lost the game.
	////////////////////////////////////////////////////
	private void GameOver()
	{
		// TODO: Implement this method.
		Debug.Log( "Game over player one." );
	}


	////////////////////////////////////////////////////
	// Method to test when collisions between player and obstacles begin.
	////////////////////////////////////////////////////
	void OnTriggerEnter2D( Collider2D otherCollider ) {
		TranslateLeftAtConstantSpeed crate = otherCollider.gameObject.GetComponent<TranslateLeftAtConstantSpeed>();
		if ( crate != null ) {
			isColliding = true;
		}
		else {
			isColliding = false;
		}
	}


	////////////////////////////////////////////////////
	// Method to test when collisions between player and obstacles end.
	////////////////////////////////////////////////////
	void OnTriggerExit2D( Collider2D otherCollider ) {
		TranslateLeftAtConstantSpeed crate = otherCollider.gameObject.GetComponent<TranslateLeftAtConstantSpeed>();
		if ( crate != null ) {
			isColliding = false;
		}
		else {
			isColliding = true;
		}
	}


	////////////////////////////////////////////////////
	// Method to determine speed of incoming obstacles.
	////////////////////////////////////////////////////
	private float getObstacleSpeed()
	{
		var crate = ( TranslateLeftAtConstantSpeed )FindObjectOfType( typeof( TranslateLeftAtConstantSpeed ) );
		if ( crate != null ) {
			return crate.getTranslationSpeed();
		}
		else {
			return 0.0f;
		}
	}


	////////////////////////////////////////////////////
	// Method to change player state based on game conditions.
	////////////////////////////////////////////////////
	private void UpdatePlayerState()
	{
		if ( isJumping ) {
			if ( transform.position.y >= ( verticalRestPosition + jumpHeight ) ) {
				isJumping = false;
				isFalling = true;
			}
		}
		else if ( isFalling ) {
			if ( transform.position.y <= verticalRestPosition ) {
				isFalling = false;
			}
		}
		else if ( isSliding ) {
			if ( timeSliding >= slideDuration ) {
				isSliding = false;
				
				// Undo the translation performed for sliding visualization.
				transform.Translate( new Vector3 ( 0.0f, slideTranslation, 0.0f ) );
			}
			timeSliding += Time.deltaTime;
		}
		else if ( isSprinting ) {
			if ( timeSprinting >= sprintDuration ) {
				isSprinting = false;
			}
			timeSprinting += Time.deltaTime;
		}
	}


	////////////////////////////////////////////////////
	// Method to determine horizontal and vertical speed of player,
	// and update player position accrodingly.
	////////////////////////////////////////////////////
	private void UpdatePlayerPosition()
	{
		// Determine player horizontal speed.
		if ( isColliding ) {
			playerSpeedHorizontal = -getObstacleSpeed();
		}
		else {
			if ( isJumping || isFalling || isSliding ) {
				playerSpeedHorizontal = 0.0f;
			}
			else if ( isSprinting ) {
				playerSpeedHorizontal = sprintSpeed;
			}
			else {
				playerSpeedHorizontal = -idlePenalty;
			}
		}
		
		// Determine player vertical speed.
		if ( isJumping ) {
			playerSpeedVertical = jumpingStep;
		}
		else if ( isFalling ) {
			playerSpeedVertical = -jumpingStep;
		}
		else {
			playerSpeedVertical = 0.0f;
		}
		
		// Update player position.
		transform.Translate( new Vector3( playerSpeedHorizontal * Time.deltaTime, 0.0f, 0.0f ) );
		transform.Translate( new Vector3( 0.0f, playerSpeedVertical * Time.deltaTime, 0.0f ) );
	}


	////////////////////////////////////////////////////
	// Player should jump.
	////////////////////////////////////////////////////
	public void jump()
	{
		if ( !isSprinting && !isSliding && !isFalling ) {
			isJumping = true;
		}
	}


	////////////////////////////////////////////////////
	// Player should slide.
	////////////////////////////////////////////////////
	public void slide()
	{
		if ( !isSprinting && !isJumping && !isFalling ) {
			isSliding = true;
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
		if ( !isJumping && !isSliding && !isFalling && !isColliding ) {
			isSprinting = true;
			timeSprinting = 0.0f;
		}
	}
}