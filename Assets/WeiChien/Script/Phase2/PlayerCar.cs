using UnityEngine;
using System.Collections;

public class PlayerCar : MonoBehaviour {

	private GameObject gameObjCamera;

	// Use this for initialization
	void Start () {
		gameObject.transform.localPosition = new Vector3 (-1, 0.0f, -0.8f);
		gameObject.transform.localRotation = Quaternion.Euler (new Vector3 (0, 90, 0));
		gameObject.transform.localScale = new Vector3 (0.3f, 0.3f, 0.3f);


		gameObjCamera = GameObject.FindGameObjectWithTag("MainCamera");
		//pgameObjCamera.camera.orthographic = true;
		//pgameObjCamera.camera.transform.localPosition = gameObject.transform.localPosition;

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	Vector3 ComputeCarPosition(float l){

		float angleY;
		float coordX;
		float coordZ;

		coordX = -1 + l;
		coordZ = -0.8f;
		//gameObject.transform.localPosition = new Vector3 (-1, 0.0f, -0.8f);

		Vector3 returnValue = new Vector3(0,0,0);
		return returnValue;
	}

	public void MoveForward(){}

	public void MoveBackward(){}

	void Combo(){


	}

}
