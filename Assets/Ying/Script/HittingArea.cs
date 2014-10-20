using UnityEngine;
using System.Collections;

public class HittingArea : MonoBehaviour {
	Vector3 orgScale;
	// Use this for initialization
	void Start () {
		orgScale = gameObject.transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 localScale = gameObject.transform.localScale;
		if (localScale.x > orgScale.x)
			gameObject.transform.localScale = new Vector3(localScale.x-0.01f, orgScale.y, localScale.z-0.01f);
	}

	public void enlarge()
	{
		gameObject.transform.localScale = new Vector3 (0.8f, orgScale.y, 0.8f);
	}
}
