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
	private int jumpBeat = 0;
	private int slideBeat = 0;
	private int sprintBeat = 0;

	public int playerInitialX;
	public int playerInitialY;
	private GameObject gameObjGrid;
	private Grid grid;

	////////////////////////////////////////////////////
	// Game object initialization.
	////////////////////////////////////////////////////
	void Start()
	{



		gameObjGrid = GameObject.FindGameObjectWithTag ("Grid");
		grid = gameObjGrid.GetComponent< Grid > ();

		playerInitialX = 15;
		playerInitialY = 1;
		gameObject.transform.position = new Vector3 (400, -4, -1);// grid.computePlayerPosition (playerInitialX, playerInitialY);

		isColliding = false;
		isJumping = false;
		isFalling = false;
		isSprinting = false;
		isSliding = false;

		playerSpeedHorizontal = 0.0f;//-idlePenalty;
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
			//UpdatePlayerState();
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
		if ( screenSpacePosition.x < -40.0f ) {
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
	// Method to determine horizontal and vertical speed of player,
	// and update player position accrodingly.
	////////////////////////////////////////////////////
	private void UpdatePlayerPosition()
	{
		// Determine player horizontal speed.
		if ( isColliding ) {
			//playerSpeedHorizontal = -getObstacleSpeed();
		}
		else {
			if ( isSliding ) {
				playerSpeedHorizontal = sprintSpeed / 4.0f;
			}
			else if ( isJumping || isFalling ) {
				playerSpeedHorizontal = sprintSpeed / 2.0f;
			}
			else if ( isSprinting ) {
				playerSpeedHorizontal = sprintSpeed;
			}
			else {
				playerSpeedHorizontal = -idlePenalty;
			}
		}
	}

	public void trigger(int actionType){


		if (actionType == 1) {
			if( !isSprinting && !isSliding && !isFalling && !isJumping ){
				isJumping = true;
				jump();

			}	
		}

		if(actionType == 2) {
			if( !isSprinting && !isSliding && !isFalling && !isJumping ){
				isSliding = true;
				slide();
				
			}	
		}

		if(actionType == 3) {
			if( !isSprinting && !isSliding && !isFalling && !isJumping ){
				isSprinting = true;
				sprint();	
			}	
		}


		if (isJumping) {
			jumpBeat++;	
		}
		if (jumpBeat >= 2 && isColliding == false) {
			jumpBeat = 0;
			jumpFall();
			isJumping = false;
		}

		if (isSliding) {
			slideBeat++;	
		}
		if (slideBeat >= 2 && isColliding == false) {
			slideBeat = 0;
			slideUp();
			isSliding = false;
		}

		if (isSprinting) {
			sprintBeat++;	
		}
		if (sprintBeat >= 2 && isColliding == false) {
			sprintBeat = 0;
			isSprinting = false;
		}

		GameObject[] crates = GameObject.FindGameObjectsWithTag("Crate");
		foreach(GameObject obj in crates){
			TranslateLeftAtConstantSpeed crate = obj.GetComponent<TranslateLeftAtConstantSpeed>();
			crate.MoveObstacle();

			if(grid.hasObstacle(playerInitialX, playerInitialY) == true)
				pushBack();
		}

	}

	public void pushBack(){
		grid.setObjectInGrid (playerInitialX, playerInitialY, -1);
		playerInitialX--;
		grid.setObjectInGrid (playerInitialX, playerInitialY, 0);
		transform.position = grid.computePlayerPosition (playerInitialX, playerInitialY);
	}
	////////////////////////////////////////////////////
	// Player should jump.
	////////////////////////////////////////////////////
	public void jump(){
		grid.setObjectInGrid (playerInitialX, playerInitialY, -1);
		playerInitialY++;
		grid.setObjectInGrid (playerInitialX, playerInitialY, 0);
		transform.position = grid.computePlayerPosition (playerInitialX, playerInitialY);

		//transform.Translate( new Vector3( 0.0f, computePositionY(1), 0.0f ) );
	}

	public void jumpFall(){
		grid.setObjectInGrid (playerInitialX, playerInitialY, -1);
		playerInitialY--;
		grid.setObjectInGrid (playerInitialX, playerInitialY, 0);
		transform.position = grid.computePlayerPosition (playerInitialX, playerInitialY);
		//transform.Translate( new Vector3( 0.0f, -computePositionY(1), 0.0f ) );
	}

	////////////////////////////////////////////////////
	// Player should slide.
	////////////////////////////////////////////////////
	public void slide(){
		grid.setObjectInGrid (playerInitialX, playerInitialY, -1);
		playerInitialY--;
		grid.setObjectInGrid (playerInitialX, playerInitialY, 0);
		transform.position = grid.computePlayerPosition (playerInitialX, playerInitialY);
		//transform.Translate( new Vector3 ( 0.0f, -computePositionY(1), 0.0f ) );
	}

	public void slideUp(){
		grid.setObjectInGrid (playerInitialX, playerInitialY, -1);
		playerInitialY++;
		grid.setObjectInGrid (playerInitialX, playerInitialY, 0);
		transform.position = grid.computePlayerPosition (playerInitialX, playerInitialY);
		//transform.Translate( new Vector3 ( 0.0f, computePositionY(1), 0.0f ) );
	}


	////////////////////////////////////////////////////
	// Player should sprint.
	////////////////////////////////////////////////////
	public void sprint(){

		transform.Translate( new Vector3( computePositionX(1), 0.0f, 0.0f ) );

	}

	public float computePositionX(int x){
		return 24.0f / 29.0f * x;
	}

	public float computePositionY(int y){
		return 24.0f / 29.0f * y;
	}
}