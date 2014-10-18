using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {

	int[] map;
	int width = 30;
	int height = 3;
	// Use this for initialization
	void Start () {
		map = new int[width * height]; 
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setObjectInGrid(int x, int y, int type){
		map [y * width + x] = type;
	}

	public bool hasObstacle(int x, int y){
		if (map [y * width + x] == 1)
			return true;
		else 
			return false;
	}

	public Vector3 computeCratePosition(int x, int y){
		return new Vector3( 400 - 12 + 24.0f / 29.0f * x, 0 - 4.2f + y * 1.6f, -1);
	}

	public Vector3 computePlayerPosition(int x, int y){
		return new Vector3( 400 - 12 + 24.0f / 29.0f * x, 0 - 4.0f + y * 1.6f, -1);
	}
}
