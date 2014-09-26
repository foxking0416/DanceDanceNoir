using UnityEngine;
using System.Collections;

public class MusicNote : MonoBehaviour {
	public Vector3 orgPos;
	public float speed;

	// Use this for initialization
	void Start () {
		orgPos = gameObject.transform.position;
		speed = 0.08f;

	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.position = new Vector3 (transform.position.x-speed, orgPos.y, orgPos.z);

		if (gameObject.transform.position.x < PlayerPrefs.GetFloat("HittingPos"))
		{
			Destroy (gameObject);
		}
	}
}
