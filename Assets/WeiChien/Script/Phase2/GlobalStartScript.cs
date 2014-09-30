using UnityEngine;
using System.Collections;

public class GlobalStartScript : MonoBehaviour {

	public GameObject gameObjPlayerCar;
	public GameObject gameObjEnemyCar;


	// Use this for initialization
	void Start () {
		GameObject objPlayerCar = Instantiate (gameObjPlayerCar, new Vector3(0,0,0), Quaternion.identity) as GameObject;
		GameObject objEnemyCar = Instantiate (gameObjEnemyCar, new Vector3(0,0,0), Quaternion.identity) as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
