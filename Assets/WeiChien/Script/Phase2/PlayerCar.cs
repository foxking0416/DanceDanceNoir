using UnityEngine;
using System.Collections;

public class PlayerCar : MonoBehaviour {

	private GameObject gameObjCamera;
	private float timer;
	private float currentLocation;
	float temp;
	bool accelerate;
	bool combo;

	// Use this for initialization
	void Start () {
		gameObject.transform.localPosition = new Vector3 (-1, 0.0f, -0.8f);
		gameObject.transform.localRotation = Quaternion.Euler (new Vector3 (0, 90, 0));
		gameObject.transform.localScale = new Vector3 (0.3f, 0.3f, 0.3f);

		timer = 0;
		currentLocation = 0;
		accelerate = false;
		combo = false;
		temp = 0;
		gameObjCamera = GameObject.FindGameObjectWithTag("MainCamera");
		gameObjCamera.camera.orthographic = false;
		gameObjCamera.camera.transform.localPosition = gameObject.transform.localPosition + new Vector3(0,10,0);
		gameObjCamera.camera.transform.localRotation = gameObject.transform.localRotation;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown ("a")) {
			accelerate = !accelerate;
		}

		timer += Time.deltaTime;
		temp += Time.deltaTime;
		if (temp > 5) {
			Debug.Log("location: " + timer);	
			temp =0;
	
		}
		if(combo == true)
			ComputeCarPosition (timer * 4);
		else if(accelerate == true)
			ComputeCarPosition (timer * 2);
		else
			ComputeCarPosition (timer);

		timer = 0;
	}

	void ComputeCarPosition(float l){

		currentLocation += l;

		float angleY;
		float coordX;
		float coordZ;

		if (currentLocation < 65) {
			coordX = currentLocation;
			coordZ = 0;
		} else if (currentLocation >= 65 && currentLocation < 65 + 20 * 2 * Mathf.PI / 4.0f) {
			coordX = 65 + 20 * Mathf.Sin ((currentLocation - 65) / (20 * 2 * Mathf.PI / 4.0f ) / 4 * Mathf.PI);
			coordZ = -20 + 20 * Mathf.Cos ((currentLocation - 65) / (20 * 2 * Mathf.PI / 4.0f) / 4 * Mathf.PI);
		} else {
			coordX = 85;
			coordZ = -20;
		}


		//coordX = currentLocation;
		//coordZ = -0.8f;
		gameObject.transform.localPosition = new Vector3 (coordX, 0.0f, coordZ);
		gameObjCamera.camera.transform.localPosition = gameObject.transform.localPosition + new Vector3(0,1,0);
		gameObjCamera.camera.transform.localRotation = gameObject.transform.localRotation;//Quaternion.Euler(gameObject.transform.localRotation.eulerAngles + new Vector3(40,0,0));

	}

	public void MoveForward(){}

	public void MoveBackward(){}

	void Combo(){


	}

}
