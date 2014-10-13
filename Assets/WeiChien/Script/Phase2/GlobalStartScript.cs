using UnityEngine;
using System.Collections;

public class GlobalStartScript : MonoBehaviour {

	public GameObject gameObjPlayerCar;
	public GameObject gameObjEnemyCar;
	public GameObject gameObjRoad90m;
	public GameObject gameObjRoadCrossX;
	public GameObject gameObjRoadCrossT;
	public GameObject gameObjRoadTurn90;



	// Use this for initialization
	void Start () {
		GameObject objPlayerCar = Instantiate (gameObjPlayerCar, new Vector3(0,0,0), Quaternion.identity) as GameObject;
		GameObject objEnemyCar = Instantiate (gameObjEnemyCar, new Vector3(0,0,0), Quaternion.identity) as GameObject;
		//GameObject objRoad90m = Instantiate (gameObjRoad90m, new Vector3(0,0,0), Quaternion.Euler(new Vector3(270,90,0))) as GameObject;


	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("a")) {

		}
	}

	Vector3 ComputeCarPosition(float l){

		Vector3 returnValue = new Vector3(0,0,0);
		return returnValue;
	}
}
