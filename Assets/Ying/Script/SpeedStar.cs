using UnityEngine;
using System.Collections;

public class SpeedStar : MonoBehaviour {
	Vector3 orgPos;
	float speed;
	// Use this for initialization
	void Start () {
		orgPos = gameObject.transform.position;
		speed = PlayerPrefs.GetFloat("noteSpeed");
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.position = new Vector3 (transform.position.x-speed, orgPos.y, orgPos.z);
		if (gameObject.transform.position.x < PlayerPrefs.GetFloat("HittingCenter")- PlayerPrefs.GetFloat ("halfHittingRange"))
			Destroy (gameObject);
	}
}
