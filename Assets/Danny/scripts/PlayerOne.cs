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

	private int min_lane;
	private int max_lane;
	private int current_lane;

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

		// Danny was here.
		num_beats_between_obstacle_movement = 1;
		num_beats_since_last_obstacle_movement = 0;
		num_beats_between_obstacle_movement_max = 3;

		// Danny was here.
		num_beats_between_obstacle_generation_min = 10;
		num_beats_between_obstacle_generation_max = 30;
		current_num_beats_between_obstacle_generation = getWaitTimeUntilNextObstacle();
		num_beats_since_last_obstacle_generation = 0;

		// Danny was here.
		num_beats_between_key_generation_min = 36;
		num_beats_between_key_generation_max = 51;
		current_num_beats_between_key_generation = getWaitTimeUntilNextKey();
		num_beats_since_last_key_generation = 0;

		min_lane = 1;
		max_lane = 3;
		current_lane = 2;
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
			num_beats_between_obstacle_generation_max += 5;
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
		Application.LoadLevel("GameLoseScene");//Win
		Debug.Log( "Game over player one." );
	}


	public void trigger( int actionType )
	{
		// Danny was here.
		++num_beats_since_last_obstacle_movement;
		++num_beats_since_last_obstacle_generation;
		++num_beats_since_last_key_generation;

		// Danny was here.
		if ( num_beats_since_last_obstacle_generation > current_num_beats_between_obstacle_generation ) {
			num_beats_since_last_obstacle_generation = 0;
			current_num_beats_between_obstacle_generation = getWaitTimeUntilNextObstacle();

			// Obstacle generation.
			int rand_lane = Random.Range( 1, 4 );
			int rand_stack = Random.Range( 1, 101 );
			int stack_chance = 40;
			if ( rand_lane == 1 ) {
				GameObject game_object = GameObject.FindGameObjectWithTag( "LowCrateGen" );
				ObstacleGenerator obstacle_generator = game_object.GetComponent<ObstacleGenerator>();
				obstacle_generator.CreateCrate();

				if ( rand_stack < stack_chance ) {
					GameObject game_object_mid = GameObject.FindGameObjectWithTag( "MidCrateGen" );
					ObstacleGenerator obstacle_generator_mid = game_object_mid.GetComponent<ObstacleGenerator>();
					obstacle_generator_mid.CreateCrate();
				}
			}
			else if ( rand_lane == 2 ) {
				GameObject game_object = GameObject.FindGameObjectWithTag( "MidCrateGen" );
				ObstacleGenerator obstacle_generator = game_object.GetComponent<ObstacleGenerator>();
				obstacle_generator.CreateCrate();

				if ( rand_stack < stack_chance ) {
					int top_or_bottom = Random.Range( 1, 3 );
					if ( top_or_bottom == 1 ) {
						GameObject game_object_top = GameObject.FindGameObjectWithTag( "HighCrateGen" );
						ObstacleGenerator obstacle_generator_top = game_object_top.GetComponent<ObstacleGenerator>();
						obstacle_generator_top.CreateCrate();
					}
					else {
						GameObject game_object_bottom = GameObject.FindGameObjectWithTag( "LowCrateGen" );
						ObstacleGenerator obstacle_generator_bottom = game_object_bottom.GetComponent<ObstacleGenerator>();
						obstacle_generator_bottom.CreateCrate();
					}
				}
			}
			else {
				GameObject game_object = GameObject.FindGameObjectWithTag( "HighCrateGen" );
				ObstacleGenerator obstacle_generator = game_object.GetComponent<ObstacleGenerator>();
				obstacle_generator.CreateCrate();

				if ( rand_stack < stack_chance ) {
					GameObject game_object_mid = GameObject.FindGameObjectWithTag( "MidCrateGen" );
					ObstacleGenerator obstacle_generator_mid = game_object_mid.GetComponent<ObstacleGenerator>();
					obstacle_generator_mid.CreateCrate();
				}
			}

//			float randValue = Random.Range( 0, 2 );
//			GameObject gameObjCrate;
//			if ( randValue < 1 ){
//				gameObjCrate = GameObject.FindGameObjectWithTag( "HighCrateGen" );
//			}
//			else {
//				gameObjCrate = GameObject.FindGameObjectWithTag( "LowCrateGen" );
//			}
//			ObstacleGenerator obsGen = gameObjCrate.GetComponent<ObstacleGenerator>();
//			obsGen.CreateCrate();
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
			if( grid.hasObstacle( playerPositionDiscreteX + 1, playerPositionDiscreteY ) == false ){
				playerPositionDiscreteX++;
				transform.position = grid.computePlayerPosition (playerPositionDiscreteX, playerPositionDiscreteY);
			}
		}

		//Action: Jump
		if (actionType == 2) {
			if ( current_lane < max_lane ) {
				++current_lane;

				playerPositionDiscreteY++;
				transform.position = grid.computePlayerPosition (playerPositionDiscreteX, playerPositionDiscreteY);
			}
		}

		//Action: Slide
		if(actionType == 3) {
			if ( current_lane > min_lane ) {
				--current_lane;

				playerPositionDiscreteY--;
				transform.position = grid.computePlayerPosition (playerPositionDiscreteX, playerPositionDiscreteY);
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
}