using UnityEngine;
using System.Collections;
using System.Linq;

[RequireComponent( typeof( SpriteRenderer ) )]
public class PlayerOne : MonoBehaviour
{
	////////////////////////////////////////////////////
	// Game object members.
	////////////////////////////////////////////////////



	// Player states.
	private bool isColliding;
	private bool isJumping;
	private bool isFalling;
	private bool isSliding;
	private bool isUping;
	private bool isSprinting;
	


	// Temporary variables used for development and testing.
	private int generateObstaclePeriod;
	private int generateObstaclePeriodMax = 9;
	private int generateObstaclePeriodMin = 6;
	private int jumpBeat = 0;
	private int slideBeat = 0;
	private int sprintBeat = 0;

	private int obsGenBeatCount = 0;
	private int keyGenBeatCount = 0;

	public int playerPositionDiscreteX;
	public int playerPositionDiscreteY;
	private GameObject gameObjGrid;
	private Grid grid;

	////////////////////////////////////////////////////
	// Game object initialization.
	////////////////////////////////////////////////////
	void Start()
	{
		gameObjGrid = GameObject.FindGameObjectWithTag ("Grid");
		grid = gameObjGrid.GetComponent< Grid > ();


		isColliding = false;
		isJumping = false;
		isFalling = false;
		isSprinting = false;
		isSliding = false;

		generateObstaclePeriod = (int)Random.Range (generateObstaclePeriodMin, generateObstaclePeriodMax);
	}


	////////////////////////////////////////////////////
	// Called once per frame of gameplay.
	////////////////////////////////////////////////////
	void Update()
	{

		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			trigger(0);
		}
		if (Input.GetKeyDown (KeyCode.Alpha3)) {
			trigger(1);
		}
		if (Input.GetKeyDown (KeyCode.Alpha4)) {
			trigger(2);
		}
		if (Input.GetKeyDown (KeyCode.Alpha5)) {
			trigger(3);
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





	public void trigger(int actionType){

		obsGenBeatCount++;
		keyGenBeatCount++;
		if(obsGenBeatCount > generateObstaclePeriod){
			generateObstaclePeriod = (int)Random.Range (generateObstaclePeriodMin, generateObstaclePeriodMax);
			obsGenBeatCount = 0;

			float randValue = Random.Range(0, 2);
			GameObject gameObjCrate;
			if(randValue < 1){
				gameObjCrate = GameObject.FindGameObjectWithTag("HighCrateGen");
			}
			else{
				gameObjCrate = GameObject.FindGameObjectWithTag("LowCrateGen");
			}
			ObstacleGenerator obsGen = gameObjCrate.GetComponent<ObstacleGenerator>();
			obsGen.CreateCrate();
		}
		if (keyGenBeatCount > 4) {
			keyGenBeatCount = 0;	
		}


		if (actionType == 1) {
			if(!isSliding && !isUping && !isFalling && !isJumping ){
				isJumping = true;
				jump();
			}	
		}

		if(actionType == 2) {
			if( !isSliding && !isUping && !isFalling && !isJumping ){
				isSliding = true;
				slide();
				
			}	
		}

		if(actionType == 3) {
			if( !isSliding && !isUping && !isFalling && !isJumping 
			   && grid.hasObstacle(playerPositionDiscreteX+1, playerPositionDiscreteY) == false){
				sprint();	
			}	
		}


		GameObject[] crates = GameObject.FindGameObjectsWithTag("Crate");
		foreach(GameObject obj in crates){
			TranslateLeftAtConstantSpeed crate = obj.GetComponent<TranslateLeftAtConstantSpeed>();
			crate.MoveObstacle();

			if(grid.hasObstacle(playerPositionDiscreteX, playerPositionDiscreteY) == true)
				pushBack();
		}

		if (isJumping) {
			jumpBeat++;	
		}
		if (jumpBeat >= 2 && grid.hasObstacle(playerPositionDiscreteX, 1) == false) {
			jumpBeat = 0;
			jumpFall();
			isJumping = false;
		}
		
		if (isSliding) {
			slideBeat++;	
		}
		if (slideBeat >= 2 && grid.hasObstacle(playerPositionDiscreteX, 1) == false) {
			slideBeat = 0;
			slideUp();
			isSliding = false;
		}

	}

	private void pushBack(){
		grid.setObjectInGrid (playerPositionDiscreteX, playerPositionDiscreteY, -1);

		playerPositionDiscreteX--;
		if (playerPositionDiscreteX < 0) {
			GameOver ();
			Destroy (gameObject);
		} 
		else {
			grid.setObjectInGrid (playerPositionDiscreteX, playerPositionDiscreteY, 0);
			transform.position = grid.computePlayerPosition (playerPositionDiscreteX, playerPositionDiscreteY);
		}

	}
	////////////////////////////////////////////////////
	// Player should jump.
	////////////////////////////////////////////////////
	private void jump(){
		grid.setObjectInGrid (playerPositionDiscreteX, playerPositionDiscreteY, -1);
		playerPositionDiscreteY++;
		grid.setObjectInGrid (playerPositionDiscreteX, playerPositionDiscreteY, 0);
		transform.position = grid.computePlayerPosition (playerPositionDiscreteX, playerPositionDiscreteY);

		//transform.Translate( new Vector3( 0.0f, computePositionY(1), 0.0f ) );
	}

	private void jumpFall(){
		grid.setObjectInGrid (playerPositionDiscreteX, playerPositionDiscreteY, -1);
		playerPositionDiscreteY--;
		grid.setObjectInGrid (playerPositionDiscreteX, playerPositionDiscreteY, 0);
		transform.position = grid.computePlayerPosition (playerPositionDiscreteX, playerPositionDiscreteY);
		//transform.Translate( new Vector3( 0.0f, -computePositionY(1), 0.0f ) );
	}

	////////////////////////////////////////////////////
	// Player should slide.
	////////////////////////////////////////////////////
	private void slide(){
		grid.setObjectInGrid (playerPositionDiscreteX, playerPositionDiscreteY, -1);
		playerPositionDiscreteY--;
		grid.setObjectInGrid (playerPositionDiscreteX, playerPositionDiscreteY, 0);
		transform.position = grid.computePlayerPosition (playerPositionDiscreteX, playerPositionDiscreteY);

	}

	private void slideUp(){
		grid.setObjectInGrid (playerPositionDiscreteX, playerPositionDiscreteY, -1);
		playerPositionDiscreteY++;
		grid.setObjectInGrid (playerPositionDiscreteX, playerPositionDiscreteY, 0);
		transform.position = grid.computePlayerPosition (playerPositionDiscreteX, playerPositionDiscreteY);

	}


	////////////////////////////////////////////////////
	// Player should sprint.
	////////////////////////////////////////////////////
	private void sprint(){
		grid.setObjectInGrid (playerPositionDiscreteX, playerPositionDiscreteY, -1);
		playerPositionDiscreteX++;
		grid.setObjectInGrid (playerPositionDiscreteX, playerPositionDiscreteY, 0);
		transform.position = grid.computePlayerPosition (playerPositionDiscreteX, playerPositionDiscreteY);
	}
	
}