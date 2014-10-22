using UnityEngine;
using System.Collections;
using System.Linq;

[RequireComponent( typeof( SpriteRenderer ) )]
public class PlayerOne : MonoBehaviour
{
	////////////////////////////////////////////////////
	// Game object members.
	////////////////////////////////////////////////////




	private GameObject gameObjPhase1;

	// Player states.
	private bool isColliding;
	private bool isJumping;
	private bool isFalling;
	private bool isSliding;
	private bool isUping;
	private bool isSprinting;
	


	// Temporary variables used for development and testing.
	private int jumpBeat = 0;
	private int slideBeat = 0;
	private int sprintBeat = 0;

	public int playerPositionDiscreteX;
	public int playerPositionDiscreteY;
	private GameObject gameObjGrid;
	private Grid grid;
	private Phase1 phase1;
	private GlobalScript global;

	// Danny was here.
	private int num_beats_between_obstacle_movement;
	private int num_beats_since_last_obstacle_movement;
	private int num_beats_between_obstacle_movement_max;

	// Danny was here.
	private int num_beats_between_obstacle_generation_min;
	private int num_beats_between_obstacle_generation_max;
	private int current_num_beats_between_obstacle_generation;
	private int num_beats_since_last_obstacle_generation;

	// Danny was here.
	private int num_beats_between_key_generation_min;
	private int num_beats_between_key_generation_max;
	private int current_num_beats_between_key_generation;
	private int num_beats_since_last_key_generation;

	////////////////////////////////////////////////////
	// Game object initialization.
	////////////////////////////////////////////////////
	void Start()
	{
		gameObjPhase1 = GameObject.Find("Phase1");
		phase1 = gameObjPhase1.GetComponent< Phase1 > ();
		global = gameObjPhase1.GetComponent<GlobalScript> ();

		gameObjGrid = GameObject.FindGameObjectWithTag ("Grid");
		grid = gameObjGrid.GetComponent< Grid > ();
		
		isColliding = false;
		isJumping = false;
		isFalling = false;
		isSprinting = false;
		isSliding = false;

		// Danny was here.
		num_beats_between_obstacle_movement = 1;
		num_beats_since_last_obstacle_movement = 0;
		num_beats_between_obstacle_movement_max = 3;

		// Danny was here.
		num_beats_between_obstacle_generation_min = 18;
		num_beats_between_obstacle_generation_max = 27;
		current_num_beats_between_obstacle_generation = getWaitTimeUntilNextObstacle();
		num_beats_since_last_obstacle_generation = 0;

		// Danny was here.
		num_beats_between_key_generation_min = 36;
		num_beats_between_key_generation_max = 51;
		current_num_beats_between_key_generation = getWaitTimeUntilNextKey();
		num_beats_since_last_key_generation = 0;
	}


	// Danny was here.
	// Generate new number of beats to wait until the next obstacle is generated.
	private int getWaitTimeUntilNextObstacle()
	{
		return ( int )Random.Range( num_beats_between_obstacle_generation_min, num_beats_between_obstacle_generation_max );
	}

	// Danny was here.
	// Generate new number of beats to wait until the next key is generated.
	private int getWaitTimeUntilNextKey()
	{
		return ( int )Random.Range( num_beats_between_key_generation_min, num_beats_between_key_generation_max );
	}

	// Danny was here.
	public void increaseObstacleSpeed()
	{
		if ( num_beats_between_obstacle_movement > 1 ) {
			--num_beats_between_obstacle_movement;
		}
	}

	// Danny was here.
	public void decreaseObstacleSpeed()
	{
		if ( num_beats_between_obstacle_movement < num_beats_between_obstacle_movement_max ) {
			++num_beats_between_obstacle_movement;
		}
	}

