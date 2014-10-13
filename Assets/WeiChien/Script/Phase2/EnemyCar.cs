using UnityEngine;
using System.Collections;

public class EnemyCar : MonoBehaviour {

	float timer;
	float beatTime;
	int moveDir;


	private float currentLocation;
	float temp;
	bool accelerate;

	// Use this for initialization
	void Start () {
		gameObject.transform.localPosition = new Vector3 (3, 0.0f, 0.0f);
		gameObject.transform.localRotation = Quaternion.Euler (new Vector3 (0, 270, 0));
		gameObject.transform.localScale = new Vector3 (0.3f, 0.3f, 0.3f);

		timer = 0;
		beatTime = 0.05f;
		moveDir = 0;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		
		/*if (timer > beatTime) {
			RandomMove((moveDir++ / 10) % 2);
			timer = 0;
		}*/

		ComputeCarPosition (timer);
		timer = 0;
	}

	void RandomMove(int dir){
		switch (dir) {
		case 0:
				MoveForward ();
				break;
		case 1:
				MoveBackward ();
				break;
		}
	}

	void ComputeCarPosition(float l){
		
		currentLocation += l;
		
		float angleY;
		float coordX;
		float coordZ;
		
		if (currentLocation < 65) {
			
		} else if (currentLocation >= 65 && currentLocation <= 80) {
			
		}
		coordX = currentLocation;
		//coordZ = -0.8f;
		gameObject.transform.localPosition = new Vector3 (3 + coordX, 0.0f, 0.0f);

		
	}

	public void MoveForward(){
		gameObject.transform.localPosition += new Vector3 (0.02f, 0.0f, 0.0f);

	}
	
	public void MoveBackward(){
		gameObject.transform.localPosition -= new Vector3 (0.02f, 0.0f, 0.0f);
	}
}
