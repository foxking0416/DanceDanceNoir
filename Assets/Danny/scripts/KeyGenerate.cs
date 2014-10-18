using UnityEngine;
using System.Collections;

public class KeyGenerate : MonoBehaviour {

	public GameObject key;
	private GameObject gameObjGrid;
	private Grid grid;

	// Use this for initialization
	void Start () {
		gameObjGrid = GameObject.FindGameObjectWithTag ("Grid");
		grid = gameObjGrid.GetComponent< Grid > ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void createKey(){
		GameObject objKey = Instantiate( key, grid.computeCratePosition(29, 2), transform.rotation ) as GameObject;
	}
}
