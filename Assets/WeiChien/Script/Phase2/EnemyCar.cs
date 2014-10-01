using UnityEngine;
using System.Collections;

public class EnemyCar : MonoBehaviour {

	float timer;
	float beatTime;
	int moveDir;

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
		
		if (timer > beatTime) {
			RandomMove((moveDir++ / 10) % 2);
			timer = 0;
		}


	}

	void RandomMove(int dir){
		//int moveDir = Random.Range (1, 3);
		//MoveForward();
		switch (dir) {
		case 0:
				MoveForward ();
				break;
		case 1:
				MoveBackward ();
				break;
		}
	}

	public void MoveForward(){
		gameObject.transform.localPosition += new Vector3 (0.02f, 0.0f, 0.0f);

	}
	
	public void MoveBackward(){
		gameObject.transform.localPosition -= new Vector3 (0.02f, 0.0f, 0.0f);
	}
}
