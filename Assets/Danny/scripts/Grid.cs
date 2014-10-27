using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {

	int[] map;
	int width = 14;
	int height = 3;

	// Use this for initialization
	void Start () {
		map = new int[width * height];

		// Debug.
//		for ( int i = 0; i < width * height; ++i ) {
//			map[i] = 1;
//		}
	}

	// Getters.
	public int getWidth()
	{
		return width;
	}
	public int getHeight()
	{
		return height;
	}

	public void setObjectInGrid(int x, int y, int type){

		int linear_index = y * width + x;
		if ( linear_index >= width * height ) {
			return;
		}

		map[linear_index] = type;
	}

	public void resetGrid(){
		for (int i = 0; i < width * height; ++i) {
			map[i] = 0;	
		}
	}

	public bool hasObstacle(int x, int y){

		int linear_index = y * width + x;
		if ( linear_index >= width * height ) {
			return true;
		}

		if ( map[linear_index] == 1 ) {
			return true;
		}
		else {
			return false;
		}
	}

	public int hasKeys(int x, int y){

		int linear_index = y * width + x;
		if ( linear_index >= width * height ) {
			return 0;
		}

		return map[linear_index];
	}

	public Vector3 computeCratePosition(int x, int y){
		return new Vector3( 400 - 12 + 24.0f / ( float )( width - 1 ) * x, 0 - 4.2f + y * 1.6f, -1);
		//return new Vector3( 400 - 12 + 24.0f / ( float )( width - 1 ) * x, 0 - 2.2f + y * 1.6f, -1);
	}

	public Vector3 computePlayerPosition(int x, int y){
		return new Vector3( 400 - 12 + 24.0f / ( float )( width - 1 ) * x, 0 - 4.0f + y * 1.6f, -1);
	}
}
