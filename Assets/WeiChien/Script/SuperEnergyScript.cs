using UnityEngine;
using System.Collections;

public class SuperEnergyScript : MonoBehaviour {

	private GameObject gameObjGridMap;
	private GridMap map;
	private GameObject gameObjGlobal;
	private GlobalScript global;

	public int positionX;
	public int positionZ;

	// Use this for initialization
	void Start () {
		gameObjGridMap = GameObject.Find ("Map");
		map = gameObjGridMap.GetComponent< GridMap >();

		gameObjGlobal = GameObject.Find ("Global");
		global = gameObjGridMap.GetComponent< GlobalScript >();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Pick(){
		map.UpdateObjectsStatus (positionX, positionZ, 0);
		global.superEnergy = 1;

		Destroy (gameObject);
	}
}