	// Danny was here.
	public void increaseRateOfObstacleGeneration()
	{
		if ( num_beats_between_obstacle_generation_max > num_beats_between_obstacle_generation_min ) {
			num_beats_between_obstacle_generation_max += 4;
		}
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

		// Danny was here.
		++num_beats_since_last_obstacle_movement;
		++num_beats_since_last_obstacle_generation;
		++num_beats_since_last_key_generation;

		// Danny was here.
		if ( num_beats_since_last_obstacle_generation > current_num_beats_between_obstacle_generation ) {
			num_beats_since_last_obstacle_generation = 0;
			current_num_beats_between_obstacle_generation = getWaitTimeUntilNextObstacle();
			
			float randValue = Random.Range( 0, 2 );
			GameObject gameObjCrate;
			if ( randValue < 1 ){
				gameObjCrate = GameObject.FindGameObjectWithTag( "HighCrateGen" );
			}
			else {
				gameObjCrate = GameObject.FindGameObjectWithTag( "LowCrateGen" );
			}
			ObstacleGenerator obsGen = gameObjCrate.GetComponent<ObstacleGenerator>();
			obsGen.CreateCrate();
		}

		// Danny was here.
		if ( num_beats_since_last_key_generation > current_num_beats_between_key_generation ) {
			int grid_width = grid.getWidth();
			if( grid.hasObstacle( grid_width - 1, 2 ) != true ) {
				num_beats_since_last_key_generation = 0;
				current_num_beats_between_key_generation = getWaitTimeUntilNextKey();

				GameObject gameObjKeyGen = GameObject.FindGameObjectWithTag( "KeyGen" );
				KeyGenerate keyGen = gameObjKeyGen.GetComponent<KeyGenerate>();
				keyGen.createKey();
			}
		}

		//Action: Sprint
		if(actionType == 1) {
			if( !isSliding && !isUping && !isFalling && !isJumping 
			   && grid.hasObstacle(playerPositionDiscreteX+1, playerPositionDiscreteY) == false){
				sprint();	
			}	
		}

		//Action: Jump
		if (actionType == 2) {
			if(!isSliding && !isUping && !isFalling && !isJumping ){
				isJumping = true;
				jump();
			}	
		}

		//Action: Slide
		if(actionType == 3) {
			if( !isSliding && !isUping && !isFalling && !isJumping ){
				isSliding = true;
				slide();
				
			}	
		}

		GameObject objKeyBlue = GameObject.FindGameObjectWithTag("BlueKey");
		GameObject objKeyYellow = GameObject.FindGameObjectWithTag("YellowKey");
		GameObject objKeyRed = GameObject.FindGameObjectWithTag("RedKey");
		GameObject objKeyGreen = GameObject.FindGameObjectWithTag("GreenKey");
		GameObject objKeyOrange = GameObject.FindGameObjectWithTag("OrangeKey");

		// Danny was here.
		if ( num_beats_since_last_obstacle_movement > num_beats_between_obstacle_movement ) {
			num_beats_since_last_obstacle_movement = 0;

			//Move all the keys in the current scene
			if (objKeyBlue != null) {
				Key keyBlue = objKeyBlue.GetComponent<Key>();
				keyBlue.MoveKey();
			}

			if (objKeyYellow != null) {
				Key keyYellow = objKeyYellow.GetComponent<Key>();
				keyYellow.MoveKey();
			}

			if (objKeyRed != null) {
				Key keyRed = objKeyRed.GetComponent<Key>();
				keyRed.MoveKey();
			}

			if (objKeyGreen != null) {
				Key keyGreen = objKeyGreen.GetComponent<Key>();
				keyGreen.MoveKey();
			}

			if (objKeyOrange != null) {
				Key keyOrange = objKeyOrange.GetComponent<Key>();
				keyOrange.MoveKey();
			}
			
			//Move all the crates every beat
			GameObject[] crates = GameObject.FindGameObjectsWithTag("Crate");
			foreach(GameObject obj in crates){
				TranslateLeftAtConstantSpeed crate = obj.GetComponent<TranslateLeftAtConstantSpeed>();
				crate.MoveObstacle();
				
				if(grid.hasObstacle(playerPositionDiscreteX, playerPositionDiscreteY) == true)
					pushBack();
			}
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

		//If player collide with keys, then collect the key
		int keyStatus = grid.hasKeys (playerPositionDiscreteX, playerPositionDiscreteY);
		switch (keyStatus) {
		case 31:
			Key keyBlue = objKeyBlue.GetComponent<Key>();
			keyBlue.Pick();
			global.holdKeyStatus = keyStatus;
			break;
		case 32:
			Key keyYellow = objKeyYellow.GetComponent<Key>();
			keyYellow.Pick();
			global.holdKeyStatus = keyStatus;
			break;
		case 33:
			Key keyRed = objKeyRed.GetComponent<Key>();
			keyRed.Pick();
			global.holdKeyStatus = keyStatus;
			break;
		case 34:
			Key keyGreen = objKeyGreen.GetComponent<Key>();
			keyGreen.Pick();
			global.holdKeyStatus = keyStatus;
			break;
		case 35:
			Key keyOrange = objKeyOrange.GetComponent<Key>();
			keyOrange.Pick();
			global.holdKeyStatus = keyStatus;
			break;
		}

	}

	private void pushBack(){
		//grid.setObjectInGrid (playerPositionDiscreteX, playerPositionDiscreteY, -1);

		playerPositionDiscreteX--;
		if (playerPositionDiscreteX < 0) {
			GameOver ();
			Destroy (gameObject);
		} 
		else {
			//grid.setObjectInGrid (playerPositionDiscreteX, playerPositionDiscreteY, 0);
			transform.position = grid.computePlayerPosition (playerPositionDiscreteX, playerPositionDiscreteY);
		}

	}
	////////////////////////////////////////////////////
	// Player should jump.
	////////////////////////////////////////////////////
	private void jump(){
		//grid.setObjectInGrid (playerPositionDiscreteX, playerPositionDiscreteY, -1);
		playerPositionDiscreteY++;
		//grid.setObjectInGrid (playerPositionDiscreteX, playerPositionDiscreteY, 0);
		transform.position = grid.computePlayerPosition (playerPositionDiscreteX, playerPositionDiscreteY);
	}

	private void jumpFall(){
		//grid.setObjectInGrid (playerPositionDiscreteX, playerPositionDiscreteY, -1);
		playerPositionDiscreteY--;
		//grid.setObjectInGrid (playerPositionDiscreteX, playerPositionDiscreteY, 0);
		transform.position = grid.computePlayerPosition (playerPositionDiscreteX, playerPositionDiscreteY);
	}

	////////////////////////////////////////////////////
	// Player should slide.
	////////////////////////////////////////////////////
	private void slide(){
		//grid.setObjectInGrid (playerPositionDiscreteX, playerPositionDiscreteY, -1);
		playerPositionDiscreteY--;
		//grid.setObjectInGrid (playerPositionDiscreteX, playerPositionDiscreteY, 0);
		transform.position = grid.computePlayerPosition (playerPositionDiscreteX, playerPositionDiscreteY);

	}

	private void slideUp(){
		//grid.setObjectInGrid (playerPositionDiscreteX, playerPositionDiscreteY, -1);
		playerPositionDiscreteY++;
		//grid.setObjectInGrid (playerPositionDiscreteX, playerPositionDiscreteY, 0);
		transform.position = grid.computePlayerPosition (playerPositionDiscreteX, playerPositionDiscreteY);

	}


	////////////////////////////////////////////////////
	// Player should sprint.
	////////////////////////////////////////////////////
	private void sprint(){
		//grid.setObjectInGrid (playerPositionDiscreteX, playerPositionDiscreteY, -1);
		playerPositionDiscreteX++;
		//grid.setObjectInGrid (playerPositionDiscreteX, playerPositionDiscreteY, 0);
		transform.position = grid.computePlayerPosition (playerPositionDiscreteX, playerPositionDiscreteY);
	}
	
}